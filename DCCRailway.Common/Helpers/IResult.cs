namespace DCCRailway.Common.Helpers;

public interface IResult {
    bool       Success   { get; }
    string     Message   { get; }
    Exception? Exception { get; }
}