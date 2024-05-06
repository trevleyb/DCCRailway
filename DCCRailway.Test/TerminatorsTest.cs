using System.Text;
using DCCRailway.Throttles.WiThrottle.Helpers;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class TerminatorsTest {

    [Test]
    public void TestEachTerminator() {

        var block = "This is a sample string";
        Assert.That(Terminators.HasTerminator(block), Is.False);
        foreach (var terminator in Terminators.PossibleTerminators) {
            Assert.That(Terminators.HasTerminator(block + terminator), Is.True);
        }
    }

    [Test]
    public void TestWeCanGetBackEachBlockSuccessfully() {
        var block = new StringBuilder("This is a sample string");
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Any(),Is.False);
        Assert.That(block.Length,Is.EqualTo("This is a sample string".Length));

        block = Terminators.AddTerminator(block);
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Any(),Is.True);
        Assert.That(block.Length,Is.EqualTo(0));

        block = new StringBuilder("Message1").Append(Terminators.Terminator);
        block.Append("Message2").Append(Terminators.Terminator);
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Count,Is.EqualTo(2));
        Assert.That(block.Length,Is.EqualTo(0));

        block = new StringBuilder("Message1").Append(Terminators.Terminator);
        block.Append("Message2").Append(Terminators.Terminator);
        block.Append("Message3").Append(Terminators.Terminator);
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Count,Is.EqualTo(3));
        Assert.That(block.Length,Is.EqualTo(0));

        block = new StringBuilder("Message1").Append(Terminators.Terminator);
        block.Append("Message2").Append(Terminators.Terminator);
        block.Append("Message3");
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Count,Is.EqualTo(2));
        Assert.That(block.ToString(),Is.EqualTo("Message3"));
    }

    [Test]
    public void TestMultipleTerminators() {
        var rnd = new Random();
        var block = new StringBuilder();
        block.Append("Message1").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message2").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message3").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message4").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message5").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message6").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message7").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message8").Append(Terminators.PossibleTerminators[rnd.Next(0, Terminators.PossibleTerminators.Length)]);
        block.Append("Message9");
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Count, Is.EqualTo(8));
        Assert.That(block.ToString(), Is.EqualTo("Message9"));
        block.Append((char)0x0a);
        block.Append((char)0x0d);
        Assert.That(Terminators.GetMessagesAndLeaveIncomplete(block).Count, Is.EqualTo(1));
        Assert.That(block.ToString(), Is.EqualTo(""));
    }
}