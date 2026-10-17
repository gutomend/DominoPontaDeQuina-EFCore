using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Services.Interfaces;

public interface IUsuarioService
{
    Task<List<Usuario>> ListarAsync();
    Task<Usuario?> BuscarPorIdAsync(Guid id);
    Task<Usuario> CadastrarAsync(Usuario usuario);
}
