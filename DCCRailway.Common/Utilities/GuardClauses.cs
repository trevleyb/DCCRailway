namespace DCCRailway.Common.Utilities;

public static class GuardClauses {
    public static void IsNotNull(object? argumentValue, string argumentName) {
        if (argumentValue == null) throw new ArgumentNullException(argumentName);
    }

    public static void IsNotNullOrEmpty(string? argumentValue, string argumentName) {
        if (string.IsNullOrEmpty(argumentValue)) throw new ArgumentNullException(argumentName);
    }

    public static void IsNotZero(int argumentValue, string argumentName) {
        if (argumentValue == 0)

            //C# 7.0 syntax: $"Argument '{argumentName}' cannot be zero"
            throw new ArgumentException("Argument '" + argumentName + "' cannot be zero", argumentName);
    }

    public static void IsLessThan(int maxValue, int argumentValue, string argumentName) {
        if (argumentValue >= maxValue)

            //C# 7.0 syntax: $"Argument '{argumentName}' cannot exceed '{maxValue}'"
            throw new ArgumentException("Argument '" + argumentName + "' cannot exceed " + maxValue, argumentName);
    }

    public static void IsMoreThan(int minValue, int argumentValue, string argumentName) {
        if (argumentValue <= minValue)

            //C# 7.0 syntax: $"Argument '{argumentName}' cannot be lower than '{minValue}'"
            throw new ArgumentException("Argument '" + argumentName + "'  cannot be lower than " + minValue, argumentName);
    }
}