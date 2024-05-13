using System.Globalization;
using System.IO.Ports;

namespace DCCRailway.Railway.Configuration.Entities;

[Serializable]
public class DCCTask {

    public DCCTask() { }

    public DCCTask(string name, string taskType, TimeSpan frequency) {
        Name = name;
        Type = taskType;
        Frequency = frequency;
    }

    public string       Name    { get; set; }
    public string       Type { get; set; }
    public TimeSpan     Frequency { get; set; }
    public Parameters   Parameters { get; set; } = [];
    public new string   ToString() => $"{Name}:{Type}@{Frequency}'";
}