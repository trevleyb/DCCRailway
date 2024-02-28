using DCCRailway.DCCController.Types;

namespace DCCRailway.DCCLayout.Entities.Base;

public class ConfigCollectionWithDecoder<T> : ConfigCollectionBase<T> where T: ConfigWithDecoder {
    public T? this[IDCCAddress address] => Find(address);
    public T? Find(IDCCAddress address) {
        var element = Find(x => x.Address.Address == address.Address);
        return element ?? null; 
    }
}