using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Services.Interfaces;

namespace DominoPontaDeQuina.Services.Services;

public class ParticipacaoPartidaService : IParticipacaoPartidaService
{
    private readonly IParticipacaoPartidaRepository _participacaoRepository;
    private readonly IPartidaRepository _partidaRepository;
    private readonly IJogadorRepository _jogadorRepository;

    public ParticipacaoPartidaService(
        IParticipacaoPartidaRepository participacaoRepository,
        IPartidaRepository partidaRepository,
        IJogadorRepository jogadorRepository)
    {
        _participacaoRepository = participacaoRepository;
        _partidaRepository = partidaRepository;
        _jogadorRepository = jogadorRepository;
    }

    public Task<List<ParticipacaoPartida>> BuscarPorPartidaAsync(Guid partidaId)
    {
        return _participacaoRepository.BuscarPorPartidaAsync(partidaId);
    }

    public Task<List<ParticipacaoPartida>> BuscarVitoriasAsync(Guid jogadorId)
    {
        return _participacaoRepository.BuscarVitoriasAsync(jogadorId);
    }

    public async Task<ParticipacaoPartida> CadastrarAsync(
        ParticipacaoPartida participacao)
    {
        ArgumentNullException.ThrowIfNull(participacao);

        var partida = await _partidaRepository.BuscarPorIdAsync(
            participacao.PartidaId
        );

        if (partida is null)
        {
            throw new InvalidOperationException(
                "A partida informada nao existe."
            );
        }

        var jogador = await _jogadorRepository.BuscarPorIdAsync(
            participacao.JogadorId
        );

        if (jogador is null)
        {
            throw new InvalidOperationException(
                "O jogador informado nao existe."
            );
        }

        var participantes = await _participacaoRepository.BuscarPorPartidaAsync(
            participacao.PartidaId
        );

        if (participantes.Any(p => p.JogadorId == participacao.JogadorId))
        {
            throw new InvalidOperationException(
                "O jogador ja participa desta partida."
            );
        }

        await _participacaoRepository.AdicionarAsync(participacao);

        return participacao;
    }
}