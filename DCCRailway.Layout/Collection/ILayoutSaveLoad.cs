namespace DCCRailway.Layout.Collection;

public interface ILayoutSaveLoad {
    string PathName { set; }
    void Save(string pathname);
    void Save();
    void Load();
}