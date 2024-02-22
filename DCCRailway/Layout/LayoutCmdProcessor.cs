using DCCRailway.Configuration;
using DCCRailway.Layout.Commands;
using DCCRailway.Layout.Commands.Types;
using DCCRailway.Layout.Controllers.Events;
using DCCRailway.Layout.Responses;

namespace DCCRailway.Layout;

public class LayoutCmdProcessor(DCCRailwayConfig config) {
    
    // Store an instance of the Layout Configuration so that we can post messages to it to update
    // the state of the layout as Commands are either sent or received by the Command Processor
    private readonly DCCRailwayConfig _config = config;
    
    
    public void ProcessCommandEvent(ControllerEventArgs e) {
        
        switch (e) {
        case ControllerEventCommandExec exec:
            switch (exec.Command) {
            case IDummyCmd:
                break;
            case ICmdLocoSetSpeed cmd:
                var loco = _config.Locomotives[cmd.Address] ?? throw new Exception("Locomotive not found.");
                loco.Speed.Speed = cmd.Speed;
                loco.Direction   = cmd.Direction;
                break;
            default:
                throw new Exception("Unexpected type of command executed.");
                break;
            }
            break;
        case ControllerEventAdapterAdd:
            break;
        case ControllerEventAdapterDel:
            break;
        case ControllerEventAdapter:
            break;
        default:
            throw new Exception("Unexpected type of event raised.");
        }
    }
}