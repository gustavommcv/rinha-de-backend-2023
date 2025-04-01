using System.ComponentModel.DataAnnotations;

public class MaxStringLength : ValidationAttribute {
    private readonly int _maxLength;
    
    public MaxStringLength(int maxLength) { _maxLength = maxLength; }

    protected override ValidationResult? IsValid(object? value, ValidationContext context) {
        if (value is IEnumerable<string> items) {
            foreach (var item in items) {
                if (item?.Length > _maxLength) {
                    return new ValidationResult($"Cada item da stack deve ter no máximo {_maxLength} caracteres.");
                }
            }
        }
        return ValidationResult.Success;
    }
}
