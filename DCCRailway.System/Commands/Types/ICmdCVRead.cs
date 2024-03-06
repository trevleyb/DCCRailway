using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}