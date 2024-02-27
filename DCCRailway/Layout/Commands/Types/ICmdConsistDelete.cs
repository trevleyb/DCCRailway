using DCCRailway.Layout.Commands.Types.BaseTypes;
using DCCRailway.Layout.Types;

namespace DCCRailway.Layout.Commands.Types;


public interface ICmdConsistDelete : ICommand,IConsistCmd {
    public IDCCAddress Address { get; set; }
}