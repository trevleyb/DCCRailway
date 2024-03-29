﻿using DCCRailway.Common.Types;
using DCCRailway.System.Attributes;
using DCCRailway.System.Controllers;

namespace DCCRailway.System.Sprog;

[Controller("Sprog II", "DCCSystems", "Sprog II")]
public class Sprog2 : Controller, IController {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<SprogVirtualAdapter>();
    }

    protected override void RegisterCommands() {
        //RegisterCommand<IDummyCmd> (typeof (Commands.VirtualDummy));
        //RegisterCommand<ICmdStatus> (typeof (Commands.VirtualStatus));
    }
}