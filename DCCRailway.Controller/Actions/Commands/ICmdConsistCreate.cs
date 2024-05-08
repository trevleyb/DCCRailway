using DCCRailway.Common.Types;
using DCCRailway.Controller.Actions.Commands.Base;

namespace DCCRailway.Controller.Actions.Commands;

public interface ICmdConsistCreate : ICommand, IConsistCmd {
    public byte             ConsistAddress { get; set; }
    public DCCAddress       LeadLoco       { get; set; }
    public DCCAddress       RearLoco       { get; set; }
    public List<DCCAddress> AddLoco        { get; }
}