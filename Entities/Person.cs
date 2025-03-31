using System.ComponentModel.DataAnnotations;

namespace rinha_de_backend_2023.Entities;

public class Person 
{
    [Required(ErrorMessage = "O apelido é obrigatório.")]
    [StringLength(32)]
    public string Apelido { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string Nascimento { get; set; }

    // TODO - Stack

    public Person(string apelido, string nome, string nascimento) {
        Apelido = apelido;
        Nome = nome;
        Nascimento = nascimento;
    }

    public override string ToString()
    {
        return $"Apelido: {Apelido}, Nome: {Nome}, Nascimento: {Nascimento}";
    }
}
