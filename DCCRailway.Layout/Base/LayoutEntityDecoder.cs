using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout.Base;

[Serializable]
public class LayoutEntityDecoder(string id = "") : LayoutEntity(id) {
    private DCCAddress _address;
    private Decoder    _decoder;

    public DCCAddress Address {
        get => _address;
        set => SetField(ref _address, value);
    }

    public Decoder Decoder {
        get => _decoder;
        set => SetField(ref _decoder, value);
    }
}