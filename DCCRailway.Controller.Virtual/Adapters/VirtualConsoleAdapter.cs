using DCCRailway.Controller.Adapters;
using DCCRailway.Controller.Attributes;
using Serilog;

namespace DCCRailway.Controller.Virtual.Adapters;

[Adapter("Console", AdapterType.Virtual)]
public class VirtualConsoleAdapter(ILogger logger) : ConsoleAdapter(logger) { }