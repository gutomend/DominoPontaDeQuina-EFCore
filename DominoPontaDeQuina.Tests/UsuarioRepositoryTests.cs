using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using DominoPontaDeQuina.Repository.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DominoPontaDeQuina.Tests;

public class UsuarioRepositoryTests
{
    [Fact]
    public async Task DeveCadastrarEConsultarUsuario()
    {
        // Cria um banco SQLite temporário em memória
        using var conexao = new SqliteConnection("Data Source=:memory:");
        await conexao.OpenAsync();

        var opcoes = new DbContextOptionsBuilder<DominoDbContext>()
            .UseSqlite(conexao)
            .Options;

        await using var contexto = new DominoDbContext(opcoes);

        // Cria as tabelas necessárias para o teste
        await contexto.Database.EnsureCreatedAsync();

        var repositorio = new UsuarioRepository(contexto);

        // Cadastra um usuário
        var usuario = new Usuario
        {
            Nome = "Usuario Teste",
            Email = "teste@exemplo.com",
            HashSenha = "hash_teste"
        };

        await repositorio.AdicionarAsync(usuario);

        // Consulta pelo e-mail
        var encontrado = await repositorio.BuscarPorEmailAsync(
            "teste@exemplo.com"
        );

        Assert.NotNull(encontrado);
        Assert.Equal(usuario.Id, encontrado.Id);
        Assert.Equal("Usuario Teste", encontrado.Nome);

        // Verifica a listagem
        var usuarios = await repositorio.ListarAsync();

        Assert.Single(usuarios);
    }
}