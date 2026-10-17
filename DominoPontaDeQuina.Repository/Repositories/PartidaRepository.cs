using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DominoPontaDeQuina.Repository.Repositories;

public class PartidaRepository
{
    private readonly DominoDbContext _db;
    public PartidaRepository(DominoDbContext db) => _db = db;

    public Task<List<Partida>> ListarAsync() => _db.Partidas.AsNoTracking().ToListAsync();
    public Task<Partida?> BuscarPorIdAsync(Guid id) =>
        _db.Partidas.FirstOrDefaultAsync(x => x.Id == id);
    public async Task AdicionarAsync(Partida entidade)
    {
        await _db.Partidas.AddAsync(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task AtualizarAsync(Partida entidade)
    {
        _db.Partidas.Update(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> ExcluirAsync(Guid id)
    {
        var entidade = await BuscarPorIdAsync(id);
        if (entidade is null) return false;
        _db.Partidas.Remove(entidade);
        await _db.SaveChangesAsync();
        return true;
    }
    public Task<List<Partida>> BuscarPorStatusAsync(StatusJogo status) =>
        _db.Partidas.Where(x => x.Status == status).ToListAsync();
}
