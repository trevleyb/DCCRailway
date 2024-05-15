using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Virtual.Adapters;

[Adapter("Console", AdapterType.Virtual)]
public class VirtualConsoleAdapter : ConsoleAdapter { }