using System.Text;
using DCCRailway.Common.Types;

namespace DCCRailway.WiThrottle.Server;

// Tracks the data for a loco that is assigned to a connection/throttle. 
// Also tracks some state data that the WiThrottle protocol needs to know
// while running such as current speed or direction of a loco
// ----------------------------------------------------------------------
public class AssignedLoco {
    public AssignedLoco() {
        // Normally functions are Momentary, unless set to Latching other than lights and bell
        // -----------------------------------------------------------------------------------
        SetMomentaryState(0, MomentaryStateEnum.Latching);
        SetMomentaryState(1, MomentaryStateEnum.Latching);
        for (byte function = 2; function < 128; function++) SetMomentaryState(function, MomentaryStateEnum.Momentary);
        for (byte function = 0; function < 128; function++) SetFunctionState(function, FunctionStateEnum.Off);
    }

    public Connection           Connection     { get; set; }
    public char                 Group          { get; set; }
    public DCCAddress           Address        { get; set; }
    public DCCSpeed             Speed          { get; set; }
    public DCCDirection         Direction      { get; set; }
    public DCCProtocol          SpeedSteps     { get; set; }
    public MomentaryStateEnum[] MomentaryState { get; } = new MomentaryStateEnum[128];
    public FunctionStateEnum[]  FunctionState  { get; } = new FunctionStateEnum[128];

    public string MomentaryStates {
        get {
            var sb = new StringBuilder();
            foreach (var state in MomentaryState) sb.Append(state == MomentaryStateEnum.Momentary ? "1" : "0");
            return sb.ToString();
        }
    }

    public string FunctionStates {
        get {
            var sb = new StringBuilder();
            foreach (var state in FunctionState) sb.Append(state == FunctionStateEnum.On ? "1" : "0");
            return sb.ToString();
        }
    }

    public void SetFunctionState(byte function, FunctionStateEnum stateEnum) {
        if (function > 127) return;
        FunctionState[function] = stateEnum;
    }

    public FunctionStateEnum GetFunctionState(byte function) {
        if (function > 127) return FunctionStateEnum.Off;
        return FunctionState[function];
    }

    public void SetMomentaryState(byte function, MomentaryStateEnum stateEnum) {
        if (function > 127) return;
        MomentaryState[function] = stateEnum;
    }

    public MomentaryStateEnum GetMomentaryState(byte function) {
        if (function > 127) return MomentaryStateEnum.Momentary;
        return MomentaryState[function];
    }
}

public enum MomentaryStateEnum {
    Latching  = 0,
    Momentary = 1
}

public enum FunctionStateEnum {
    Off = 0,
    On  = 1
}