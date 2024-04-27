using System.ComponentModel.DataAnnotations;

namespace DCCRailway.Common.Helpers;

internal sealed class RangeValidationAttribute : ValidationAttribute {
    public RangeValidationAttribute(byte minValue, byte maxValue, string errorMessage) {
        ErrorMessage = errorMessage;
        MinValue     = minValue;
        MaxValue     = maxValue;
    }

    public byte MinValue { get; set; }
    public byte MaxValue { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) => base.IsValid(value, validationContext);
}