﻿using DCCRailway.System.Types;

namespace DCCRailway.System.Commands.Interfaces;

public interface ICmdAccyOpsProg : ICommand, IAccyCommand  {
    public IDCCAddress LocoAddress { get; set; }
    public IDCCAddress CVAddress   { get; set; }
    public byte        Value       { get; set; }
}