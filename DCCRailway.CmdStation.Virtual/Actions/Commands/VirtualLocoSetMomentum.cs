﻿using DCCRailway.CmdStation.Actions;
using DCCRailway.CmdStation.Actions.Commands;
using DCCRailway.CmdStation.Attributes;
using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Virtual.Actions.Commands;

[Command("LocoSetMomentum", "Set the Momentum of a Loco")]
public class VirtualLocoSetMomentum : VirtualCommand, ICmdLocoSetMomentum, ICommand {
    public VirtualLocoSetMomentum() { }

    public VirtualLocoSetMomentum(int address, byte momentum) : this(new DCCAddress(address), new DCCMomentum(momentum)) { }

    public VirtualLocoSetMomentum(DCCAddress address, DCCMomentum momentum) {
        Address  = address;
        Momentum = momentum;
    }

    public DCCAddress Address  { get; set; }
    public DCCMomentum Momentum { get; set; }

    public override string ToString() => $"LOCO MOMENTUM ({Address}={Momentum}";
}