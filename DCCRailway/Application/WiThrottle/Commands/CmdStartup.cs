using System.Text;
using DCCRailway.Common.Helpers;
using DCCRailway.Common.Types;
using DCCRailway.Components;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Repository.Base;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Commands;

/// <summary>
///     Startup command used to setup a new connection for a Throttle
/// </summary>
public class CmdStartup : ThrottleCmd, IThrottleCmd {
    public CmdStartup(WiThrottleConnectionEntry connectionEntry, string cmdString, ref WiThrottleServerOptions options) : base(connectionEntry, cmdString, ref options) { }

    public string? Execute() {
        StringBuilder response = new();
        response.AppendLine("VN2.0");           // Version 2.0 of the WiThrottle Protocol
        response.AppendLineIfNotNull(BuildRosterList(Options?.Config?.LocomotiveRepository ?? null));
        response.AppendLineIfNotNull(BuildPowerState());
        response.AppendLineIfNotNull(BuildTurnoutLabels());
        response.AppendLineIfNotNull(BuildTurnoutList(Options?.Config?.TurnoutRepository ?? null));
        //response.AppendLine ("PRT0");		// TODO: Add Routes
        //response.AppendLine ("PCC0");		// TODO: Add Consists
        response.AppendLine($"PW{Options?.Port ?? 12080}");
        response.AppendLine($"*{ConnectionEntry.HeartbeatSeconds:D2}");

        return response.ToString();
    }

    private string? BuildTurnoutList(IRepository<Turnout>? turnoutRepository) {
        if (turnoutRepository == null) return null;

        // PTL]\[LT12}|{Rico Station N}|{1]\[LT324}|{Rico Station S}|{2
        var turnoutList = new StringBuilder();
        turnoutList.Append("PTL");
        foreach (var turnout in turnoutRepository.GetAllAsync().Result) {
            turnoutList.Append("]\\[");
            turnoutList.Append(turnout.Address.Address);
            turnoutList.AppendLine("}|{");
            turnoutList.Append(turnout.Name);
            turnoutList.AppendLine("}|{");
            turnoutList.Append(GetWiThrottleTurnoutState(turnout.CurrentState));
        }
        return turnoutList.ToString();
    }

    private int GetWiThrottleTurnoutState(DCCTurnoutState turnoutCurrentState) {
        return turnoutCurrentState switch  {
            DCCTurnoutState.Thrown   => 4,
            DCCTurnoutState.Closed   => 2,
            _                        => 1
        };
    }

    private string? BuildTurnoutLabels() {
        return @"PTT]\[Turnouts}|{Turnout]\[Closed}|{2]\[Thrown}|{4]\[Unknown}|{1]\[Inconsistent}|{8";
    }

    private string? BuildPowerState() {
        // The state of the Power Control is one of the following:
        // PPA0 = Power is OFF
        // PPA1 = Power is ON
        // PPA2 = Unknown State
        return "PPA2";
    }

    private string? BuildRosterList(IRepository<Locomotive>? locoRepository) {
        if (locoRepository == null) return "RL0";

        // Example formats:
        // ---------------------------------------------------------------
        // RL0
        // RL2]\[RGS 41}|{41}|{L]\[Test Loco}|{1234}|{L
        // RL3]\[D&RGW 341}|{3}|{S]\[RGS 41}|{41}|{L]\[Test Loco}|{1234}|{L

        var locoList = new StringBuilder();
        locoList.Append("RL");
        locoList.Append(locoRepository.GetAllAsync().Result.Count());
        foreach (var loco in locoRepository.GetAllAsync().Result) {
            locoList.Append("]\\[");
            locoList.Append(loco.RoadName);
            locoList.AppendLine("}|{");
            locoList.Append(loco.Address.Address);
            locoList.AppendLine("}|{");
            locoList.Append(loco.Address.IsLong ? "L" : "S");
        }
        return locoList.ToString();
    }

    public override string ToString() => "COMMAND: STARTUP";
}