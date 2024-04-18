using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Block(Guid id) : BaseEntity(id) {
    public Block() : this(Guid.NewGuid()) { }
}