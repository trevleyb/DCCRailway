using DCCPacketAnalyser.Analyser.Base;
using DCCPacketAnalyser.Analyser.Messages;
using DCCRailway.Common.Result;

namespace DCCRailway.Controller.States.PacketUpdater;

/// <summary>
///     The LayoutCmdUpdated class is a bridge between an Event being recieved from a system,
///     and the Entities Configuration itself.This is because, while the layout might issue a command
///     to the command statin and it will be executed, other systems might issue a command also
///     and so if these are detected (for example if we have a DC packet analyser listening to commands)
///     then we need to update the Entities data with these changes.
///     So this is a bridge between the two systems. It takes a DCCRailwayConfig instance whcih is
///     the collection of all data related to the current executing layout.
/// </summary>
public static class PacketStateUpdater {
    public static IResult Process(IPacketMessage packetMessage, IStateTracker stateTracker) {
        var result = packetMessage switch {
            AccessoryMessage accyMsg          => UpdateAccyMsg(accyMsg),
            AnalogFunctionMessage funcMsg     => UpdateAnalogFuncMsg(funcMsg),
            BinaryStateMessage binMsg         => UpdateBinMsg(binMsg),
            ConfigCvMessage cvMsg             => UpdateCvMsg(cvMsg),
            ConsistMessage consistMsg         => UpdateConsistMsg(consistMsg),
            ControlMessage controlMsg         => UpdateControlMsg(controlMsg),
            DuplicateMessage dupMsg           => UpdateDupMsg(dupMsg),
            ErrorMessage error                => UpdateErrorMsg(error),
            FunctionsMessage funcMsg          => UpdateFuncMsg(funcMsg),
            IdleMessage idleMsg               => UpdateIdleMsg(idleMsg),
            MomentumMessage momMsg            => UpdateMomMsg(momMsg),
            SignalMessage signalMsg           => UpdateSignalMsg(signalMsg),
            SpeedAndDirectionMessage speedMsg => UpdateSpeedMsg(speedMsg),
            SpeedStepsMessage stepsMsg        => UpdateStepsMsg(stepsMsg),
            _                                 => Result.Fail("Unknown Packet Message Type.")
        };

        return result;
    }

    private static IResult UpdateStepsMsg(SpeedStepsMessage stepsMsg) {
        return Result.Ok();
    }

    private static IResult UpdateSpeedMsg(SpeedAndDirectionMessage speedMsg) {
        return Result.Ok();
    }

    private static IResult UpdateSignalMsg(SignalMessage signalMsg) {
        return Result.Ok();
    }

    private static IResult UpdateMomMsg(MomentumMessage momMsg) {
        return Result.Ok();
    }

    private static IResult UpdateIdleMsg(IdleMessage idleMsg) {
        return Result.Ok();
    }

    private static IResult UpdateErrorMsg(ErrorMessage error) {
        return Result.Ok();
    }

    private static IResult UpdateDupMsg(DuplicateMessage dupMsg) {
        return Result.Ok();
    }

    private static IResult UpdateControlMsg(ControlMessage controlMsg) {
        return Result.Ok();
    }

    private static IResult UpdateConsistMsg(ConsistMessage consistMsg) {
        return Result.Ok();
    }

    private static IResult UpdateCvMsg(ConfigCvMessage cvMsg) {
        return Result.Ok();
    }

    private static IResult UpdateBinMsg(BinaryStateMessage binMsg) {
        return Result.Ok();
    }

    private static IResult UpdateFuncMsg(FunctionsMessage funcMsg) {
        return Result.Ok();
    }

    private static IResult UpdateAnalogFuncMsg(AnalogFunctionMessage funcMsg) {
        return Result.Ok();
    }

    public static IResult UpdateAccyMsg(AccessoryMessage accyMsg) {
        return Result.Ok();
    }
}