﻿using DCCRailway.System.Adapters.Events;
using DCCRailway.System.Attributes;
using DCCRailway.System.Commands.Interfaces;
using DCCRailway.System.Commands.Results;
using DCCRailway.System.Exceptions;
using DCCRailway.System.NCE.Adapters;
using DCCRailway.System.NCE.Commands;
using DCCRailway.System.Types;
using DCCRailway.System.Utilities;

namespace DCCRailway.System.NCE;

[System("NCEPowerCab", "North Coast Engineering (NCE)", "PowerCab", "1.65")]
public class NcePowerCab : System, ISystem {
    public override IDCCAddress CreateAddress() => new DCCAddress();

    public override IDCCAddress CreateAddress(int address, DCCAddressType type = DCCAddressType.Long) => new DCCAddress(address, type);

    protected override void RegisterAdapters() {
        ClearAdapters();
        RegisterAdapter<NCESerial>();
        RegisterAdapter<NCEUSBSerial>();
        RegisterAdapter<NCEVirtualAdapter>();
    }

    protected override void RegisterCommands() {
        // With the NCE Devices, it depends on the ADAPTER being used
        // as to what commands it will support
        // -----------------------------------------------------------------
        // The NCE USB Interface doesn't support all JMRI features and functions.
        // Some of the restrictions are based on the type of system the USB Adapter is connected to.
        // The USB version 6.* can't get information from AIUs, so they can't be used to get feedback from the layout.
        // The USB 7.* version when connected to a system with the 1.65 or higher firmware (PowerCab, SB5, Twin)
        // the AIU cards can be used, but with restricted cab numbers as in the system manual.
        // The turnout feedback mode MONITORING isn't available when using a USB, and the Clock functions
        // are also not available.
        // The USB when connected to a Power Pro system doesn't support any type of loco programming,
        // and when connected to a SB3 only operation mode (no program track) is available for loco programming.
        // -----------------------------------------------------------------
        ClearCommands();

        if (Adapter != null) {
            RegisterCommand<IDummyCmd>(typeof(NCEDummyCmd));
            RegisterCommand<ICmdStatus>(typeof(NCEStatusCmd));

            RegisterCommand<ICmdTrackMain>(typeof(NCESetMainTrk));
            RegisterCommand<ICmdTrackProg>(typeof(NCESetProgTrk));
            RegisterCommand<ICmdPowerSetOn>(typeof(NCESetMainTrk));
            RegisterCommand<ICmdPowerSetOff>(typeof(NCESetProgTrk));

            RegisterCommand<ICmdSensorGetState>(typeof(NCESensorGetState));
            RegisterCommand<ICmdSignalSetAspect>(typeof(NCESignalSetAspect));
            RegisterCommand<ICmdAccySetState>(typeof(NCEAccySetState));

            RegisterCommand<ICmdLocoSetFunctions>(typeof(NCELocoSetFunctions));
            RegisterCommand<ICmdLocoSetSpeed>(typeof(NCELocoSetSpeed));
            RegisterCommand<ICmdLocoSetSpeedSteps>(typeof(NCELocoSetSpeedSteps));
            RegisterCommand<ICmdLocoSetMomentum>(typeof(NCELocoSetMomentum));
            RegisterCommand<ICmdLocoStop>(typeof(NCELocoStop));

            RegisterCommand<ICmdAccyOpsProg>(typeof(NCEAccyOpsProg));
            RegisterCommand<ICmdLocoOpsProg>(typeof(NCELocoOpsProg));

            RegisterCommand<ICmdConsistCreate>(typeof(NCEConsistCreate));
            RegisterCommand<ICmdConsistKill>(typeof(NCEConsistKill));
            RegisterCommand<ICmdConsistAdd>(typeof(NCEConsistAdd));
            RegisterCommand<ICmdConsistDelete>(typeof(NCEConsistDelete));

            RegisterCommand<ICmdCVRead>(typeof(NCECVRead));
            RegisterCommand<ICmdCVWrite>(typeof(NCECVWrite));

            RegisterCommand<ICmdMacroRun>(typeof(NCEMacroRun));

            if (Adapter is NCESerial) {
                RegisterCommand<ICmdClockSet>(typeof(NCESetClock));
                RegisterCommand<ICmdClockRead>(typeof(NCEReadClock));
                RegisterCommand<ICmdClockStart>(typeof(NCEStartClock));
                RegisterCommand<ICmdClockStop>(typeof(NCEStopClock));
            } else if (Adapter is NCEUSBSerial) {
                if (CreateCommand<ICmdStatus>() is NCEStatusCmd statusCmd && statusCmd.Execute(Adapter) is IResultStatus status) {
                    switch (status.Version) {
                    case "6.x.x": break; // Cannot get AIU Information
                    case "7.3.0": break;
                    case "7.3.1": break;
                    case "7.3.2": break;
                    case "7.3.3": break;
                    case "7.3.4": break;
                    case "7.3.5": break;
                    case "7.3.6": break;
                    case "7.3.7": break;
                    }
                } else {
                    throw new AdapterException(Adapter, ":Unable to communicate with the Command Station.");
                }
            }
        }
    }
}