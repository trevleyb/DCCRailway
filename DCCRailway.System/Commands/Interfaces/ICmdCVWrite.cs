using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCommand {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int                CV              { get; set; }
    public byte               Value           { get; set; }
}