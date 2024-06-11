using System.Text;

namespace DCCRailway.WiThrottle.Helpers;

public static class Terminators {
    public const char Terminator = (char)0x0a;

    public static readonly string[] PossibleTerminators = {
        new([(char)0x0a]),
        new([(char)0x0d]),
        new([(char)0x0a, (char)0x0d]),
        new([(char)0x0d, (char)0x0a])
    };

    public static string ForDisplay(string message) {
        return message.Replace((char)0x0A, '•').Replace((char)0x0d, '•');
    }

    public static string WithTerminator(this string input) {
        return HasTerminator(input) ? input : input + Terminator;
    }

    public static StringBuilder AddTerminator(StringBuilder input) {
        if (input.Length > 0) {
            if (!HasTerminator(input)) {
                input.Append(Terminator);
            }
        }

        return input;
    }

    public static string AddTerminator(string? input) {
        if (!string.IsNullOrEmpty(input)) {
            if (!HasTerminator(input)) {
                return input + Terminator;
            }
        }

        return input ?? "";
    }

    public static bool HasTerminator(StringBuilder input) {
        return HasTerminator(input.ToString());
    }

    public static bool HasTerminator(string input) {
        foreach (var terminator in PossibleTerminators) {
            if (input.Contains(terminator)) {
                return true;
            }
        }

        return false;
    }

    public static List<string> GetMessagesAndLeaveIncomplete(StringBuilder sb) {
        var blocks       = new List<string>();
        var remaining    = sb.ToString();
        var currentBlock = "";

        for (var i = 0; i < remaining.Length; i++) {
            currentBlock += remaining[i];

            if (PossibleTerminators.Any(t => currentBlock.EndsWith(t))) {
                currentBlock = RemoveTerminators(currentBlock);
                if (!string.IsNullOrEmpty(currentBlock)) blocks.Add(currentBlock);
                currentBlock = "";
            }
        }

        // Clear StringBuilder and append the remaining un-terminated block
        sb.Clear();
        sb.Append(currentBlock);
        return blocks;
    }

    public static string RemoveTerminators(string input) {
        foreach (var terminator in PossibleTerminators) input = input.Replace(terminator, "");
        return input;
    }

    /*
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
    */
}