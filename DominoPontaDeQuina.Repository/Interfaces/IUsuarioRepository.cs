using DominoPontaDeQuina.Domain.Entities;

namespace DominoPontaDeQuina.Repository.Interfaces;

public interface IUsuarioRepository
{
    Task<List<Usuario>> ListarAsync();
    Task<Usuario?> BuscarPorIdAsync(Guid id);
    Task AdicionarAsync(Usuario entidade);
    Task AtualizarAsync(Usuario entidade);
    Task<bool> ExcluirAsync(Guid id);
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task<List<Usuario>> PesquisarPorNomeAsync(string termo);
}
