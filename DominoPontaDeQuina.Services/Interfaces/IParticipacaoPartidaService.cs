using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Services.Interfaces;

public interface IParticipacaoPartidaService
{
    Task<List<ParticipacaoPartida>> BuscarPorPartidaAsync(Guid partidaId);
    Task<List<ParticipacaoPartida>> BuscarVitoriasAsync(Guid jogadorId);
    Task<ParticipacaoPartida> CadastrarAsync(ParticipacaoPartida participacao);
}
