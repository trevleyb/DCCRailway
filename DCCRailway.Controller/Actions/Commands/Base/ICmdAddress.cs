using DCCRailway.Common.Types;

namespace DCCRailway.Controller.Actions.Commands.Base;

public interface ICmdAddress {
    public DCCAddress Address { get; set; }
}