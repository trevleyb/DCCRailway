using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;

public interface ICmdConsistCreate : ICommand,IConsistCmd {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public List<DCCAddress> AddLoco        { get; }
}