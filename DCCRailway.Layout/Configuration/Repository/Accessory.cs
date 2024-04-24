﻿using DCCRailway.Layout.Configuration.Entities;
using DCCRailway.Layout.Configuration.Entities.Layout;

namespace DCCRailway.Layout.Configuration.Repository;

public class AccessoryRepository(IEntityCollection<Accessory> collection) :
    BaseRepository<Guid, Accessory>(collection) { }