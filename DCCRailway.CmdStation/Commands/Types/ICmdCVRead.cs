using DCCRailway.CmdStation.Commands.Types.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}