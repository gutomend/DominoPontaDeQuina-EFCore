using DominoPontaDeQuina.Domain.Entities;
using DominoPontaDeQuina.Repository.Context;
using DominoPontaDeQuina.Repository.Interfaces;
using DominoPontaDeQuina.Repository.Repositories;
using DominoPontaDeQuina.Services.Interfaces;
using DominoPontaDeQuina.Services.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using var connection = new SqliteConnection("Data Source=:memory:");
connection.Open();

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

var usuarioService = scope.ServiceProvider.GetRequiredService<IUsuarioService>();
var jogadorService = scope.ServiceProvider.GetRequiredService<IJogadorService>();
var partidaService = scope.ServiceProvider.GetRequiredService<IPartidaService>();
var participacaoService = scope.ServiceProvider.GetRequiredService<IParticipacaoPartidaService>();

Console.WriteLine("=== Domino Ponta de Quina - Laboratorio DI ===");

var usuario = await usuarioService.CadastrarAsync(new Usuario
{
    Nome = "Usuario Teste",
    Email = "teste@domino.com",
    HashSenha = "hash-demonstracao"
});

Console.WriteLine($"Usuario cadastrado: {usuario.Nome}");

var jogador = await jogadorService.CadastrarAsync(new Jogador
{
    NomeExibicao = "Jogador Teste",
    UsuarioId = usuario.Id
});

Console.WriteLine($"Jogador cadastrado: {jogador.NomeExibicao}");

var partida = await partidaService.CadastrarAsync(new Partida());

Console.WriteLine($"Partida criada: {partida.Id}");

await participacaoService.CadastrarAsync(new ParticipacaoPartida
{
    PartidaId = partida.Id,
    JogadorId = jogador.Id,
    Posicao = 1,
    Pontuacao = 0,
    Vencedor = false
});

Console.WriteLine("Participacao registrada com sucesso!");

var usuarios = await usuarioService.ListarAsync();
var jogadores = await jogadorService.BuscarPorUsuarioAsync(usuario.Id);
var partidas = await partidaService.ListarAsync();
var participantes = await participacaoService.BuscarPorPartidaAsync(partida.Id);

Console.WriteLine();
Console.WriteLine("=== Resultado das consultas LINQ ===");
Console.WriteLine($"Usuarios encontrados: {usuarios.Count}");
Console.WriteLine($"Jogadores encontrados: {jogadores.Count}");
Console.WriteLine($"Partidas encontradas: {partidas.Count}");
Console.WriteLine($"Participantes encontrados: {participantes.Count}");

Console.WriteLine();
Console.WriteLine("Fluxo completo executado com sucesso utilizando DI!");