namespace DCCRailway.Controller.Actions.Commands.Base;

public interface ICVCmd : ICmdAddress {
    public int CV { get; }
}