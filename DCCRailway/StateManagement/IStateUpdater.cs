using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.StateManagement;

public interface IStateUpdater {
    IResult Process(ICmdResult result);
}