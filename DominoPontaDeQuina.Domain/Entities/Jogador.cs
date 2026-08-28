using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DominoPontaDeQuina.Domain.Entities;

[Table("Jogadores")]
public class Jogador
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "O nome de exibição é obrigatório.")]
    [StringLength(
        50,
        MinimumLength = 3,
        ErrorMessage = "O nome de exibição deve possuir entre 3 e 50 caracteres.")]
    public string NomeExibicao { get; set; } = string.Empty;

    [Required]
    [ForeignKey(nameof(Usuario))]
    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;

    public ICollection<ParticipacaoJogo> Participacoes { get; set; }
        = new List<ParticipacaoJogo>();
}