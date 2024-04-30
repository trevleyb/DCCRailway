using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Controllers(string prefix = "CMD") : Repository<Controller>(prefix) {
    public Controllers() : this("CMD") { }

    [JsonIgnore] public Controller? DefaultController {
        get {
            if (Count == 1) return this.Values.ToArray()[0];
            foreach (var controller in this) {
                if (controller.Value.IsActive) return controller.Value;
            }
            if (Count > 0) return this.Values.ToArray()[0];
            return null;
        }
    }
}