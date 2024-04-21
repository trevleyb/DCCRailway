using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public byte               Value           { get; set; }
}