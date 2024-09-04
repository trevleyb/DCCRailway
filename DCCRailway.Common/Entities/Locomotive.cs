using System.Diagnostics;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using DCCRailway.Common.Entities.Base;
using DCCRailway.Common.Types;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("LOCOMOTIVE={Id}, Name: {Name}, Address: {Address.Address}")]
public partial class Locomotive : LayoutEntityDecoder {
    [ObservableProperty] private DCCDirection      _direction      = DCCDirection.Stop;
    [ObservableProperty] private DCCFunctionBlocks _functionBlocks = new();
    [ObservableProperty] private string            _manufacturer   = "";
    [ObservableProperty] private string            _model;
    [ObservableProperty] private DCCMomentum       _momentum   = new(0);
    [ObservableProperty] private string            _roadName   = "";
    [ObservableProperty] private string            _roadNumber = "";
    [ObservableProperty] private DCCSpeed          _speed      = new(0);

    [JsonInclude] public List<LabelFunction> Labels = [];

    public Locomotive(string id = "") : base(id) {
        if (Labels.Count == 0) {
            Labels.Add(new LabelFunction() { Key = 0, Label = "Lights" });
            Labels.Add(new LabelFunction() { Key = 1, Label = "Bell" });
            Labels.Add(new LabelFunction() { Key = 2, Label = "Horn" });
        }
    }

    /// <summary>
    ///     Represents a locomotive layoutEntity with DCC address, direction, and speed.
    /// </summary>
    public Locomotive(string id, DCCAddress address, DCCDirection direction = DCCDirection.Forward) : this(id) {
        Address     = address;
        Direction   = direction;
        Speed.Value = 0;
    }

    /// <summary>
    ///     Represents a locomotive layoutEntity with DCC address, direction, and speed.
    /// </summary>
    public Locomotive(string id, int address, DCCAddressType type = DCCAddressType.Long, DCCDirection direction = DCCDirection.Stop) : this(id) {
        Address     = new DCCAddress(address, type);
        Direction   = direction;
        Speed.Value = 0;
    }

    [JsonConstructor]
    protected Locomotive() : this("") { }
}