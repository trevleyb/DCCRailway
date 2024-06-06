using DCCRailway.Common.Helpers;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class LoggerTest {
    [Test]
    public void TestWeHaveALoggerWithoutContext() {
        var logger = LoggerHelper.DebugLogger;
        Assert.That(logger, Is.Not.Null, "Should always have a logger instance via the singleton.");

        Console.WriteLine("Outputting to the Console");
        logger.Debug("Debug information");
        logger.Information("Information Information");
        logger.Error("Error Information");
        logger.Fatal("Fatal Error");
        logger.Verbose("Verbose");
        logger.Warning("Warning");
    }

    [Test]
    public void TestWeHaveALoggerWithContext() {
        var logger = LoggerHelper.DebugLogger;
        Assert.That(logger, Is.Not.Null, "Should always have a logger instance via the singleton.");

        logger.ForContext<LoggerTest>();
        logger.Debug("Debug information");
        logger.Information("Information Information");
        logger.Error("Error Information");
        logger.Fatal("Fatal Error");
        logger.Verbose("Verbose");
        logger.Warning("Warning");
    }
}