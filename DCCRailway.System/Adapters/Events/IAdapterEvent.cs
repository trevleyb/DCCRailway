using DCCRailway.System.Commands;

namespace DCCRailway.System.Adapters.Events;

public interface IAdapterEvent {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}