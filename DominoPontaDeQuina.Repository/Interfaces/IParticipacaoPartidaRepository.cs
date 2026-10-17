using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Repository.Interfaces;

public interface IParticipacaoPartidaRepository
{
    Task<List<ParticipacaoPartida>> ListarAsync();
    Task<ParticipacaoPartida?> BuscarPorIdAsync(Guid id);
    Task AdicionarAsync(ParticipacaoPartida entidade);
    Task AtualizarAsync(ParticipacaoPartida entidade);
    Task<bool> ExcluirAsync(Guid id);
    Task<List<ParticipacaoPartida>> BuscarPorPartidaAsync(Guid partidaId);
    Task<List<ParticipacaoPartida>> BuscarVitoriasAsync(Guid jogadorId);
}
