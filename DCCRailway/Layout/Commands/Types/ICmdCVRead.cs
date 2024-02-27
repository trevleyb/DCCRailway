using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}