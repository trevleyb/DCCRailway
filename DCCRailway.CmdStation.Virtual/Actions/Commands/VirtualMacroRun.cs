using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("MacroRun", "Execute a Macro")]
public class VirtualMacroRun : VirtualCommand, ICmdMacroRun {
    public          byte   Macro      { get; set; }
    public override string ToString() => $"RUN MACRO ({Macro})";
}