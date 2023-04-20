using DCCRailway.Core.Systems.Adapters;

namespace DCCRailway.Systems.NCE.Adapters; 

public class NCENetwork : NetworkAdapter, IAdapter {
    public new static string Name => "NCE Network Adapter";

    public override string Description => "NCE-Network";
}