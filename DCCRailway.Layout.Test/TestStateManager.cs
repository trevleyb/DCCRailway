using DCCRailway.Common.State;
using DCCRailway.Common.Types;
using DCCRailway.Layout.Layout.Entities;
using DCCRailway.Layout.StateManager;

namespace DCCRailway.Layout.Test;

[TestFixture]
public class TestStateManager {

    [Test]
    public void TestStatesAndAndRetrieve() {

        var lsm = new LayoutStateManager();
        var so = new StateObject("T01");
        so.Add("Turnout", DCCTurnoutState.Thrown);
        lsm.SetState(so);
        var rs = lsm.GetState("T01");
        var sc = rs.Get<DCCTurnoutState>("Turnout");
        Assert.That(rs, Is.Not.Null);
        Assert.That(sc, Is.EqualTo(DCCTurnoutState.Thrown));
    }

    [Test]
    public void TestStatesAndDelete() {

        var lsm = new LayoutStateManager();
        var so = new StateObject("T01");
        so.Add("Turnout", DCCTurnoutState.Thrown);
        lsm.SetState(so);
        var rs = lsm.GetState("T01");
        var sc = rs.Get<DCCTurnoutState>("Turnout");
        Assert.That(rs, Is.Not.Null);
        Assert.That(sc, Is.EqualTo(DCCTurnoutState.Thrown));

        lsm.DeleteState("T01");
        var ss = lsm.GetState("T01");
        Assert.That(ss, Is.Null);
    }

    [Test]
    public void TestStatesUpdate() {

        var lsm = new LayoutStateManager();
        var so = new StateObject("T01");
        so.Add("Turnout", DCCTurnoutState.Thrown);
        lsm.SetState(so);
        var rs = lsm.GetState("T01");
        var sc = rs.Get<DCCTurnoutState>("Turnout");
        Assert.That(rs, Is.Not.Null);
        Assert.That(sc, Is.EqualTo(DCCTurnoutState.Thrown));

        so.Add("Signal", (int) 15);
        so.Add("Route", DCCActiveState.Active);

        lsm.SetState(so);
        Assert.That(lsm.GetState("T01").Get<int>("Signal"),Is.EqualTo(15));
        Assert.That(lsm.GetState("T01").Get<DCCActiveState>("Route"),Is.EqualTo(DCCActiveState.Active));
        Assert.That(lsm.GetState("T01").Get<DCCTurnoutState>("Turnout"),Is.EqualTo(DCCTurnoutState.Thrown));

    }

}