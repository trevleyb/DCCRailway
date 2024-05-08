using DCCRailway.CmdStation.Commands.Types.Base;

namespace DCCRailway.CmdStation.Commands.Types;

/// <summary>
///     Get the current status of the controller
/// </summary>
public interface ICmdStatus : ICommand, ISystemCmd { }