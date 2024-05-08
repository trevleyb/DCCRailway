using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Adapters.Base;

namespace DCCRailway.Controller.Adapters.Events;

public interface IAdapterEvent {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}