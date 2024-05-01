using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types;
using DCCRailway.Station.Controllers;

namespace DCCRailway.Application.WiThrottle;

public class WitThrottleLayoutCmd(IController controller, DCCAddress address) {

    public void Stop() { }
    public void Release() { }
    public void Dispatch() { }

    public bool IsReleaseSupported() => controller.IsCommandSupported<ICmdLocoRelease>();
    public bool IsDispatchSupported() => controller.IsCommandSupported<ICmdLocoDispatch>();
    public bool IsAcquireSupported() => controller.IsCommandSupported<ICmdLocoAcquire>();
    public bool IsPowerSupported() => controller.IsCommandSupported<ICmdPowerSetOn>();

    public void SetPowerState(DCCPowerState state) {
        //_ = state switch {
        //    DCCPowerState.On => controller.CreateCommand<ICmdPowerSetOn>().Execute(null);
        //
        //}

        /*
        if (connection.ActiveController.IsCommandSupported<ICmdPowerSetOff>()) {
            var powerCmd = connection.ActiveController.CreateCommand<ICmdPowerSetOff>();
            if (powerCmd != null) connection.ActiveController.Execute(powerCmd);
        }
        break;
        case '1':
        if (connection.ActiveController.IsCommandSupported<ICmdPowerSetOn>()) {
            var powerCmd = connection.ActiveController.CreateCommand<ICmdPowerSetOn>();
            if (powerCmd != null) connection.ActiveController.Execute(powerCmd);
        }
        break;
*/

    }
}