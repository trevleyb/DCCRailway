using DCCRailway.Configuration.Base;
using DCCRailway.Layout.Types;

namespace DCCRailway.Configuration.Entities;
public class Turnout : ConfigWithDecoder {
    public Turnout() : base(DCCAddressType.Turnout) { }
    
    public Controller Controller { get; set; }
    
    public bool IsManual { get; set; }
    public bool IsReversed { get; set; }
    public bool ResetOnPowerOn { get; set; }
    
    public DCCTurnoutState InitialState { get; set; }
    public DCCTurnoutState CurrentState { get; set; }
    public DCCTurnoutType  Type  { get; set; }
    
}