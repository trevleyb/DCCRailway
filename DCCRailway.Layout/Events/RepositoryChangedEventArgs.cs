namespace DCCRailway.Layout.Events;

public class RepositoryChangedEventArgs(string repository, string id, RepositoryChangeAction action)
    : EventArgs {
    public string                 Id         { get; set; } = id;
    public string                 Repository { get; set; } = repository;
    public RepositoryChangeAction Action     { get; set; } = action;
}

public enum RepositoryChangeAction {
    Add,
    Delete,
    Update
}