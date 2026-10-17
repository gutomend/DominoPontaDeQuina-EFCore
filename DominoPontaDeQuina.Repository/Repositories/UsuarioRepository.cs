using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DominoPontaDeQuina.Repository.Repositories;

public class UsuarioRepository
{
    private readonly DominoDbContext _db;
    public UsuarioRepository(DominoDbContext db) => _db = db;

    public Task<List<Usuario>> ListarAsync() => _db.Usuarios.AsNoTracking().ToListAsync();
    public Task<Usuario?> BuscarPorIdAsync(Guid id) =>
        _db.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    public async Task AdicionarAsync(Usuario entidade)
    {
        await _db.Usuarios.AddAsync(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task AtualizarAsync(Usuario entidade)
    {
        _db.Usuarios.Update(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> ExcluirAsync(Guid id)
    {
        var entidade = await BuscarPorIdAsync(id);
        if (entidade is null) return false;
        _db.Usuarios.Remove(entidade);
        await _db.SaveChangesAsync();
        return true;
    }
    public Task<Usuario?> BuscarPorEmailAsync(string email) =>
        _db.Usuarios.FirstOrDefaultAsync(x => x.Email == email);
    public Task<List<Usuario>> PesquisarPorNomeAsync(string termo) =>
        _db.Usuarios.Where(x => x.Nome.Contains(termo)).ToListAsync();
}
