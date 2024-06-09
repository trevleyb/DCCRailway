using System.Text.Json;
using DCCRailway.Common.Helpers;
using DCCRailway.Controller.Controllers;
using DCCRailway.Controller.Tasks.Events;
using DCCRailway.Controller.Virtual.Adapters;

namespace DCCRailway.Controller.Test;

[TestFixture]
public class BackgroundTest {
    bool workStarted  = false;
    bool workFinished = false;
    int  workHappened = 0;

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
        workStarted  = false;
        workFinished = false;
        workHappened = 0;

        var virtualSystem = new CommandStationFactory(LoggerHelper.DebugLogger).Find("Virtual")?.Create(new VirtualConsoleAdapter(LoggerHelper.DebugLogger));
        Assert.That(virtualSystem, Is.Not.Null, "Should have created a Virtual System with a Adapter");
        var tasks = virtualSystem.Tasks;
        Assert.That(tasks.Count, Is.GreaterThan(0), "Should return i the Virtual System at least 1 background task");
        var taskInstance = virtualSystem.CreateTask("VirtualDummyTask");
        Assert.That(taskInstance, Is.Not.Null, "Should have a task instance");

        // This will run the task on the background
        taskInstance.TaskEvent += TaskInstanceOnTaskEvent;
        taskInstance.Start();
        Thread.Sleep(taskInstance.Milliseconds * 5); // Go to sleep for 5 times the duration of the process
        taskInstance.Stop();
        Thread.Sleep(500); // Give the background process a change to end before we test it ended

        Assert.That(workStarted, Is.True);
        Assert.That(workFinished, Is.True);
        Assert.That(workHappened, Is.GreaterThan(0));
    }

    private void TaskInstanceOnTaskEvent(object? sender, ITaskEvent e) {
        if (e is TaskStartEvent) workStarted      = true;
        else if (e is TaskStopEvent) workFinished = true;
        else workHappened++;
    }
}