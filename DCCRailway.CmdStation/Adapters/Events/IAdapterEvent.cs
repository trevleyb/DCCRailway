using DCCRailway.CmdStation.Adapters.Base;
using DCCRailway.CmdStation.Commands;

namespace DCCRailway.CmdStation.Adapters.Events;

public interface IAdapterEvent {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}