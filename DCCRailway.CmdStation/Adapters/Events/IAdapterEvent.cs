using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Adapters.Base;

namespace DCCRailway.CmdStation.Adapters.Events;

public interface IAdapterEvent {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}