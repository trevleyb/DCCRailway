﻿using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.System;

[Serializable]
public class Adapter {
    public string? AdapterName;
    public Parameters Parameters { get; set; } = [];
}