using System.Collections.Generic;
using DCCRailway.Core.Systems.Types;

namespace DCCRailway.Core.Systems.Commands.Interfaces; 

public interface ICmdConsistCreate : ICommand {
    public byte ConsistAddress { get; set; }
    public IDCCLoco LeadLoco { get; set; }
    public IDCCLoco RearLoco { get; set; }
    public List<IDCCLoco> AddLoco { get; }
}