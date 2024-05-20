using DCCRailway.WiThrottle.Helpers;
using DCCRailway.WiThrottle.Messages;

namespace DCCRailway.WiThrottle;

public class WiThrottleMsgQueue : Queue<IThrottleMsg> {
    public bool HasMessages => this.Any();

    public IThrottleMsg? Next => HasMessages ? Dequeue() : null;

    public void Add(IThrottleMsg message) {
        if (string.IsNullOrEmpty(Terminators.RemoveTerminators(message.Message))) return;
        Enqueue(message);
    }
}