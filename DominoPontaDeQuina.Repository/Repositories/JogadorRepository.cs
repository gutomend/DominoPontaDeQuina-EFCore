using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DominoPontaDeQuina.Repository.Repositories;

public class JogadorRepository
{
    private readonly DominoDbContext _db;
    public JogadorRepository(DominoDbContext db) => _db = db;

    public Task<List<Jogador>> ListarAsync() => _db.Jogadores.AsNoTracking().ToListAsync();
    public Task<Jogador?> BuscarPorIdAsync(Guid id) =>
        _db.Jogadores.FirstOrDefaultAsync(x => x.Id == id);
    public async Task AdicionarAsync(Jogador entidade)
    {
        await _db.Jogadores.AddAsync(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task AtualizarAsync(Jogador entidade)
    {
        _db.Jogadores.Update(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> ExcluirAsync(Guid id)
    {
        var entidade = await BuscarPorIdAsync(id);
        if (entidade is null) return false;
        _db.Jogadores.Remove(entidade);
        await _db.SaveChangesAsync();
        return true;
    }
    public Task<List<Jogador>> BuscarPorUsuarioAsync(Guid usuarioId) =>
        _db.Jogadores.Where(x => x.UsuarioId == usuarioId).ToListAsync();
    public Task<List<Jogador>> PesquisarPorNomeAsync(string termo) =>
        _db.Jogadores.Where(x => x.NomeExibicao.Contains(termo)).ToListAsync();
}
