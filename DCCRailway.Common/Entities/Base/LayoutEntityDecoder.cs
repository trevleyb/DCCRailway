using CommunityToolkit.Mvvm.ComponentModel;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities.Base;

[Serializable]
public partial class LayoutEntityDecoder : LayoutEntity {
    [ObservableProperty] private DCCAddress _address = new DCCAddress();
    [ObservableProperty] private Decoder    _decoder = new Decoder();

    /// <inheritdoc/>
    public LayoutEntityDecoder(string id = "", DCCAddressType addressType = DCCAddressType.Long) : base(id) {
        Address.AddressType = addressType;
    }
}