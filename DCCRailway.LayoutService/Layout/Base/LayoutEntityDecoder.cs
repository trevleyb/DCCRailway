using DCCRailway.Common.Types;
using DCCRailway.LayoutService.Layout.Entities;

namespace DCCRailway.LayoutService.Layout.Base;

[Serializable]
public class LayoutEntityDecoder(string id = "") : LayoutEntity(id) {

    private DCCAddressType _addressType;
    private DCCAddress     _address;
    private Decoder        _decoder;

    public DCCAddress       Address     { get => _address; set => SetField(ref _address, value); }
    public DCCAddressType   AddressType { get => _addressType; set => SetField(ref _addressType, value); }
    public Decoder          Decoder     { get => _decoder; set => SetField(ref _decoder, value); }

}