using DCCRailway.Station.Attributes;
using DCCRailway.Station.Commands.Types;

namespace DCCRailway.Station.Virtual.Commands;

[Command("MacroRun", "Execute a Macro")]
public class VirtualMacroRun : VirtualCommand, ICmdMacroRun {
    public          byte   Macro      { get; set; }
    public override string ToString() => $"RUN MACRO ({Macro})";
}