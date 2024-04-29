using DCCRailway.Layout.Configuration.Entities.Collection;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Controllers(string prefix = "CMD") : Repository<Controller>(prefix) {
    public Controller? DefaultController {
        get {
            if (this.Count == 1) return this[0];
            foreach (var controller in this) {
                if (controller.IsActive) return controller;
            }
            if (this.Count > 0) return this[0];
            return null;
        }
    }
}