using DCCRailway.DCCController.Commands.Types.BaseTypes;

namespace DCCRailway.DCCController.Commands.Types;

/// <summary>
///     Get the current status of the controller
/// </summary>
public interface ICmdStatus : ICommand, ISystemCmd { }