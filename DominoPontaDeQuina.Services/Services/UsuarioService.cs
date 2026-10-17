using System.ComponentModel.DataAnnotations;
using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Services.Interfaces;

namespace DominoPontaDeQuina.Services.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public Task<List<Usuario>> ListarAsync()
    {
        return _usuarioRepository.ListarAsync();
    }

    public Task<Usuario?> BuscarPorIdAsync(Guid id)
    {
        return _usuarioRepository.BuscarPorIdAsync(id);
    }

    public async Task<Usuario> CadastrarAsync(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        Validator.ValidateObject(
            usuario,
            new ValidationContext(usuario),
            validateAllProperties: true
        );

        var existente = await _usuarioRepository.BuscarPorEmailAsync(usuario.Email);

        if (existente is not null)
        {
            throw new InvalidOperationException(
                "Ja existe um usuario com este e-mail."
            );
        }

        await _usuarioRepository.AdicionarAsync(usuario);

        return usuario;
    }
}