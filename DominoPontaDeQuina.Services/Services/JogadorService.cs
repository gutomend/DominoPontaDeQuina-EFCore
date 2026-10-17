using System.ComponentModel.DataAnnotations;
using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Services.Interfaces;

namespace DominoPontaDeQuina.Services.Services;

public class JogadorService : IJogadorService
{
    private readonly IJogadorRepository _jogadorRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public JogadorService(
        IJogadorRepository jogadorRepository,
        IUsuarioRepository usuarioRepository)
    {
        _jogadorRepository = jogadorRepository;
        _usuarioRepository = usuarioRepository;
    }

    public Task<List<Jogador>> ListarAsync()
    {
        return _jogadorRepository.ListarAsync();
    }

    public Task<List<Jogador>> BuscarPorUsuarioAsync(Guid usuarioId)
    {
        return _jogadorRepository.BuscarPorUsuarioAsync(usuarioId);
    }

    public async Task<Jogador> CadastrarAsync(Jogador jogador)
    {
        ArgumentNullException.ThrowIfNull(jogador);

        Validator.ValidateObject(
            jogador,
            new ValidationContext(jogador),
            validateAllProperties: true
        );

        var usuario = await _usuarioRepository.BuscarPorIdAsync(jogador.UsuarioId);

        if (usuario is null)
        {
            throw new InvalidOperationException(
                "O usuario informado nao existe."
            );
        }

        await _jogadorRepository.AdicionarAsync(jogador);

        return jogador;
    }
}