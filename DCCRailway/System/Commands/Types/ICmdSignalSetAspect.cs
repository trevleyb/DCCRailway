﻿namespace DCCRailway.System.Commands.Types;

public interface ICmdSignalSetAspect : ICommand,IAccyCommand {
    public byte        Aspect  { get; set; }
    public bool        Off     { set; }
}