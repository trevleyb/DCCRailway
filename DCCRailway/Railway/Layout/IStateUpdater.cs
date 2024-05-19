using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Actions.Results;

namespace DCCRailway.Railway.Layout;

public interface IStateUpdater {
    IResult Process(ICmdResult result);
}