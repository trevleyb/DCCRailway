using DCCRailway.Layout.Commands;

namespace DCCRailway.Layout.Adapters.Events;

public interface IAdapterEvent  {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}