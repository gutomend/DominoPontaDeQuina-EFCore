using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Repository.Interfaces;

public interface IJogadorRepository
{
    Task<List<Jogador>> ListarAsync();
    Task<Jogador?> BuscarPorIdAsync(Guid id);
    Task AdicionarAsync(Jogador entidade);
    Task AtualizarAsync(Jogador entidade);
    Task<bool> ExcluirAsync(Guid id);
    Task<List<Jogador>> BuscarPorUsuarioAsync(Guid usuarioId);
    Task<List<Jogador>> PesquisarPorNomeAsync(string termo);
}
