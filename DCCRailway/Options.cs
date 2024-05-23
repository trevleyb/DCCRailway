using CommandLine;
using Serilog.Events;

namespace DCCRailway;

public class Options {
    [Option('l', "log", Required = false,
            HelpText = "Set the level of loging [Verbose,Information,Warning,Error,Debug,Fatal] .",
            Default = LogEventLevel.Information)]
    public LogEventLevel LogLevel { get; set; }

    [Option('c', "console", Required = false, HelpText = "Write logging output to the Console", Default = true)]
    public bool Console { get; set; }

    [Option('x', "clean", Required = false, HelpText = "Do not load existing data. Make a clean config set.",
            Default = false)]
    public bool Clean { get; set; }

    [Option('p', "path", Required = false, HelpText = "Path/Directory containing configuration files.",
            Default = "./Settings")]
    public string Path { get; set; }

    [Option('n', "name", Required = false, HelpText = "Name of the Railway (default will search for *.Settings.json")]
    public string? Name { get; set; }

    [Option('w', "withrottle", Required = false, HelpText = "If specified, then Withrottle server will run",
            Default = true)]
    public bool RunWiThrottle { get; set; }
}