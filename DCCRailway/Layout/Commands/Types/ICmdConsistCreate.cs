using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdConsistCreate : ICommand {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public List<DCCAddress> AddLoco        { get; }
}