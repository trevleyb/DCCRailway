using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.Managers.Updater;

public interface IStateUpdater {
    IResult Process(ICmdResult result);
}