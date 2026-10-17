using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using DominoPontaDeQuina.Repository.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DominoPontaDeQuina.Tests;

public class RepositoriesIntegracaoTests
{
    [Fact]
    public async Task DeveCadastrarEConsultarJogador()
    {
        using var conexao = new SqliteConnection("Data Source=:memory:");
        await conexao.OpenAsync();

        var opcoes = new DbContextOptionsBuilder<DominoDbContext>()
            .UseSqlite(conexao)
            .Options;

        await using var contexto = new DominoDbContext(opcoes);
        await contexto.Database.EnsureCreatedAsync();

        var usuarioRepository = new UsuarioRepository(contexto);
        var jogadorRepository = new JogadorRepository(contexto);

        var usuario = new Usuario
        {
            Nome = "Usuario Teste",
            Email = "jogador@teste.com",
            HashSenha = "hash_teste"
        };

        await usuarioRepository.AdicionarAsync(usuario);

        var jogador = new Jogador
        {
            NomeExibicao = "Jogador Teste",
            UsuarioId = usuario.Id
        };

        await jogadorRepository.AdicionarAsync(jogador);

        var encontrado = await jogadorRepository.BuscarPorIdAsync(jogador.Id);
        var jogadores = await jogadorRepository.BuscarPorUsuarioAsync(usuario.Id);

        Assert.NotNull(encontrado);
        Assert.Equal("Jogador Teste", encontrado.NomeExibicao);
        Assert.Single(jogadores);
    }

    [Fact]
    public async Task DeveCadastrarEConsultarPartida()
    {
        using var conexao = new SqliteConnection("Data Source=:memory:");
        await conexao.OpenAsync();

        var opcoes = new DbContextOptionsBuilder<DominoDbContext>()
            .UseSqlite(conexao)
            .Options;

        await using var contexto = new DominoDbContext(opcoes);
        await contexto.Database.EnsureCreatedAsync();

        var partidaRepository = new PartidaRepository(contexto);

        var partida = new Partida
        {
            Status = StatusJogo.Aguardando
        };

        await partidaRepository.AdicionarAsync(partida);

        var encontrada = await partidaRepository.BuscarPorIdAsync(partida.Id);
        var partidas = await partidaRepository.BuscarPorStatusAsync(
            StatusJogo.Aguardando
        );

        Assert.NotNull(encontrada);
        Assert.Equal(StatusJogo.Aguardando, encontrada.Status);
        Assert.Single(partidas);
    }

    [Fact]
    public async Task DeveCadastrarEConsultarParticipacao()
    {
        using var conexao = new SqliteConnection("Data Source=:memory:");
        await conexao.OpenAsync();

        var opcoes = new DbContextOptionsBuilder<DominoDbContext>()
            .UseSqlite(conexao)
            .Options;

        await using var contexto = new DominoDbContext(opcoes);
        await contexto.Database.EnsureCreatedAsync();

        var usuarioRepository = new UsuarioRepository(contexto);
        var jogadorRepository = new JogadorRepository(contexto);
        var partidaRepository = new PartidaRepository(contexto);
        var participacaoRepository =
            new ParticipacaoPartidaRepository(contexto);

        var usuario = new Usuario
        {
            Nome = "Usuario Teste",
            Email = "participacao@teste.com",
            HashSenha = "hash_teste"
        };

        await usuarioRepository.AdicionarAsync(usuario);

        var jogador = new Jogador
        {
            NomeExibicao = "Jogador Teste",
            UsuarioId = usuario.Id
        };

        await jogadorRepository.AdicionarAsync(jogador);

        var partida = new Partida
        {
            Status = StatusJogo.Aguardando
        };

        await partidaRepository.AdicionarAsync(partida);

        var participacao = new ParticipacaoPartida
        {
            PartidaId = partida.Id,
            JogadorId = jogador.Id,
            Posicao = 1,
            Pontuacao = 100,
            Vencedor = true
        };

        await participacaoRepository.AdicionarAsync(participacao);

        var participacoes =
            await participacaoRepository.BuscarPorPartidaAsync(partida.Id);

        var vitorias =
            await participacaoRepository.BuscarVitoriasAsync(jogador.Id);

        Assert.Single(participacoes);
        Assert.Single(vitorias);
        Assert.Equal(100, participacoes[0].Pontuacao);
        Assert.True(vitorias[0].Vencedor);
    }
}