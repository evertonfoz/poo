# UC01 - Cadastrar Estudante - Diagrama de Classes

Primeiro caso de uso identificado para implementacao: **UC01 - Cadastrar Estudante**.

```mermaid
classDiagram
    direction LR

    class OperadorIncubadora {
        <<actor>>
    }

    class CadastroEstudanteController {
        +CadastrarEstudante(nome: string, matricula: string, email: string, curso: string) Estudante
    }

    class EstudanteValidator {
        +ValidarCamposObrigatorios(nome: string, matricula: string, email: string, curso: string) void
    }

    class Estudante {
        +Nome: string
        +Matricula: string
        +Email: string
        +Curso: string
        +Estudante(nome: string, matricula: string, email: string, curso: string)
    }

    class EstudanteRepository {
        +Adicionar(estudante: Estudante) void
        +BuscarPorMatricula(matricula: string) Estudante
    }

    class ResultadoCadastro {
        +Sucesso: bool
        +Mensagem: string
    }

    OperadorIncubadora --> CadastroEstudanteController : solicita cadastro
    CadastroEstudanteController ..> EstudanteValidator : valida dados
    CadastroEstudanteController ..> Estudante : cria instancia
    CadastroEstudanteController ..> EstudanteRepository : persiste
    CadastroEstudanteController --> ResultadoCadastro : confirma resultado
    EstudanteRepository "1" o-- "0..*" Estudante : armazena
```

## Classes relacionadas ao UC01

- OperadorIncubadora (ator que dispara o caso de uso)
- CadastroEstudanteController (orquestra o fluxo)
- EstudanteValidator (garante campos obrigatorios)
- Estudante (entidade de dominio criada)
- EstudanteRepository (persistencia/colecao de estudantes)
- ResultadoCadastro (retorno de confirmacao para o operador)
