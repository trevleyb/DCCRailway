using DCCRailway.System.Layout.Types;

namespace DCCRailway.System.Layout.Commands.Types;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCommand {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}