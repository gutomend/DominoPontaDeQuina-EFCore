using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Services.Interfaces;

public interface IPartidaService
{
    Task<List<Partida>> ListarAsync();
    Task<Partida?> BuscarPorIdAsync(Guid id);
    Task<List<Partida>> BuscarPorStatusAsync(StatusJogo status);
    Task<Partida> CadastrarAsync(Partida partida);
}
