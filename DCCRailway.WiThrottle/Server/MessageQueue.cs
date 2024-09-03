using DCCRailway.WiThrottle.Server.Messages;

namespace DCCRailway.WiThrottle.Server;

public class MessageQueue : Queue<IThrottleMsg> {
    public bool         HasMessages => this.Any();
    public IThrottleMsg Next        => HasMessages ? Dequeue() : new MsgIgnore();

    public void Add(IThrottleMsg message) {
        Enqueue(message);
    }
}