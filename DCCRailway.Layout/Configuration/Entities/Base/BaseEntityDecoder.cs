using DCCRailway.Common.Types;
using DCCRailway.Layout.Configuration.Entities.Layout;
using DCCRailway.Layout.Configuration.Entities.System;
using DCCRailway.Layout.Configuration.Repository;

namespace DCCRailway.Layout.Configuration.Entities.Base;

[Serializable]
public class BaseEntityDecoder(Guid id, DCCAddressType addressType) : BaseEntity(id) {
    public DCCAddressType AddressType { get; set; } = addressType;
    public Decoder? Decoder { get; set; }
    public IDCCAddress? Address { get; set; }
}