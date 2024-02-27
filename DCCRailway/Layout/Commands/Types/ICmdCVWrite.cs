using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public byte               Value           { get; set; }
}