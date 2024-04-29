using System.Net.Sockets;
using DCCRailway.Application.WiThrottle.Commands;
using DCCRailway.Application.WiThrottle.Messages;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleMsgQueue : Queue<IThrottleMsg> {
    public bool HasMessages => this.Any();
    public void Add(IThrottleMsg message) => Enqueue(message);
    public IThrottleMsg? Next => HasMessages ? Dequeue() : null;
}