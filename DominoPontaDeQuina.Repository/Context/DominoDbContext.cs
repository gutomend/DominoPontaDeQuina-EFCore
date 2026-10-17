using DominoPontaDeQuina.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DominoPontaDeQuina.Repository.Context;

public class DominoDbContext : DbContext
{
    public DominoDbContext(DbContextOptions<DominoDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Jogador> Jogadores => Set<Jogador>();
    public DbSet<Partida> Partidas => Set<Partida>();
    public DbSet<ParticipacaoPartida> ParticipacoesPartida => Set<ParticipacaoPartida>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(e =>
        {
            e.ToTable("Usuarios");
            e.HasKey(x => x.Id);
            e.HasMany(x => x.Jogadores).WithOne(x => x.Usuario)
                .HasForeignKey(x => x.UsuarioId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(x => x.Email).IsUnique();
        });
        modelBuilder.Entity<Jogador>(e =>
        {
            e.ToTable("Jogadores");
            e.HasKey(x => x.Id);
            e.HasMany(x => x.Participacoes).WithOne(x => x.Jogador)
                .HasForeignKey(x => x.JogadorId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Partida>(e =>
        {
            e.ToTable("Partidas");
            e.HasKey(x => x.Id);
            e.HasMany(x => x.Participacoes).WithOne(x => x.Partida)
                .HasForeignKey(x => x.PartidaId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<ParticipacaoPartida>(e =>
        {
            e.ToTable("ParticipacoesPartida");
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.PartidaId, x.JogadorId }).IsUnique();
        });
    }
}
