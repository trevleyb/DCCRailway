﻿using DCCRailway.Common.Types;
using DCCRailway.Controller.Attributes;
using DCCRailway.Controller.Controllers;
using Serilog;

namespace DCCRailway.Controller.NCE;

[Controller("NCETwinCab", "North Coast Engineering (NCE)", "TwinCab", "1.1")]
public class NceTwinCab(ILogger logger) : CommandStation(logger), ICommandStation {
    public override DCCAddress CreateAddress() {
        return new DCCAddress();
    }

    public override DCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) {
        return new DCCAddress(address, type);
    }
}