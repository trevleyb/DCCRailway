using System.Text;

namespace DCCRailway.Common.Helpers;

public static class StringBuilderExtensions {
    public static StringBuilder AppendLineIfNotNull(this StringBuilder sb, string? value) {
        if (value != null) sb.AppendLine(value);
        return sb;
    }
}