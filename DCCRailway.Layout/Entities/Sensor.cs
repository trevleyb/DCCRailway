using DCCRailway.Common.Types;
using DCCRailway.Layout.Entities.Base;

namespace DCCRailway.Layout.Entities;

public class Sensor() : ConfigWithDecoder(DCCAddressType.Sensor) {
    private Controller _controller;

    public Controller Controller {
        get => _controller;
        set => SetField(ref _controller, value);
    }
}