using DCCRailway.Common.Types;

namespace DCCRailway.Layout.Entities.Base;

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