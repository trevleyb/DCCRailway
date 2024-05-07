using DCCRailway.Railway.Throttles.WiThrottle.Helpers;
using DCCRailway.Railway.Throttles.WiThrottle.Messages;

namespace DCCRailway.Railway.Throttles.WiThrottle;

public class WiThrottleMsgQueue : Queue<IThrottleMsg> {
    public bool HasMessages => this.Any();
    public void Add(IThrottleMsg message) {
        if (string.IsNullOrEmpty(Terminators.RemoveTerminators(message.Message))) return;
        Enqueue(message);
    }
    public IThrottleMsg? Next => HasMessages ? Dequeue() : null;
}