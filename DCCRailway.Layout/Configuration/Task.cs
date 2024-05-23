namespace DCCRailway.Layout.Configuration;

[Serializable]
public class Task {
    public Task() { }

    public Task(string name, string taskType, TimeSpan frequency) {
        Name      = name;
        Type      = taskType;
        Frequency = frequency;
    }

    public string     Name       { get; set; }
    public string     Type       { get; set; }
    public TimeSpan   Frequency  { get; set; }
    public Parameters Parameters { get; set; } = [];

    public new string ToString() {
        return $"{Name}:{Type}@{Frequency}'";
    }
}