using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace rinha_de_backend_2023.Entities;

public class Person {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    // [Required(ErrorMessage = "O apelido é obrigatório.")]
    [Required]
    [StringLength(32)]
    public string Apelido { get; set; }

    // [Required(ErrorMessage = "O nome é obrigatório.")]
    [Required]
    [StringLength(100)]
    public string Nome { get; set; }

    // [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    [CustomValidation(typeof(Person), nameof(ValidateNascimento))]
    public string Nascimento { get; set; }

   [MaxStringLength(32)]
    public List<string>? Stack { get; set; }

    public Person(string apelido, string nome, string nascimento) {
        Apelido = apelido;
        Nome = nome;
        Nascimento = nascimento;
    }

    public static ValidationResult? ValidateNascimento(string nascimento, ValidationContext context) {
        if (!DateTime.TryParseExact(nascimento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var data)) {
            return new ValidationResult("Data inválida.");
        }
        // if (data > DateTime.Now) {
        //     return new ValidationResult("Data não pode ser futura.");
        // }
        // if (data.Year < 1900) {
        //     return new ValidationResult("Data muito antiga.");
        // }

        return ValidationResult.Success;
    }

   public override string ToString() {
        return $"Id: {Id}, Apelido: {Apelido}, Nome: {Nome}, Nascimento: {Nascimento}, Stack: {(Stack != null ? string.Join(", ", Stack) : "null")}";
    }
}
