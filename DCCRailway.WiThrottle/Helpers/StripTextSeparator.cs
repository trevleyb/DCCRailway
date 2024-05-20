namespace DCCRailway.WiThrottle.Helpers;

public static class StripTextSeparator {
    public static string RemoveWiThrottleSeparators(this string message) => RemoveAll(message, new[] { "]\\[", "}|{", "{", "}", "[", "]" });

    private static string RemoveAll(string message, string[] stringsToRemove) {
        foreach (var separator in stringsToRemove) {
            message = message.Replace(separator, "");
        }
        return message;
    }
}