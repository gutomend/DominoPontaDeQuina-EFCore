using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace DominoPontaDeQuina.Repository.Repositories;

public class ParticipacaoPartidaRepository : IParticipacaoPartidaRepository
{
    private readonly DominoDbContext _db;
    public ParticipacaoPartidaRepository(DominoDbContext db) => _db = db;

    public Task<List<ParticipacaoPartida>> ListarAsync() => _db.ParticipacoesPartida.AsNoTracking().ToListAsync();
    public Task<ParticipacaoPartida?> BuscarPorIdAsync(Guid id) =>
        _db.ParticipacoesPartida.FirstOrDefaultAsync(x => x.Id == id);
    public async Task AdicionarAsync(ParticipacaoPartida entidade)
    {
        await _db.ParticipacoesPartida.AddAsync(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task AtualizarAsync(ParticipacaoPartida entidade)
    {
        _db.ParticipacoesPartida.Update(entidade);
        await _db.SaveChangesAsync();
    }
    public async Task<bool> ExcluirAsync(Guid id)
    {
        var entidade = await BuscarPorIdAsync(id);
        if (entidade is null) return false;
        _db.ParticipacoesPartida.Remove(entidade);
        await _db.SaveChangesAsync();
        return true;
    }
    public Task<List<ParticipacaoPartida>> BuscarPorPartidaAsync(Guid partidaId) =>
        _db.ParticipacoesPartida.Where(x => x.PartidaId == partidaId)
            .OrderBy(x => x.Posicao).ToListAsync();
    public Task<List<ParticipacaoPartida>> BuscarVitoriasAsync(Guid jogadorId) =>
        _db.ParticipacoesPartida.Where(x => x.JogadorId == jogadorId && x.Vencedor)
            .ToListAsync();
}

