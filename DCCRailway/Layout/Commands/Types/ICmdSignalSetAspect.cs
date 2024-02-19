namespace DCCRailway.Layout.Commands.Types;

public interface ICmdSignalSetAspect : ICommand,IAccyCommand {
    public byte        Aspect  { get; set; }
    public bool        Off     { set; }
}