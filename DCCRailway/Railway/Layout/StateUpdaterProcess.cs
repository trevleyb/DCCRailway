using DCCRailway.Controller.Actions;
using DCCRailway.Controller.Actions.Results;
using DCCRailway.Controller.Attributes;

namespace DCCRailway.Railway.Layout;

public abstract class StateUpdaterProcess(ICmdResult result) : IStateUpdaterProcess {
    public string?    Name    => Command?.AttributeInfo()?.Name ?? "Unknown Command";
    public ICmdResult Result  { get; } = result;
    public ICommand?  Command => Result.Command ?? null;
    public byte[]?    Data    => Result.Data;

    public abstract bool Process();

    private void Write(bool isError, string description) {
        Console.WriteLine($"{Name}\t : {(isError ? "Error" : " ")}\t : {description}");
    }

    public void Error(string error) {
        Write(true, error);
    }

    public void Event(string description) {
        Write(false, description);
    }
}