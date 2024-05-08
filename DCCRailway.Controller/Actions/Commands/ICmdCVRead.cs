using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

/// <summary>
///     Read a CV from a controller
/// </summary>
public interface ICmdCVRead : ICommand, ICVCmd {
    public DCCProgrammingMode ProgrammingMode { get; set; }
}