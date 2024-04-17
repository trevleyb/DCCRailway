using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdConsistCreate : ICommand, IConsistCmd {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public List<DCCAddress> AddLoco        { get; }
}