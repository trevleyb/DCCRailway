using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using DCCRailway.Common.Entities.Base;

namespace DCCRailway.Common.Entities;

[Serializable]
[DebuggerDisplay("DECODER={Id}, Name: {Name}, Manufacturers: {Manufacturer}")]
public partial class Decoder(string id = "") : LayoutEntity(id) {
    [ObservableProperty] private string? _family       = "";
    [ObservableProperty] private byte    _manufacturer = 00;
    [ObservableProperty] private string? _model        = "";
}