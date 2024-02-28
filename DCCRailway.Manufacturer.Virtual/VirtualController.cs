﻿using DCCRailway.DCCController.Commands.Types;
using DCCRailway.DCCController.Controllers;
using DCCRailway.DCCController.Types;
using DCCRailway.Manufacturer.Virtual.Adapters;
using DCCRailway.Manufacturer.Virtual.Commands;

namespace DCCRailway.Manufacturer.Virtual;

[Controller("Virtual", "Virtual", "Virtual")]
public class VirtualController : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<VirtualAdapter>();
    }

    protected override void RegisterCommands() {
        RegisterCommand<IDummyCmd>(typeof(VirtualDummyCmd));
        RegisterCommand<ICmdStatus>(typeof(VirtualStatusCmd));
        RegisterCommand<ICmdLocoSetSpeed>(typeof(VirtualLocoSetSpeed));
    }
}