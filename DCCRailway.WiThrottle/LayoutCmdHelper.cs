using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Controllers;

namespace DCCRailway.WiThrottle;

public class LayoutCmdHelper(ICommandStation commandStation, DCCAddress? address = null)
{
    public DCCPowerState PowerState
    {
        get
        {
            var command = commandStation.CreateCommand<ICmdPowerGetState>();
            if (command?.Execute() is ICmdResultPowerState res) return res.State;
            return DCCPowerState.Unknown;
        }
    }

    public void Stop()
    {
    }

    public void Release()
    {
    }

    public void Dispatch()
    {
    }

    public bool IsReleaseSupported()  => commandStation.IsCommandSupported<ICmdLocoRelease>();
    public bool IsDispatchSupported() => commandStation.IsCommandSupported<ICmdLocoDispatch>();
    public bool IsAcquireSupported()  => commandStation.IsCommandSupported<ICmdLocoAcquire>();
    public bool IsPowerSupported()    => commandStation.IsCommandSupported<ICmdPowerSetOn>();

    public void SetTurnoutState(DCCTurnoutState state)
    {
        if (commandStation.IsCommandSupported<ICmdTurnoutSet>())
        {
            var command = commandStation.CreateCommand<ICmdTurnoutSet>(address);
            if (command != null)
            {
                command.State = state;
                command.Execute();
            }
        }
    }

    public void SetPowerState(DCCPowerState state)
    {
        _ = state switch
        {
            DCCPowerState.On  => commandStation.CreateCommand<ICmdPowerSetOn>()?.Execute(),
            DCCPowerState.Off => commandStation.CreateCommand<ICmdPowerSetOff>()?.Execute(),
            _                 => null
        };
    }
}