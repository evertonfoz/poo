# Sistema de Cadastro Academico — Guia do Aluno

Este projeto e um sistema academico desenvolvido em C# com .NET 10. Ele demonstra, de forma progressiva, como organizar uma aplicacao orientada a objetos em multiplos projetos, separando dominio, regras de negocio, servicos de aplicacao e interfaces de usuario.

O mesmo modelo de negocio e a mesma camada de servicos sao consumidos por duas interfaces distintas: uma aplicacao Console e uma aplicacao Blazor.

---

## Resumo Rapido

| Objetivo | Onde ir |
|---|---|
| Entender o que o sistema faz | [PRD](Docs/PRD/prd_sistema_academico_console_blazor_poo.md) |
| Executar o projeto | [Como Executar](#como-executar) |
| Estudar a documentacao didatica | [Mapa da Documentacao](#mapa-da-documentacao--por-onde-comecar) |
| Implementar uma nova entidade | [Roteiro](#roteiro-para-implementar-um-novo-problema) |
| Localizar um conceito no codigo | [Indice de Conceitos](#onde-cada-conceito-esta-no-codigo) |
| Duvidas frequentes | [FAQ](#perguntas-frequentes) |

---

## Pre-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado
- Git para clonar o repositorio
- VS Code ou outro editor compativel com C#

---

## Como Executar

### Compilar a solution completa

```bash
dotnet build CadastroAcademico.slnx
```

### Executar o ConsoleApp

```bash
dotnet run --project src/CadastroAcademico.ConsoleApp
```

### Executar o BlazorApp

```bash
dotnet run --project src/CadastroAcademico.BlazorApp
```

Apos iniciar o Blazor, abra o navegador e acesse as rotas abaixo pela sidebar de navegacao:

| Rota | Descricao |
|---|---|
| `/cursos` | CRUD completo de cursos |
| `/alunos` | CRUD completo de alunos com perfil academico |
| `/matriculas` | Matricular, cancelar, concluir e excluir matriculas; busca filtravel de aluno |
| `/academico` | Tela didatica com os tres fluxos lado a lado (para fins de estudo) |

---

## Estrutura da Solution

```
MultiProjetos/
  CadastroAcademico.slnx               ← Arquivo de solution (.NET 10)
  Docs/                                ← Documentacao didatica (leia antes do codigo)
  src/
    CadastroAcademico.Domain/          ← Entidades, regras e invariantes de negocio
    CadastroAcademico.Application/     ← Servicos, DTOs, resultados e store em memoria
    CadastroAcademico.ConsoleApp/      ← Interface textual (usa a Application)
    CadastroAcademico.BlazorApp/       ← Interface web (usa a Application)
```

### Dependencias entre projetos

```
ConsoleApp  ──┐
              ├──► Application ──► Domain
BlazorApp   ──┘
```

- `Domain` nao depende de nenhum outro projeto.
- `Application` depende apenas de `Domain`.
- `ConsoleApp` e `BlazorApp` dependem de `Application` (e indiretamente de `Domain`).

---

## Mapa da Documentacao — Por Onde Comecar

Leia os documentos na ordem abaixo. Cada etapa prepara voce para a proxima.

### Etapa 0 — Entenda o que o sistema deve fazer

[Docs/PRD/prd_sistema_academico_console_blazor_poo.md](Docs/PRD/prd_sistema_academico_console_blazor_poo.md)

O PRD (Product Requirements Document) descreve o problema que o sistema resolve, as entidades envolvidas, os requisitos funcionais e a arquitetura esperada. Leia antes de qualquer codigo.

---

### Etapa 1 — Entenda como a solution e os projetos se organizam

Pasta: `Docs/01-fundamentos-solucao-dotnet/`

| Arquivo | O que voce aprende |
|---|---|
| [01_tipos-de-solution-sln-e-slnx.md](Docs/01-fundamentos-solucao-dotnet/01_tipos-de-solution-sln-e-slnx.md) | Diferenca entre `.sln` e `.slnx` e por que usamos `.slnx` |
| [02_por-que-usar-varios-projetos-na-solution.md](Docs/01-fundamentos-solucao-dotnet/02_por-que-usar-varios-projetos-na-solution.md) | Razoes para separar em projetos distintos |
| [03_referencias-entre-projetos-na-pratica.md](Docs/01-fundamentos-solucao-dotnet/03_referencias-entre-projetos-na-pratica.md) | Como um projeto referencia outro e o impacto no build |
| [04_ordem-de-compilacao-e-restauracao-na-solution.md](Docs/01-fundamentos-solucao-dotnet/04_ordem-de-compilacao-e-restauracao-na-solution.md) | Como o .NET decide a ordem de compilacao |
| [05_projeto-isolado-vs-solution-completa.md](Docs/01-fundamentos-solucao-dotnet/05_projeto-isolado-vs-solution-completa.md) | Quando compilar so um projeto vs a solution inteira |

---

### Etapa 2 — Entenda o Dominio e a POO

Pasta: `Docs/02-dominio-e-poo/`

| Arquivo | O que voce aprende |
|---|---|
| [01_papel_da_camada_domain.md](Docs/02-dominio-e-poo/01_papel_da_camada_domain.md) | O que e o Domain e por que ele nao depende de nada |
| [02_entities_na_pratica.md](Docs/02-dominio-e-poo/02_entities_na_pratica.md) | Como sao criadas as entities: `Id`, `private set`, validacao no construtor, colecoes protegidas |
| [03_value_objects_e_invariantes.md](Docs/02-dominio-e-poo/03_value_objects_e_invariantes.md) | O que e um Value Object, o que sao invariantes e como sao implementadas |
| [04_matricula_e_transicao_de_estado.md](Docs/02-dominio-e-poo/04_matricula_e_transicao_de_estado.md) | Maquina de estados simples aplicada a `Matricula` |
| [05_relacao_domain_application_ui.md](Docs/02-dominio-e-poo/05_relacao_domain_application_ui.md) | Como Domain, Application e UI se relacionam no fluxo de chamada |
| [06_checklist_dominio.md](Docs/02-dominio-e-poo/06_checklist_dominio.md) | Checklist de validacao da etapa de dominio |
| [07_exercicio_guiado_dominio.md](Docs/02-dominio-e-poo/07_exercicio_guiado_dominio.md) | Exercicio de leitura de codigo para fixar os conceitos |

---

### Etapa 3 — Entenda como os projetos sao organizados internamente

Pasta: `Docs/03-organizacao-arquitetura-projetos/`

| Arquivo | O que voce aprende |
|---|---|
| [01_por-que-organizar-por-pastas.md](Docs/03-organizacao-arquitetura-projetos/01_por-que-organizar-por-pastas.md) | Beneficios da organizacao por responsabilidade |
| [02_organizacao_do_domain.md](Docs/03-organizacao-arquitetura-projetos/02_organizacao_do_domain.md) | Estrutura de pastas do projeto Domain |
| [03_organizacao_do_application.md](Docs/03-organizacao-arquitetura-projetos/03_organizacao_do_application.md) | Estrutura de pastas do projeto Application |
| [04_organizacao_do_console_e_blazor.md](Docs/03-organizacao-arquitetura-projetos/04_organizacao_do_console_e_blazor.md) | Estrutura de pastas dos projetos de interface |
| [05_checklist_de_organizacao.md](Docs/03-organizacao-arquitetura-projetos/05_checklist_de_organizacao.md) | Checklist de validacao da organizacao |

Para aprofundamento, leia tambem `Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/`:

| Arquivo | O que voce aprende |
|---|---|
| [01_principios_arquiteturais_da_organizacao.md](Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/01_principios_arquiteturais_da_organizacao.md) | Os cinco principios que guiaram as decisoes |
| [07_entities_valueobjects_enums_na_pratica.md](Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/07_entities_valueobjects_enums_na_pratica.md) | Comparativo detalhado entre Entity, ValueObject e Enum |
| [08_exercicio_guiado_entities_valueobjects_enums.md](Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/08_exercicio_guiado_entities_valueobjects_enums.md) | Exercicio com 8 casos e gabarito comentado |

---

### Etapa 4 — Entenda a camada Application e a persistencia em memoria

Pasta: `Docs/04-application-e-persistencia-em-memoria/`

| Arquivo | O que voce aprende |
|---|---|
| [01_papel_da_camada_application.md](Docs/04-application-e-persistencia-em-memoria/01_papel_da_camada_application.md) | O que e a Application e o que ela nao deve fazer |
| [02_store_em_memoria_compartilhado.md](Docs/04-application-e-persistencia-em-memoria/02_store_em_memoria_compartilhado.md) | Como o `InMemoryAcademicStore` funciona e por que ele existe |
| [03_services_de_aplicacao.md](Docs/04-application-e-persistencia-em-memoria/03_services_de_aplicacao.md) | O que faz cada service: `CourseService`, `StudentService`, `EnrollmentService` |
| [04_dtos_e_resultados.md](Docs/04-application-e-persistencia-em-memoria/04_dtos_e_resultados.md) | O que sao DTOs, `OperationResult` e `OperationResult<T>` |
| [05_integracao_console_blazor.md](Docs/04-application-e-persistencia-em-memoria/05_integracao_console_blazor.md) | Como ConsoleApp e BlazorApp consomem a Application |
| [06_checklist_da_etapa.md](Docs/04-application-e-persistencia-em-memoria/06_checklist_da_etapa.md) | Checklist de validacao da etapa Application |
| [07_evolucao_status_matricula_na_blazor.md](Docs/04-application-e-persistencia-em-memoria/07_evolucao_status_matricula_na_blazor.md) | Como cancelar/concluir matricula pela interface Blazor |

---

## Fluxo de Chamada na Pratica

Para entender como tudo se conecta, siga este fluxo com o codigo:

1. **ConsoleApp** ([src/CadastroAcademico.ConsoleApp/Program.cs](src/CadastroAcademico.ConsoleApp/Program.cs)): instancia os servicos e os chama diretamente.
2. **BlazorApp** ([src/CadastroAcademico.BlazorApp/Program.cs](src/CadastroAcademico.BlazorApp/Program.cs)): registra os servicos no container de DI.
3. **FluxoAcademico.razor** ([src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor](src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor)): injeta os servicos e os chama nos eventos de botao.
4. **CourseService** ([src/CadastroAcademico.Application/Services/CourseService.cs](src/CadastroAcademico.Application/Services/CourseService.cs)): recebe o input, cria a entity e salva no store.
5. **Curso** ([src/CadastroAcademico.Domain/Entities/Curso.cs](src/CadastroAcademico.Domain/Entities/Curso.cs)): valida os dados e protege as invariantes.

---

## Roteiro para Implementar um Novo Problema

Quando o professor apresentar um novo problema, siga exatamente as etapas abaixo. O padrao e o mesmo que foi usado para construir o sistema atual.

> Nas instrucoes abaixo, substitua `NomeEntidade` pelo nome real do conceito que voce vai implementar.

---

### Passo 1 — Entenda o problema e identifique os conceitos

Antes de escrever qualquer codigo, responda:

1. Qual e a entidade principal? (precisa de `Id`? tem ciclo de vida?)
2. Ha value objects? (grupos de valores sem identidade propria)
3. Ha enums? (conjuntos fechados de estados)
4. Quais sao as invariantes? (o que nunca pode ser invalido)
5. Ha associacoes com entidades existentes?

Use como referencia: [07_entities_valueobjects_enums_na_pratica.md](Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/07_entities_valueobjects_enums_na_pratica.md)

---

### Passo 2 — Crie a Entity (ou Value Object/Enum) no Domain

Caminho: `src/CadastroAcademico.Domain/Entities/NomeEntidade.cs`

Modelo base de Entity:

```csharp
namespace CadastroAcademico.Domain;

public class NomeEntidade
{
    private static int _ultimoId;

    public int Id { get; }
    public string Campo1 { get; private set; }
    public int Campo2 { get; private set; }

    public NomeEntidade(string campo1, int campo2)
    {
        Id = ++_ultimoId;
        Campo1 = ValidarCampo1(campo1);
        Campo2 = ValidarCampo2(campo2);
    }

    private static string ValidarCampo1(string campo1)
    {
        if (string.IsNullOrWhiteSpace(campo1))
        {
            throw new ArgumentException("Campo1 e obrigatorio.", nameof(campo1));
        }

        return campo1.Trim();
    }

    private static int ValidarCampo2(int campo2)
    {
        if (campo2 <= 0)
        {
            throw new ArgumentException("Campo2 deve ser maior que zero.", nameof(campo2));
        }

        return campo2;
    }
}
```

Checklist do Passo 2:
- [ ] Classe criada na pasta `Entities` (ou `ValueObjects` ou `Enums`)
- [ ] `Id` e gerado automaticamente via `++_ultimoId`
- [ ] Todas as propriedades usam `private set`
- [ ] Todos os campos obrigatorios sao validados no construtor
- [ ] Nenhuma dependencia de Console, Blazor ou Application

---

### Passo 3 — Crie os DTOs na Application

Crie dois arquivos na pasta `src/CadastroAcademico.Application/DTOs/NomeEntidades/`:

**`CreateNomeEntidadeInput.cs`** — dados de entrada para criacao:

```csharp
namespace CadastroAcademico.Application.DTOs.NomeEntidades;

public sealed class CreateNomeEntidadeInput
{
    public string Campo1 { get; init; } = string.Empty;
    public int Campo2 { get; init; }
}
```

**`NomeEntidadeDto.cs`** — dados de saida apos operacao:

```csharp
namespace CadastroAcademico.Application.DTOs.NomeEntidades;

public sealed class NomeEntidadeDto
{
    public int Id { get; init; }
    public string Campo1 { get; init; } = string.Empty;
    public int Campo2 { get; init; }
}
```

Checklist do Passo 3:
- [ ] `Input` contem apenas o que a interface precisa enviar
- [ ] `Dto` contem apenas o que a interface precisa exibir
- [ ] Nenhuma referencia a entidades de dominio nos DTOs

---

### Passo 4 — Adicione a nova entidade ao Store

Arquivo: [src/CadastroAcademico.Application/Common/InMemoryAcademicStore.cs](src/CadastroAcademico.Application/Common/InMemoryAcademicStore.cs)

Adicione uma lista privada, uma propriedade somente leitura e um metodo `Add`:

```csharp
private readonly List<NomeEntidade> _nomeEntidades = new();

public IReadOnlyList<NomeEntidade> NomeEntidades => _nomeEntidades;

public void AddNomeEntidade(NomeEntidade nomeEntidade)
{
    _nomeEntidades.Add(nomeEntidade);
}
```

Checklist do Passo 4:
- [ ] Lista interna e `private readonly`
- [ ] Propriedade publica retorna `IReadOnlyList<T>`
- [ ] Metodo `Add` aceita a entity do dominio

---

### Passo 5 — Crie o Service na Application

Caminho: `src/CadastroAcademico.Application/Services/NomeEntidadeService.cs`

```csharp
using CadastroAcademico.Application.Common;
using CadastroAcademico.Application.DTOs.NomeEntidades;
using CadastroAcademico.Application.Results;
using CadastroAcademico.Domain;

namespace CadastroAcademico.Application.Services;

public sealed class NomeEntidadeService
{
    private readonly InMemoryAcademicStore _store;

    public NomeEntidadeService(InMemoryAcademicStore store)
    {
        _store = store;
    }

    public OperationResult<NomeEntidadeDto> Create(CreateNomeEntidadeInput input)
    {
        try
        {
            var entidade = new NomeEntidade(input.Campo1, input.Campo2);
            _store.AddNomeEntidade(entidade);

            return OperationResult<NomeEntidadeDto>.Success(ToDto(entidade), "NomeEntidade criada com sucesso.");
        }
        catch (Exception ex)
        {
            return OperationResult<NomeEntidadeDto>.Failure(ex.Message);
        }
    }

    public OperationResult<NomeEntidadeDto> GetById(int id)
    {
        var entidade = _store.NomeEntidades.FirstOrDefault(e => e.Id == id);
        if (entidade is null)
        {
            return OperationResult<NomeEntidadeDto>.Failure("NomeEntidade nao encontrada.");
        }

        return OperationResult<NomeEntidadeDto>.Success(ToDto(entidade));
    }

    public IReadOnlyList<NomeEntidadeDto> GetAll()
    {
        return _store.NomeEntidades.Select(ToDto).ToList();
    }

    private static NomeEntidadeDto ToDto(NomeEntidade entidade)
    {
        return new NomeEntidadeDto
        {
            Id = entidade.Id,
            Campo1 = entidade.Campo1,
            Campo2 = entidade.Campo2
        };
    }
}
```

Checklist do Passo 5:
- [ ] Service recebe `InMemoryAcademicStore` pelo construtor
- [ ] Metodos retornam `OperationResult<T>` ou `IReadOnlyList<T>`
- [ ] Excecoes do dominio sao capturadas e convertidas em `Failure`
- [ ] Metodo `ToDto` privado converte entity para DTO

---

### Passo 6 — Compile e valide

```bash
dotnet build CadastroAcademico.slnx
```

O build deve passar sem erros antes de integrar nas interfaces.

---

### Passo 7 — Integre no ConsoleApp

Arquivo: [src/CadastroAcademico.ConsoleApp/Program.cs](src/CadastroAcademico.ConsoleApp/Program.cs)

Instancie e use o novo service seguindo o padrao existente:

```csharp
var nomeEntidadeService = new NomeEntidadeService(store);

// Criacao
var resultado = nomeEntidadeService.Create(new CreateNomeEntidadeInput
{
    Campo1 = "Valor de teste",
    Campo2 = 10
});

if (!resultado.IsSuccess || resultado.Value is null)
{
    Console.WriteLine($"Falha: {resultado.Message}");
    return;
}

Console.WriteLine($"Criado: Id={resultado.Value.Id}, Campo1={resultado.Value.Campo1}");

// Listagem
foreach (var item in nomeEntidadeService.GetAll())
{
    Console.WriteLine($"{item.Id} - {item.Campo1} - {item.Campo2}");
}
```

Checklist do Passo 7:
- [ ] Service instanciado com o mesmo `store` compartilhado
- [ ] `OperationResult.IsSuccess` verificado antes de usar `Value`
- [ ] Mensagem de erro exibida quando `IsSuccess` for `false`

---

### Passo 8 — Integre no BlazorApp

#### 8.1 Registrar o service no DI

Arquivo: [src/CadastroAcademico.BlazorApp/Program.cs](src/CadastroAcademico.BlazorApp/Program.cs)

```csharp
builder.Services.AddScoped<NomeEntidadeService>();
```

#### 8.2 Injetar e usar no componente

Arquivo: [src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor](src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor)

Na secao de `@using` e `@inject`:

```razor
@using CadastroAcademico.Application.DTOs.NomeEntidades
@using CadastroAcademico.Application.Services

@inject NomeEntidadeService NomeEntidadeService
```

No formulario (HTML):

```razor
<div class="card">
    <div class="card-header">Criar NomeEntidade</div>
    <div class="card-body">
        <div class="mb-2">
            <label class="form-label">Campo1</label>
            <input class="form-control" @bind="_campo1" />
        </div>
        <div class="mb-3">
            <label class="form-label">Campo2</label>
            <input type="number" class="form-control" @bind="_campo2" />
        </div>
        <button class="btn btn-primary" @onclick="CreateNomeEntidade">Criar</button>
    </div>
</div>
```

Na listagem (HTML):

```razor
<h3>NomeEntidades</h3>
<ul class="list-group">
    @foreach (var item in _nomeEntidades)
    {
        <li class="list-group-item">@item.Id - @item.Campo1 - @item.Campo2</li>
    }
</ul>
```

No bloco `@code`:

```razor
@code {
    private string _campo1 = string.Empty;
    private int _campo2;
    private IReadOnlyList<NomeEntidadeDto> _nomeEntidades = [];

    private void CreateNomeEntidade()
    {
        var result = NomeEntidadeService.Create(new CreateNomeEntidadeInput
        {
            Campo1 = _campo1,
            Campo2 = _campo2
        });

        SetFeedback(result.IsSuccess, result.Message);
        if (result.IsSuccess)
        {
            _campo1 = string.Empty;
            _campo2 = 0;
            RefreshNomeEntidades();
        }
    }

    private void RefreshNomeEntidades()
    {
        _nomeEntidades = NomeEntidadeService.GetAll();
    }
}
```

Checklist do Passo 8:
- [ ] Service registrado no `Program.cs` com `AddScoped`
- [ ] `@inject` declarado no topo do componente
- [ ] Formulario usa `@bind` para capturar valores
- [ ] Evento de botao (`@onclick`) chama metodo do `@code`
- [ ] Lista atualizada apos operacao bem-sucedida

---

### Passo 9 — Teste os cenarios principais

Antes de declarar o problema resolvido, valide estes cenarios:

| Cenario | O que verificar |
|---|---|
| Criar com dados validos | Registro aparece na listagem |
| Criar com campo obrigatorio vazio | Mensagem de erro exibida, sem crash |
| Criar com valor numerico invalido (ex.: 0 ou negativo) | Mensagem de erro exibida |
| Listar apos criar varios registros | Todos aparecem corretamente |
| Verificar que a regra esta no Domain | O erro deve vir de `ArgumentException` lancada no construtor |

---

## Onde Cada Conceito Esta no Codigo

| Conceito | Onde ler no codigo |
|---|---|
| Entity com `Id` e `private set` | [src/CadastroAcademico.Domain/Entities/Aluno.cs](src/CadastroAcademico.Domain/Entities/Aluno.cs) |
| Entity com colecao protegida | [src/CadastroAcademico.Domain/Entities/Curso.cs](src/CadastroAcademico.Domain/Entities/Curso.cs) |
| Entity com maquina de estados | [src/CadastroAcademico.Domain/Entities/Matricula.cs](src/CadastroAcademico.Domain/Entities/Matricula.cs) |
| Value Object | [src/CadastroAcademico.Domain/ValueObjects/PerfilAcademico.cs](src/CadastroAcademico.Domain/ValueObjects/PerfilAcademico.cs) |
| Enum de estado | [src/CadastroAcademico.Domain/Enums/StatusMatricula.cs](src/CadastroAcademico.Domain/Enums/StatusMatricula.cs) |
| Store em memoria | [src/CadastroAcademico.Application/Common/InMemoryAcademicStore.cs](src/CadastroAcademico.Application/Common/InMemoryAcademicStore.cs) |
| OperationResult | [src/CadastroAcademico.Application/Results/OperationResult.cs](src/CadastroAcademico.Application/Results/OperationResult.cs) |
| OperationResult generico | [src/CadastroAcademico.Application/Results/OperationResultOfT.cs](src/CadastroAcademico.Application/Results/OperationResultOfT.cs) |
| Service de aplicacao | [src/CadastroAcademico.Application/Services/CourseService.cs](src/CadastroAcademico.Application/Services/CourseService.cs) |
| DI no Blazor | [src/CadastroAcademico.BlazorApp/Program.cs](src/CadastroAcademico.BlazorApp/Program.cs) |
| Componente Blazor completo | [src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor](src/CadastroAcademico.BlazorApp/Features/FluxoAcademico.razor) |
| ConsoleApp com menus interativos | [src/CadastroAcademico.ConsoleApp/Program.cs](src/CadastroAcademico.ConsoleApp/Program.cs) |

---

## Perguntas Frequentes

**Por que o Domain nao tem `using` para a Application?**

Porque o Domain e a camada mais interna e nao pode depender de nada. Se precisar de algo da Application no Domain, a separacao esta errada.

**Por que o service captura excecoes com `try/catch`?**

Porque o dominio lanca excecoes quando uma invariante e violada (ex.: `ArgumentException`). O service converte essa excecao em um `OperationResult.Failure(...)` com a mensagem, para que a interface possa exibir o erro sem crashar.

**Por que o `InMemoryAcademicStore` e `Singleton` no Blazor?**

Porque queremos que os dados persistam enquanto a aplicacao estiver rodando. Se fosse `Scoped`, uma nova instancia seria criada por conexao e os dados seriam perdidos entre navegacoes.

**Posso colocar a regra de negocio diretamente no service?**

Nao. Regras que definem o que e valido no negocio pertencem ao Domain. O service orquestra o fluxo, mas nao decide o que e valido.
