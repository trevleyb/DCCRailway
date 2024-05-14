using System.Collections;
using System.Diagnostics;
using System.Text.Json.Serialization;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Base;

namespace DCCRailway.Layout.Entities;

[Serializable]
[DebuggerDisplay("LOCOMOTIVE={Id}, Name: {Name}, Address: {Address.Address}")]
public class Locomotive : LayoutEntityDecoder {

    private string            _type             = "";
    private string            _roadName         = "";
    private string            _roadNumber       = "";
    private string            _manufacturer     = "";
    private string            _model;
    private DCCMomentum       _momentum         = new DCCMomentum(0);
    private DCCSpeed          _speed            = new(0);
    private DCCDirection      _direction        = DCCDirection.Stop;
    private DCCFunctionBlocks _functionBlocks   = new();

    /// <summary>
    /// Represents a locomotive layoutEntity with DCC address, direction, and speed.
    /// </summary>
    public Locomotive(string id, DCCAddress address, DCCDirection direction = DCCDirection.Forward) : this(id) {
        Address     = address;
        Direction   = direction;
        Speed.Value = 0;
    }

    /// <summary>
    /// Represents a locomotive layoutEntity with DCC address, direction, and speed.
    /// </summary>
    public Locomotive(string id, int address, DCCAddressType type = DCCAddressType.Long, DCCDirection direction = DCCDirection.Stop) : this(id) {
        Address     = new DCCAddress(address, type);
        Direction   = direction;
        Speed.Value = 0;
    }

    [JsonConstructor]
    protected Locomotive() : this("") { }

    public Locomotive(string id = "") : base(id) {
        AddressType = DCCAddressType.Long;
    }

    // Hate backing fields but need them for INotifyPropertyChanged Events
    // ----------------------------------------------------------------------------
    public string Type              { get => _type;         set => SetField(ref _type, value); }
    public string RoadName          { get => _roadName;     set => SetField(ref _roadName, value); }
    public string RoadNumber        { get => _roadNumber;   set => SetField(ref _roadNumber, value); }
    public string Manufacturer      { get => _manufacturer; set => SetField(ref _manufacturer, value); }
    public string Model             { get => _model;        set => SetField(ref _model, value); }

    public DCCMomentum Momentum             { get => _momentum;         set => SetField(ref _momentum, value); }
    public DCCSpeed Speed                   { get => _speed;            set => SetField(ref _speed, value); }
    public DCCDirection Direction           { get => _direction;        set => SetField(ref _direction, value); }
    public DCCFunctionBlocks FunctionBlocks { get => _functionBlocks;   set => SetField(ref _functionBlocks, value); }

    public new string                       ToString() => $"{Name}";
    public IEnumerator GetEnumerator() {
        throw new NotImplementedException();
    }
}