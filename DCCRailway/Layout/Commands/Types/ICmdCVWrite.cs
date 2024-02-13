using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCommand {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public byte               Value           { get; set; }
}