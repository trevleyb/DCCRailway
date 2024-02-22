namespace DCCRailway.Layout.Commands.Types;

public interface ICmdAccyOpsProg : ICommand, IAccyCommand  {
    public byte        Value       { get; set; }
}