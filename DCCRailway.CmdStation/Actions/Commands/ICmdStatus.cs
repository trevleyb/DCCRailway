using DCCRailway.CmdStation.Actions.Commands.Base;

namespace DCCRailway.CmdStation.Actions.Commands;

/// <summary>
///     Get the current status of the controller
/// </summary>
public interface ICmdStatus : ICommand, ISystemCmd { }