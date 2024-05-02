using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Results;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Application.WiThrottle;

public class WitThrottleLayoutCmd(IController controller, IDCCAddress? address = null) {

    public void Stop() { }
    public void Release() { }
    public void Dispatch() { }

    public bool IsReleaseSupported() => controller.IsCommandSupported<ICmdLocoRelease>();
    public bool IsDispatchSupported() => controller.IsCommandSupported<ICmdLocoDispatch>();
    public bool IsAcquireSupported() => controller.IsCommandSupported<ICmdLocoAcquire>();
    public bool IsPowerSupported() => controller.IsCommandSupported<ICmdPowerSetOn>();

    public void SetTurnoutState(DCCTurnoutState state) {
        if (controller.IsCommandSupported<ICmdTurnoutSet>()) {
            var command = controller.CreateCommand<ICmdTurnoutSet>();
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

    public DCCPowerState PowerState => ((IResultPowerState)controller.CreateCommand<ICmdPowerGetState>()?.Execute()!).State;

}