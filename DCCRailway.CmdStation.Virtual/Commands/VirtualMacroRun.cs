using DCCRailway.CmdStation.Attributes;
using DCCRailway.CmdStation.Commands.Types;

namespace DCCRailway.CmdStation.Virtual.Commands;

[Command("MacroRun", "Execute a Macro")]
public class VirtualMacroRun : VirtualCommand, ICmdMacroRun {
    public          byte   Macro      { get; set; }
    public override string ToString() => $"RUN MACRO ({Macro})";
}