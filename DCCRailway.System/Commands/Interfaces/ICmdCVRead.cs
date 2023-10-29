using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

/// <summary>
///     Read a CV from a system
/// </summary>
public interface ICmdCVRead : ICommand, ICVCommand {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }
}