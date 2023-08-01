using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdConsistCreate : ICommand {
    public byte ConsistAddress { get; set; }
    public IDCCLoco LeadLoco { get; set; }
    public IDCCLoco RearLoco { get; set; }
    public List<IDCCLoco> AddLoco { get; }
}