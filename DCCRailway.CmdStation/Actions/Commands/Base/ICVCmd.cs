using DCCRailway.Common.Types;

namespace DCCRailway.CmdStation.Commands.Types.Base;

public interface ICVCmd : ICmdAddress{
    public int          CV      { get; }
}