﻿using System.Text.Json.Serialization;
using DCCRailway.Layout.Configuration.Entities.Base;

namespace DCCRailway.Layout.Configuration.Entities.Layout;

[Serializable]
public class Block(string id = "") : BaseEntity(id) { }