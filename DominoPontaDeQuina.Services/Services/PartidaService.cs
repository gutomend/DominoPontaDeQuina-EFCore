using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Services.Interfaces;

namespace DominoPontaDeQuina.Services.Services;

public class PartidaService : IPartidaService
{
    private readonly IPartidaRepository _partidaRepository;

    public PartidaService(IPartidaRepository partidaRepository)
    {
        _partidaRepository = partidaRepository;
    }

    public Task<List<Partida>> ListarAsync()
    {
        return _partidaRepository.ListarAsync();
    }

    public Task<Partida?> BuscarPorIdAsync(Guid id)
    {
        return _partidaRepository.BuscarPorIdAsync(id);
    }

    public Task<List<Partida>> BuscarPorStatusAsync(StatusJogo status)
    {
        return _partidaRepository.BuscarPorStatusAsync(status);
    }

    public async Task<Partida> CadastrarAsync(Partida partida)
    {
        ArgumentNullException.ThrowIfNull(partida);

        await _partidaRepository.AdicionarAsync(partida);

        return partida;
    }
}