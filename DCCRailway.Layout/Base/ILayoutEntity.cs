using DCCRailway.Layout.Entities;

namespace DCCRailway.Layout.Base;

public interface ILayoutEntity {
    string     Id          { get; set; }
    string     Name        { get; set; }
    string     Description { get; set; }
    Parameters Parameters  { get; set; }
}