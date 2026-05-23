# Value Objects e Invariantes

## O que e um Value Object

Value Object e um objeto definido pelos valores que carrega, nao por identidade propria.

Diferente de uma Entity, o Value Object nao tem `Id`. Ele existe para representar um conceito descritivo e garantir que esse conceito seja sempre valido.

Exemplo do mundo real: um "periodo academico" como "1o periodo" ou "2026/1" nao precisa ter um identificador unico no banco de dados. O que importa e que o valor em si seja valido (nao pode ser vazio, nao pode ser um texto sem sentido).

---

## Value Object deste projeto

Este projeto possui um Value Object no dominio:

| Value Object     | Arquivo                                            | Papel                                         |
|------------------|----------------------------------------------------|-----------------------------------------------|
| `PerfilAcademico`| `src/Domain/ValueObjects/PerfilAcademico.cs`       | Agrupa os dados academicos do aluno           |

O `PerfilAcademico` encapsula dois campos que pertencem ao contexto academico do aluno: `RegistroAcademico` e `Periodo`.

---

## Implementacao do PerfilAcademico

```csharp
public class PerfilAcademico
{
    public string RegistroAcademico { get; private set; }
    public string Periodo { get; private set; }

    public PerfilAcademico(string registroAcademico, string periodo)
    {
        RegistroAcademico = ValidarRegistroAcademico(registroAcademico);
        Periodo = ValidarPeriodo(periodo);
    }

    public void AtualizarPeriodo(string periodo)
    {
        Periodo = ValidarPeriodo(periodo);
    }

    private static string ValidarRegistroAcademico(string registroAcademico)
    {
        if (string.IsNullOrWhiteSpace(registroAcademico))
        {
            throw new ArgumentException("Registro academico e obrigatorio.", nameof(registroAcademico));
        }

        return registroAcademico.Trim().ToUpperInvariant();
    }

    private static string ValidarPeriodo(string periodo)
    {
        if (string.IsNullOrWhiteSpace(periodo))
        {
            throw new ArgumentException("Periodo e obrigatorio.", nameof(periodo));
        }

        return periodo.Trim();
    }
}
```

---

## Por que PerfilAcademico e um Value Object, nao uma Entity

| Criterio                     | Entity (Aluno)        | Value Object (PerfilAcademico)     |
|------------------------------|-----------------------|------------------------------------|
| Precisa de `Id`?             | Sim                   | Nao                                |
| Sobrevive sozinho no sistema?| Sim                   | Nao — esta sempre ligado a um Aluno |
| Mudou o valor, mudou o objeto? | Nao (mesmo `Id`)    | Sim (e um conjunto diferente de valores) |

Se dois alunos diferentes tivessem os mesmos dados de `PerfilAcademico` (ex.: mesmo RA e mesmo periodo), seria um problema de negocio — mas isso e validado no servico de aplicacao ao verificar duplicidade de `RegistroAcademico` entre alunos.

---

## Como PerfilAcademico e usado na Entity Aluno

```csharp
public class Aluno
{
    public PerfilAcademico PerfilAcademico { get; private set; }

    public Aluno(string nome, string email, PerfilAcademico perfilAcademico)
    {
        // ...
        PerfilAcademico = perfilAcademico ?? throw new ArgumentNullException(
            nameof(perfilAcademico),
            "Perfil academico e obrigatorio."
        );
    }
}
```

A Entity recebe o Value Object ja construido e validado. Isso mantém a construcao de `PerfilAcademico` separada da construcao de `Aluno`.

No servico de aplicacao:

```csharp
// StudentService.cs
var profile = new PerfilAcademico(input.RegistroAcademico, input.Periodo);
var student = new Aluno(input.Nome, input.Email, profile);
```

---

## O que sao Invariantes

Invariante e uma regra que deve ser sempre verdadeira para que o objeto exista em estado valido.

Exemplos de invariantes neste projeto:

| Classe           | Invariante                                           |
|------------------|------------------------------------------------------|
| `Aluno`          | Nome e email nao podem ser vazios                   |
| `Curso`          | Nome, sigla e carga horaria sao obrigatorios         |
| `Curso`          | Carga horaria deve ser maior que zero                |
| `PerfilAcademico`| Registro academico e periodo nao podem ser vazios    |
| `Matricula`      | Apenas matriculas ativas podem ser canceladas        |
| `Matricula`      | Apenas matriculas ativas podem ser concluidas        |

As invariantes sao verificadas no construtor ou nos metodos da propria classe, nao na interface.

---

## Por que colocar invariantes no dominio

Imagine que a regra "carga horaria maior que zero" estivesse apenas na tela Blazor.

Quando o ConsoleApp fosse implementado, a regra precisaria ser reimplementada la tambem.

Quando uma API fosse adicionada futuramente, a regra precisaria ser implementada novamente.

Se a regra estiver no dominio (em `Curso.ValidarCargaHoraria()`), ela e verificada automaticamente toda vez que um `Curso` e criado, independente da interface que iniciou a operacao.

---

## Padrao de validacao adotado

Metodos privados estaticos de validacao, chamados no construtor:

```csharp
private static int ValidarCargaHoraria(int cargaHoraria)
{
    if (cargaHoraria <= 0)
    {
        throw new ArgumentException(
            "Carga horaria deve ser maior que zero.",
            nameof(cargaHoraria)
        );
    }

    return cargaHoraria;
}
```

E o construtor usa assim:

```csharp
public Curso(string nome, string sigla, int cargaHoraria)
{
    Id = ++_ultimoId;
    Nome = ValidarNome(nome);
    Sigla = ValidarSigla(sigla);
    CargaHoraria = ValidarCargaHoraria(cargaHoraria);
}
```

Beneficio: se a validacao precisar mudar, ha um unico ponto para alterar.

---

## Resumo

| Conceito         | Definicao                                          | Exemplo no projeto          |
|------------------|----------------------------------------------------|-----------------------------|
| Value Object     | Objeto definido por valores, sem identidade propria | `PerfilAcademico`           |
| Invariante       | Regra sempre verdadeira para que o objeto seja valido | Nome obrigatorio, CH > 0  |
| Validacao        | Verificacao de invariante no construtor ou metodo  | `ValidarNome()`, `Cancelar()` |

---

## Leitura complementar

- [02_entities_na_pratica.md](02_entities_na_pratica.md): entenda como as entities protegem estado e comportamento.
- [04_matricula_e_transicao_de_estado.md](04_matricula_e_transicao_de_estado.md): veja invariantes aplicadas ao ciclo de vida da Matricula.
- [Docs/03-organizacao-arquitetura-projetos/06_fundamentacao/07_entities_valueobjects_enums_na_pratica.md](../03-organizacao-arquitetura-projetos/06_fundamentacao/07_entities_valueobjects_enums_na_pratica.md): comparativo entre Entity, Value Object e Enum com exercicio.
