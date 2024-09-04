namespace DCCRailway.Common.Entities.Base;

public interface ILayoutEntity {
    string     Id          { get; set; }
    string     Name        { get; set; }
    string     Description { get; set; }
    Parameters Parameters  { get; set; }

    bool IsDirty     { get; set; }
    bool IsTemporary { get; set; }
}