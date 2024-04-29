using System.Text;

namespace DCCRailway.Application.WiThrottle.Helpers;

public static class Terminators {

    public const char Terminator = (char)0x0a;

    public static string AddTerminator(StringBuilder input) => AddTerminator(input.ToString());

    public static string AddTerminator(string? input) {
        if (!string.IsNullOrEmpty(input)) {
            if (!HasTerminator(input)) return input + Terminator;
        }
        return input ?? "";
    }

    public static bool HasTerminator(StringBuilder input) => HasTerminator(input.ToString());

    public static bool HasTerminator(string input) {
        if (input.Contains(new string([(char)0x0a, (char)0x0d])) ||
            input.Contains(new string([(char)0x0d, (char)0x0a])) ||
            input.Contains((char)0x0a) ||
            input.Contains((char)0x0d)) {
            return true;
        }
        return false;
    }

    public static string[] RemoveTerminators(StringBuilder input) => RemoveTerminators(input.ToString());

    public static string[] RemoveTerminators(string input) {
        var separators = new byte[][] {
            new byte[] { 0x0a },
            new byte[] { 0x0d },
            new byte[] { 0x0a, 0x0d },
            new byte[] { 0x0d, 0x0a },
        };

        Func<byte[], byte[], bool> equalFunc = (byte[] first, byte[] second) =>
        {
            if (first.Length != second.Length) return false;

            for (int i = 0; i < first.Length; ++i)
                if (first[i] != second[i])
                    return false;
            return true;
        };

        byte[] inputBytes = Encoding.Default.GetBytes(input);
        List<string> splitStrings = new List<string>();
        int startIndex = 0;

        for (int i = 0; i < inputBytes.Length; ++i) {
            foreach (var separator in separators) {
                int length = separator.Length;
                if (i + length > inputBytes.Length)
                    continue;

                if (equalFunc(separator, inputBytes.Skip(i).Take(length).ToArray())) {
                    splitStrings.Add(Encoding.Default.GetString(inputBytes, startIndex, i - startIndex));
                    startIndex = i + length;
                    i = startIndex - 1;
                    break;
                }
            }
        }

        splitStrings.Add(Encoding.Default.GetString(inputBytes, startIndex, inputBytes.Length - startIndex));
        return splitStrings.ToArray();
    }

}