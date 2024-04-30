using System.Text;
using DCCRailway.Application.WiThrottle.Helpers;
using Microsoft.Extensions.Primitives;

namespace DCCRailway.Application.WiThrottle.Messages;

public class MsgRosterList(WiThrottleServerOptions options) : ThrottleMsg, IThrottleMsg {
    public string Message {
        get {
            var locos = options?.Config?.Locomotives.Values;
            if (locos is null || !locos.Any()) return "RL0";

            var message = new StringBuilder();
            message.Append($"RL{locos.Count}");
            foreach (var loco in locos) {
                message.Append("]\\["); // Separator
                message.Append(loco.Name.RemoveWiThrottleSeparators());
                message.Append("}|{");
                message.Append(loco.Address);
                message.Append("}|{");
                message.Append(loco.Address.IsLong ? "L" : "S");
            }
            message.Append(Terminators.Terminator);
            return message.ToString();
        }
    }
    public override string ToString() => $"MSG:RosterList=>{NoTerminators(Message)}";
}

/*
RL0
RL2]\[RGS 41}|{41}|{L]\[Test Loco}|{1234}|{L
RL3]\[D&RGW 341}|{3}|{S]\[RGS 41}|{41}|{L]\[Test Loco}|{1234}|{L
*/