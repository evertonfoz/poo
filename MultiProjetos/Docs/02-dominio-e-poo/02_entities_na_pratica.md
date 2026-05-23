# Entities na Pratica

## O que e uma Entity

Entity e um objeto com identidade propria ao longo do tempo.

Isso significa que mesmo que seus dados mudem (nome, email, status), o objeto continua sendo o mesmo no sistema porque possui um `Id` unico que o identifica.

Exemplo do mundo real: um aluno pode trocar o email, mudar de periodo e continuar sendo o mesmo aluno. O que o identifica no sistema e o `Id`, nao o conjunto de valores.

---

## Entities deste projeto

Este projeto possui tres entities no dominio:

| Entity      | Arquivo                                      | Papel                                      |
|-------------|----------------------------------------------|--------------------------------------------|
| `Curso`     | `src/Domain/Entities/Curso.cs`               | Representa um curso academico              |
| `Aluno`     | `src/Domain/Entities/Aluno.cs`               | Representa um aluno do sistema             |
| `Matricula` | `src/Domain/Entities/Matricula.cs`           | Representa o vinculo entre aluno e curso   |

---

## Padrao de identidade

Todas as entities neste projeto usam um `Id` inteiro gerado automaticamente em memoria:

```csharp
public class Aluno
{
    private static int _ultimoId;

    public int Id { get; }

    public Aluno(string nome, string email, PerfilAcademico perfilAcademico)
    {
        Id = ++_ultimoId;
        // ...
    }
}
```

Pontos importantes:

1. `Id` nao tem `set`, nem `private set`. Ele e atribuido apenas uma vez no construtor.
2. `_ultimoId` e `static`: e compartilhado entre todas as instancias da classe.
3. `++_ultimoId`: incrementa antes de atribuir, garantindo que o primeiro Id seja 1.

---

## Padrao de encapsulamento

As entities usam `private set` para proteger as propriedades de alteracoes externas:

```csharp
public class Aluno
{
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public PerfilAcademico PerfilAcademico { get; private set; }
}
```

Isso significa:

- Qualquer codigo fora da classe pode **ler** `Nome`, `Email` e `PerfilAcademico`.
- Apenas codigo **dentro da propria classe** pode atribuir novo valor a essas propriedades.

---

## Validacao no construtor

As entities validam os dados recebidos logo no construtor, garantindo que nenhum objeto invalido seja criado:

```csharp
public Aluno(string nome, string email, PerfilAcademico perfilAcademico)
{
    Id = ++_ultimoId;
    Nome = ValidarNome(nome);
    Email = ValidarEmail(email);
    PerfilAcademico = perfilAcademico ?? throw new ArgumentNullException(...);
}

private static string ValidarNome(string nome)
{
    if (string.IsNullOrWhiteSpace(nome))
    {
        throw new ArgumentException("Nome do aluno e obrigatorio.", nameof(nome));
    }

    return nome.Trim();
}
```

Resultado: e impossivel criar um `Aluno` sem nome. A regra esta no objeto, nao na interface.

---

## Protecao de colecoes internas

Quando uma entity possui uma lista de associacoes (ex.: Curso tem Matriculas), a lista interna e privada e exposta apenas para leitura:

```csharp
public class Curso
{
    private readonly List<Matricula> _matriculas = new();

    public IReadOnlyCollection<Matricula> Matriculas => _matriculas.AsReadOnly();
}
```

Por que isso importa:

- Codigo externo nao pode chamar `curso.Matriculas.Add(...)` diretamente.
- A unica forma de adicionar uma matricula e pelo metodo de dominio `Curso.MatricularAluno(...)`.
- A regra de negocio fica controlada dentro da entity.

---

## Comportamento de negocio

Entities nao sao apenas estruturas de dados. Elas carregam comportamento:

```csharp
public class Curso
{
    public Matricula MatricularAluno(Aluno aluno)
    {
        var matriculaAtiva = _matriculas.Any(m => m.Aluno.Id == aluno.Id && m.Status == StatusMatricula.Ativa);
        if (matriculaAtiva)
        {
            throw new InvalidOperationException("Aluno ja possui matricula ativa neste curso.");
        }

        var matricula = new Matricula(aluno, this, DateTime.UtcNow);
        _matriculas.Add(matricula);
        aluno.RegistrarMatricula(matricula);

        return matricula;
    }
}
```

Este metodo:

1. Verifica se ja existe matricula ativa para esse aluno nesse curso.
2. Cria a matricula.
3. Associa a matricula ao aluno.
4. Retorna a matricula criada.

Toda essa logica esta no dominio, nao no servico de aplicacao nem na interface.

---

## Resumo do padrao de Entity

| Caracteristica         | Como aparece no codigo                              |
|------------------------|-----------------------------------------------------|
| Identidade             | `public int Id { get; }` + `++_ultimoId`            |
| Encapsulamento         | `public string Nome { get; private set; }`          |
| Colecao protegida      | `private List<T>` + `IReadOnlyCollection<T>`        |
| Validacao no construtor| Metodos `Validar...()` chamados no construtor       |
| Comportamento          | Metodos como `MatricularAluno`, `Cancelar`, `Concluir` |

---

## Regra didatica

Entity nao e apenas um "saco de dados". Se uma classe so tem propriedades e nenhuma regra de negocio, ela provavelmente nao e uma entity — e um DTO.

Entity tem comportamento que define como o estado pode mudar e quais transicoes sao validas.

---

## Leitura complementar

- [03_value_objects_e_invariantes.md](03_value_objects_e_invariantes.md): entenda a diferenca entre Entity e Value Object.
- [04_matricula_e_transicao_de_estado.md](04_matricula_e_transicao_de_estado.md): veja como a entity Matricula controla seu ciclo de vida.
- [Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/07_entities_valueobjects_enums_na_pratica.md](../03-organizacao-arquitetura-projetos/06_fundamentacao/07_entities_valueobjects_enums_na_pratica.md): estudo comparativo com exercicio.
