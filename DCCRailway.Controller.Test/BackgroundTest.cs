using System.Text.Json;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Virtual.Adapters;

namespace DCCRailway.Controller.Test;

[TestFixture]
public class BackgroundTest {
    [Test]
    public void TestDeserializationofTimeSpan() {
        var timespan1 = new TimeSpan(0, 1, 1, 1, 1);
        var jsonTime1 = JsonSerializer.Serialize(timespan1);
        Console.WriteLine(jsonTime1);

        var timespan2 = new TimeSpan(0, 0, 0, 1);
        var jsonTime2 = JsonSerializer.Serialize(timespan2);
        Console.WriteLine(jsonTime2);

        var timespan3 = new TimeSpan(0, 0, 0, 0, 500);
        var jsonTime3 = JsonSerializer.Serialize(timespan3);
        Console.WriteLine(jsonTime3);
    }

    [Test]
    public void TestThatWeCanLoadVirtualCommandStation() {
        var workStarted  = false;
        var workFinished = false;
        var workHappened = 0;

        var virtualSystem = new CommandStationFactory().Find("Virtual")?.Create(new VirtualConsoleAdapter());
        Assert.That(virtualSystem, Is.Not.Null, "Should have created a Virtual System with a Adapter");
        var tasks = virtualSystem.Tasks;
        Assert.That(tasks.Count, Is.GreaterThan(0), "Should return i the Virtual System at least 1 background task");
        var taskInstance = virtualSystem.CreateTask("VirtualDummyTask");
        Assert.That(taskInstance, Is.Not.Null, "Should have a task instance");

        // This will run the task on the background
        taskInstance.WorkStarted    += (sender, args) => workStarted  = true;
        taskInstance.WorkFinished   += (sender, args) => workFinished = true;
        taskInstance.WorkInProgress += (sender, args) => workHappened++;
        taskInstance.Start();
        Thread.Sleep(taskInstance.Milliseconds * 5); // Go to sleep for 5 times the duration of the process
        taskInstance.Stop();
        Thread.Sleep(500); // Give the background process a change to end before we test it ended

        Assert.That(workStarted, Is.True);
        Assert.That(workFinished, Is.True);
        Assert.That(workHappened, Is.GreaterThan(0));
    }
}