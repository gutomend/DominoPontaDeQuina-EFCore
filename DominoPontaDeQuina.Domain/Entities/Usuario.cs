using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominoPontaDeQuina.Domain.Entities;

[Table("Usuarios")]
public class Usuario
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
    [StringLength(
        100,
        MinimumLength = 3,
        ErrorMessage = "O nome deve possuir entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    [StringLength(
        150,
        ErrorMessage = "O e-mail deve possuir no máximo 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(
        255,
        ErrorMessage = "O hash da senha deve possuir no máximo 255 caracteres.")]
    public string HashSenha { get; set; } = string.Empty;

    [Required]
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

    public ICollection<Jogador> Jogadores { get; set; }
        = new List<Jogador>();
}