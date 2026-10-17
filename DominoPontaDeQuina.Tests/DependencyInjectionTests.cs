using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Repository.Repositories;
using DominoPontaDeQuina.Services.Interfaces;
using DominoPontaDeQuina.Services.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DominoPontaDeQuina.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public async Task DeveResolverServicesECadastrarUsuario()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var services = new ServiceCollection();

        services.AddDbContext<DominoDbContext>(options =>
            options.UseSqlite(connection));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IJogadorRepository, JogadorRepository>();
        services.AddScoped<IPartidaRepository, PartidaRepository>();
        services.AddScoped<IParticipacaoPartidaRepository, ParticipacaoPartidaRepository>();

        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IJogadorService, JogadorService>();
        services.AddScoped<IPartidaService, PartidaService>();
        services.AddScoped<IParticipacaoPartidaService, ParticipacaoPartidaService>();

        using var provider = services.BuildServiceProvider(
            validateScopes: true);

        using var scope = provider.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<DominoDbContext>();
        await db.Database.EnsureCreatedAsync();

        var usuarioService = scope.ServiceProvider
            .GetRequiredService<IUsuarioService>();

        Assert.NotNull(scope.ServiceProvider
            .GetRequiredService<IJogadorService>());

        Assert.NotNull(scope.ServiceProvider
            .GetRequiredService<IPartidaService>());

        Assert.NotNull(scope.ServiceProvider
            .GetRequiredService<IParticipacaoPartidaService>());

        var usuario = await usuarioService.CadastrarAsync(new Usuario
        {
            Nome = "Usuario Integracao",
            Email = "integracao@domino.com",
            HashSenha = "hash-teste"
        });

        var encontrado = await usuarioService.BuscarPorIdAsync(usuario.Id);

        Assert.NotNull(encontrado);
        Assert.Equal(usuario.Id, encontrado.Id);
        Assert.Equal("integracao@domino.com", encontrado.Email);
    }
}