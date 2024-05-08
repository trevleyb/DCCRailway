using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public byte               Value           { get; set; }
}