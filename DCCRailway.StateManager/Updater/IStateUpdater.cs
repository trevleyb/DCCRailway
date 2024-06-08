using DCCRailway.Common.Helpers;

namespace DCCRailway.StateManager.Updater;

public interface IStateUpdater {
    IResult Process(object message);
}