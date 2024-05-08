using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

/// <summary>
///     Get the current status of the controller
/// </summary>
public interface ICmdStatus : ICommand, ISystemCmd { }