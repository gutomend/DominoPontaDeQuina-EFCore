using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Repository.Interfaces;

public interface IPartidaRepository
{
    Task<List<Partida>> ListarAsync();
    Task<Partida?> BuscarPorIdAsync(Guid id);
    Task AdicionarAsync(Partida entidade);
    Task AtualizarAsync(Partida entidade);
    Task<bool> ExcluirAsync(Guid id);
    Task<List<Partida>> BuscarPorStatusAsync(StatusJogo status);
}
