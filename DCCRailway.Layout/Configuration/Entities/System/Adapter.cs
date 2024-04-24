using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

public class Adapter : BaseEntity {

    public string AdapterName;
    public Parameters Parameters = new();
    public Adapter() : base() { }

}