namespace DCCRailway.Common.Entities;

[Serializable]
public class LabelFunction {
    public byte   Key       { get; set; }
    public bool   Momentary { get; set; }
    public string Label     { get; set; }
}