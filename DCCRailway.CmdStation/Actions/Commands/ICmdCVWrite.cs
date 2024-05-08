using DCCRailway.CmdStation.Actions.Commands.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Actions.Commands;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public byte               Value           { get; set; }
}