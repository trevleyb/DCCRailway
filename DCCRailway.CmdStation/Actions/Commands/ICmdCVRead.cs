using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}