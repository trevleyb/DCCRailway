using DCCRailway.Common.Types;
using DCCRailway.System.Commands.Types.Base;

namespace DCCRailway.System.Commands.Types;

public interface ICmdConsistDelete : ICommand, IConsistCmd {
    public IDCCAddress Address { get; set; }
}