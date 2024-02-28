using DCCRailway.DCCController.Commands.Types.BaseTypes;
using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCController.Commands.Types;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public byte               Value           { get; set; }
}