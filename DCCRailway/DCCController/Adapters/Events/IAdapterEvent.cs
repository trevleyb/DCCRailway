using DCCRailway.DCCController.Commands;

namespace DCCRailway.DCCController.Adapters.Events;

public interface IAdapterEvent  {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}