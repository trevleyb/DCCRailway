namespace DCCRailway.LayoutEventUpdater;

public class LayoutEventLogger {

    private void Write(Type eventType, bool isError, string description) {
        Console.WriteLine($"{eventType.ToString()}\t : {(isError ? "Error" : " ")}\t : {description}");
    }

    public void Error(Type eventType, string error) {
        Write(eventType, true, error);
    }

    public void Event(Type eventType, string description) {
        Write(eventType, false, description);
    }

}