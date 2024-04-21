using DCCRailway.Station.Adapters.Base;
using DCCRailway.Station.Commands;

namespace DCCRailway.Station.Adapters.Events;

public interface IAdapterEvent {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}