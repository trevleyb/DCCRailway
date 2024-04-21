using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

/// <summary>
///     Get the current status of the controller
/// </summary>
public interface ICmdStatus : ICommand, ISystemCmd { }