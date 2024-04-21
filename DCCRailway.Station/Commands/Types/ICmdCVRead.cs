using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}