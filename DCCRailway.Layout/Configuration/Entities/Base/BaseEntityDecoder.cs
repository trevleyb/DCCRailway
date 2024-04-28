using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Repository;

namespace DCCRailway.Layout.Configuration.Entities.Base;

[Serializable]
public class BaseEntityDecoder(string id, DCCAddressType addressType) : BaseEntity(id) {

    private DCCAddressType _addressType = addressType;
    private IDCCAddress    _address;
    private Decoder        _decoder;

    public IDCCAddress      Address     { get => _address; set => SetField(ref _address, value); }
    public DCCAddressType   AddressType { get => _addressType; set => SetField(ref _addressType, value); }
    public Decoder          Decoder     { get => _decoder; set => SetField(ref _decoder, value); }

}