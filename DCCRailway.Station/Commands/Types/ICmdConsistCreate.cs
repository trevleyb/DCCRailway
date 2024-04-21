using DCCRailway.Common.Types;
using DCCRailway.Station.Commands.Types.Base;

namespace DCCRailway.Station.Commands.Types;

public interface ICmdConsistCreate : ICommand, IConsistCmd {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public List<DCCAddress> AddLoco        { get; }
}