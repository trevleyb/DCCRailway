using DCCRailway.WiThrottle.Messages;

namespace DCCRailway.WiThrottle;

public class MessageQueue : Queue<IThrottleMsg>
{
    public bool         HasMessages               => this.Any();
    public IThrottleMsg Next                      => HasMessages ? Dequeue() : new MsgIgnore();
    public void         Add(IThrottleMsg message) => Enqueue(message);
}