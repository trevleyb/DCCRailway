using DCCRailway.Throttles.WiThrottle.Helpers;
using DCCRailway.Throttles.WiThrottle.Messages;

namespace DCCRailway.Throttles.WiThrottle;

public class WiThrottleMsgQueue : Queue<IThrottleMsg> {
    public bool HasMessages => this.Any();
    public void Add(IThrottleMsg message) {
        if (string.IsNullOrEmpty(Terminators.RemoveTerminators(message.Message))) return;
        Enqueue(message);
    }
    public IThrottleMsg? Next => HasMessages ? Dequeue() : null;
}