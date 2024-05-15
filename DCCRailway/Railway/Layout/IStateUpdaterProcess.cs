using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.Railway.Layout;

public interface IStateUpdaterProcess {
    ICmdResult Result  { get; }
    ICommand?  Command { get; }
    byte[]?    Data    { get; }

    bool Process();
}