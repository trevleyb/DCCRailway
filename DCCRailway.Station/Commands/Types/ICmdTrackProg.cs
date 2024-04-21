using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

/// <summary>
///     Turn the power to the controller ON or OFF
/// </summary>
public interface ICmdTrackProg : ICommand, ISystemCmd { }