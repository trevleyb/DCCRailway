using DCCRailway.Common.Helpers;
using NUnit.Framework;
using Serilog;

namespace DCCRailway.Test;

[TestFixture]
public class LoggerTest {

    [TestCase]
    public void TestWeHaveALoggerWithoutContext() {

        var logger = Logger.Instance;
        Assert.That(logger,Is.Not.Null, "Should always have a logger instance via the singleton.");

        Console.WriteLine("Outputting to the Console");
        Logger.Log.Debug("Debug information");
        Logger.Log.Information("Information Information");
        Logger.Log.Error("Error Information");
        Logger.Log.Fatal("Fatal Error");
        Logger.Log.Verbose("Verbose");
        Logger.Log.Warning("Warning");
    }

    [TestCase]
    public void TestWeHaveALoggerWithContext() {

        var logger = Logger.Instance;
        Assert.That(logger,Is.Not.Null, "Should always have a logger instance via the singleton.");

        Logger.Instance.ForContext<LoggerTest>();
        Logger.Log.Debug("Debug information");
        Logger.Log.Information("Information Information");
        Logger.Log.Error("Error Information");
        Logger.Log.Fatal("Fatal Error");
        Logger.Log.Verbose("Verbose");
        Logger.Log.Warning("Warning");
    }

    [TestCase]
    public void TestWeHaveALoggerOfAType() {

        var logger = Logger.LogContext<LoggerTest>();
        Assert.That(logger,Is.Not.Null, "Should always have a logger instance via the singleton.");

        Logger.Log.Debug("Debug information");
        Logger.Log.Information("Information Information");
        Logger.Log.Error("Error Information");
        Logger.Log.Fatal("Fatal Error");
        Logger.Log.Verbose("Verbose");
        Logger.Log.Warning("Warning");


    }

}