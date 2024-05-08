using DCCRailway.CmdStation.Commands.Results;
using DCCRailway.CmdStation.Commands.Types;
using DCCRailway.CmdStation.Controllers;
using DCCRailway.Common.Types;

namespace DCCRailway.Railway.Throttles.WiThrottle;

public class WitThrottleLayoutCmd(IController controller, DCCAddress? address = null) {

    public void Stop() { }
    public void Release() { }
    public void Dispatch() { }

    public bool IsReleaseSupported() => controller.IsCommandSupported<ICmdLocoRelease>();
    public bool IsDispatchSupported() => controller.IsCommandSupported<ICmdLocoDispatch>();
    public bool IsAcquireSupported() => controller.IsCommandSupported<ICmdLocoAcquire>();
    public bool IsPowerSupported() => controller.IsCommandSupported<ICmdPowerSetOn>();

    public void SetTurnoutState(DCCTurnoutState state) {
        if (controller.IsCommandSupported<ICmdTurnoutSet>()) {
            var command = controller.CreateCommand<ICmdTurnoutSet>(address);
            if (command != null) {
                command.State = state;
                command.Execute();
            }
        }
    }

    public void SetPowerState(DCCPowerState state) {
        _ = state switch {
            DCCPowerState.On  => controller.CreateCommand<ICmdPowerSetOn>()?.Execute(),
            DCCPowerState.Off => controller.CreateCommand<ICmdPowerSetOff>()?.Execute(),
            _                 => null
        };
    }

    public DCCPowerState PowerState => ((ICmdResultPowerState)controller.CreateCommand<ICmdPowerGetState>()?.Execute()!).State;

}