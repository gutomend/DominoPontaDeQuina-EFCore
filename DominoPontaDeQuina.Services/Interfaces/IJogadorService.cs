using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Services.Interfaces;

public interface IJogadorService
{
    Task<List<Jogador>> ListarAsync();
    Task<List<Jogador>> BuscarPorUsuarioAsync(Guid usuarioId);
    Task<Jogador> CadastrarAsync(Jogador jogador);
}
