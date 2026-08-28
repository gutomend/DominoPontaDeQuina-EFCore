# Dominó Ponta de Quina — Laboratório EF Core

Projeto acadêmico desenvolvido para praticar persistência de dados com Entity Framework Core. Nesta etapa, o modelo de dados do jogo de dominó foi evoluído com entidades persistentes, Data Annotations, `DbContext`, SQLite e migrations.

## Integrantes

- **Augusto Mendonça** — RM558371
- **Gabriel Vasquez** — RM557056
- **Gustavo Oliveira** — RM559163

Engenharia de Software — FIAP

## Objetivos da atividade

- Configurar `Usuario` e `Jogador` com Data Annotations;
- manter `Jogo` mapeado pelas convenções do EF Core;
- implementar o `DominoDbContext` com os `DbSet`s necessários;
- configurar a conexão com um banco de dados;
- gerar a migration inicial;
- aplicar a migration ao banco.

## Estrutura da solução

```text
DominoPontaDeQuina/
├── DominoPontaDeQuina.Core/        # Regras e fluxo do jogo
├── DominoPontaDeQuina.Domain/      # Entidades persistentes
├── DominoPontaDeQuina.Repository/  # DbContext e migrations
├── DominoPontaDeQuina.Migrations/  # Projeto de inicialização do EF Core
├── DominoPontaDeQuina.Tests/       # Testes automatizados do jogo
└── DominoPontaDeQuina.slnx         # Solução
```

## Tecnologias utilizadas

- C#;
- .NET 8;
- Entity Framework Core 8.0.15;
- Microsoft.EntityFrameworkCore.Sqlite;
- SQLite;
- xUnit;
- Visual Studio;
- Git e GitHub.

## Modelo persistente

### Usuario

Representa a conta utilizada no aplicativo. A entidade foi configurada com Data Annotations para definir:

- tabela `Usuarios`;
- chave primária `Id`;
- nome obrigatório entre 3 e 100 caracteres;
- e-mail obrigatório, validado e limitado a 150 caracteres;
- hash de senha obrigatório e limitado a 255 caracteres;
- data de criação;
- relacionamento de um usuário para vários jogadores.

### Jogador

Representa um perfil de jogo associado a um usuário. A entidade foi configurada com Data Annotations para definir:

- tabela `Jogadores`;
- chave primária `Id`;
- nome de exibição obrigatório entre 3 e 50 caracteres;
- chave estrangeira `UsuarioId`;
- relacionamento com `Usuario`;
- coleção de participações em jogos.

### Jogo

A entidade `Jogo` foi mantida sem Data Annotations e sem configuração Fluent API específica. Seu mapeamento utiliza as convenções do EF Core, conforme solicitado na atividade.

### ParticipacaoJogo

Relaciona `Jogador` e `Jogo`, armazenando posição, pontuação e indicação de vencedor.

## Relacionamentos

```text
Usuario 1 ─── N Jogador
Jogador 1 ─── N ParticipacaoJogo
Jogo    1 ─── N ParticipacaoJogo
```

## DominoDbContext

O contexto foi implementado no projeto `DominoPontaDeQuina.Repository` com os seguintes conjuntos:

```csharp
public DbSet<Usuario> Usuarios => Set<Usuario>();
public DbSet<Jogador> Jogadores => Set<Jogador>();
public DbSet<Jogo> Jogos => Set<Jogo>();
public DbSet<ParticipacaoJogo> ParticipacoesJogo
    => Set<ParticipacaoJogo>();
```

O contexto recebe `DbContextOptions<DominoDbContext>` pelo construtor, permitindo que o provedor de banco seja configurado externamente.

## Banco de dados

Foi escolhido o SQLite, por ser simples, local e compatível com o escopo do laboratório.

A conexão é configurada na `DominoDbContextFactory`:

```csharp
optionsBuilder.UseSqlite("Data Source=domino.db");
```

Ao aplicar a migration, o banco é criado em:

```text
DominoPontaDeQuina.Migrations/domino.db
```

O arquivo `domino.db` é gerado localmente e não deve ser enviado ao repositório.

## Pré-requisitos

- SDK do .NET 8;
- ferramenta `dotnet-ef` 8.0.15.

Para conferir o SDK:

```powershell
dotnet --list-sdks
```

Para instalar a ferramenta do EF Core:

```powershell
dotnet tool install --global dotnet-ef --version 8.0.15
```

Para verificar a instalação:

```powershell
dotnet ef --version
```

## Restaurar e compilar

Na raiz da solução, execute:

```powershell
dotnet restore
dotnet build
```

## Criar a migration inicial

O projeto `DominoPontaDeQuina.Migrations` deve ser utilizado como startup project, enquanto o contexto e as migrations ficam em `DominoPontaDeQuina.Repository`.

```powershell
dotnet ef migrations add Inicial --project ".\DominoPontaDeQuina.Repository\DominoPontaDeQuina.Repository.csproj" --startup-project ".\DominoPontaDeQuina.Migrations\DominoPontaDeQuina.Migrations.csproj" --context DominoDbContext
```

## Aplicar a migration

```powershell
dotnet ef database update --project ".\DominoPontaDeQuina.Repository\DominoPontaDeQuina.Repository.csproj" --startup-project ".\DominoPontaDeQuina.Migrations\DominoPontaDeQuina.Migrations.csproj" --context DominoDbContext
```

## Consultar as migrations

```powershell
dotnet ef migrations list --project ".\DominoPontaDeQuina.Repository\DominoPontaDeQuina.Repository.csproj" --startup-project ".\DominoPontaDeQuina.Migrations\DominoPontaDeQuina.Migrations.csproj" --context DominoDbContext
```

## Estrutura criada no banco

A migration inicial cria:

- `Usuarios`;
- `Jogadores`;
- `Jogos`;
- `ParticipacoesJogo`;
- `__EFMigrationsHistory`;
- chaves primárias;
- chaves estrangeiras;
- índices dos relacionamentos.

## Validações realizadas

- compilação da solução concluída sem erros nas alterações do EF Core;
- migration `Inicial` gerada com sucesso;
- migration aplicada ao SQLite;
- arquivo `domino.db` criado;
- migration registrada no banco;
- arquivos de migration e snapshot gerados no projeto Repository.

## Testes automatizados

O projeto-base possui 78 testes do núcleo do jogo. Na versão recebida:

- 44 testes são bem-sucedidos;
- 34 testes falham em métodos do projeto `Core` que ainda contêm `NotImplementedException` ou exceções genéricas.

Essas lacunas pertencem às regras preexistentes do jogo e não às configurações de persistência implementadas neste laboratório. A implementação desses métodos está fora do escopo da atividade de EF Core.

## Arquivos que não devem ser versionados

```gitignore
.vs/
**/bin/
**/obj/
*.db
```

## Créditos

Projeto-base disponibilizado para a atividade acadêmica. A configuração das entidades persistentes, do contexto, do SQLite e da migration inicial foi realizada pela equipe.
