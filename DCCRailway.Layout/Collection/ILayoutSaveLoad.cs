namespace DCCRailway.Layout.Collection;

public interface ILayoutSaveLoad {
    void Save(string pathname);
    void Save();
    void Load();
}