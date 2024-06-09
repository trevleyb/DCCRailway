using DCCRailway.Common.Types;
using DCCRailway.StateManager;
using NUnit.Framework;

namespace DCCRailway.Test;

[TestFixture]
public class TestStateManager {
    [Test]
    public void TestStatesAndAndRetrieve() {
        var lsm = new StateManager.StateManager();
        var so  = new StateObject("T01");
        so.Add(StateType.Turnout, DCCTurnoutState.Thrown);
        Assert.That(so, Is.Not.Null);
        lsm.SetState(so);
        var rs = lsm.GetState("T01");
        Assert.That(rs, Is.Not.Null);
        var sc = rs!.Get<DCCTurnoutState>(StateType.Turnout);
        Assert.That(rs, Is.Not.Null);
        Assert.That(sc, Is.EqualTo(DCCTurnoutState.Thrown));
    }

    [Test]
    public void TestSetAndGetProperties() {
        var stateManager = new StateManager.StateManager();
        var stateObject  = new StateObject("XX");
        stateObject.Add(StateType.Signal, 15);
        stateObject.Add(StateType.Route, DCCActiveState.Active);
        stateManager.SetState(stateObject);
        var getState = stateManager.GetState("XX");
        Assert.That(getState, Is.Not.Null);
        Assert.That((int)getState![StateType.Signal]!, Is.EqualTo(15));
        Assert.That((DCCActiveState)getState[StateType.Route]!, Is.EqualTo(DCCActiveState.Active));

        stateManager.SetState("XX", StateType.Turnout, DCCTurnoutState.Thrown);
        Assert.That(stateManager.GetState<DCCActiveState>("XX", StateType.Route), Is.EqualTo(DCCActiveState.Active));

        var res1 = stateManager.GetState("XX", StateType.Block, "NOT");
        Assert.That(res1, Is.EqualTo("NOT"));

        var res2 = stateManager.GetState("XX", StateType.Aspect, new DCCAddress(0));
        Assert.That(res2.Address, Is.EqualTo(0));
    }

    [Test]
    public void TestStatesAndDelete() {
        var lsm = new StateManager.StateManager();
        var so  = new StateObject("T01");
        so.Add(StateType.Turnout, DCCTurnoutState.Thrown);
        Assert.That(so, Is.Not.Null);
        lsm.SetState(so);
        var rs = lsm.GetState("T01");
        Assert.That(rs, Is.Not.Null);
        var sc = rs!.Get<DCCTurnoutState>(StateType.Turnout);
        Assert.That(rs, Is.Not.Null);
        Assert.That(sc, Is.EqualTo(DCCTurnoutState.Thrown));

        lsm.DeleteState("T01");
        var ss = lsm.GetState("T01");
        Assert.That(ss, Is.Null);
    }

    [Test]
    public void TestStatesUpdate() {
        var lsm = new StateManager.StateManager();
        var so  = new StateObject("T01");
        so.Add(StateType.Turnout, DCCTurnoutState.Thrown);
        Assert.That(so, Is.Not.Null);
        lsm.SetState(so);
        var rs = lsm.GetState("T01");
        Assert.That(rs, Is.Not.Null);
        var sc = rs!.Get<DCCTurnoutState>(StateType.Turnout);
        Assert.That(rs, Is.Not.Null);
        Assert.That(sc, Is.Not.Null);
        Assert.That(sc, Is.EqualTo(DCCTurnoutState.Thrown));

        so.Add(StateType.Signal, 15);
        so.Add(StateType.Route, DCCActiveState.Active);

        lsm.SetState(so);
        Assert.That(lsm, Is.Not.Null);
        Assert.That(lsm.GetState("T01")?.Get<int>(StateType.Signal), Is.EqualTo(15));
        Assert.That(lsm.GetState("T01")?.Get<DCCActiveState>(StateType.Route), Is.EqualTo(DCCActiveState.Active));
        Assert.That(lsm.GetState("T01")?.Get<DCCTurnoutState>(StateType.Turnout), Is.EqualTo(DCCTurnoutState.Thrown));
    }
}