using System.Net.Sockets;
using DCCRailway.Application.WiThrottle.Messages;

namespace DCCRailway.Application.WiThrottle;

public class WiThrottleMsgQueue : Queue<IServerMsg> {
    public bool HasMessages => this.Any();
    public void Add(IServerMsg message) => Enqueue(message);
    public IServerMsg? Next => HasMessages ? Dequeue() : null;
}