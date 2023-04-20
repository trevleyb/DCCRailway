using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

/// <summary>
///     Write a value to a specific CV
/// </summary>
public interface ICmdCVWrite : ICommand {
    public DCCProgrammingMode ProgrammingMode { get; set; }
    public int CV { get; set; }
    public byte Value { get; set; }
}