using DCCRailway.System.Layout.Commands;

namespace DCCRailway.System.Layout.Adapters.Events;

public interface IAdapterEvent  {
    public IAdapter? Adapter { get; set; }
    public ICommand? Command { get; set; }
}