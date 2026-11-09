# Sistema de Gestão de Eventos Acadêmicos — primeiros passos

Este documento explica como preparar uma solução C# no VS Code, organizada em projetos para apresentação, negócio e persistência. A ideia é compreender a estrutura antes de escrever as classes do sistema.

> **Nesta etapa não estamos criando a solução nem implementando funcionalidades.** Os comandos abaixo são para você executar quando estiver pronto; por enquanto, estamos apenas organizando o material de estudo.

## 1. O que vamos construir

Uma **solução** é um arquivo que reúne e organiza projetos relacionados. Cada **projeto** é uma parte compilável da aplicação e pode ter uma responsabilidade própria.

Para começar de forma simples, podemos pensar em três projetos:

| Projeto | Responsabilidade | Exemplo de trabalho futuro |
|---|---|---|
| `SistemaGestaoEventosAcademicos.Apresentacao` | Conversar com a pessoa que usa o sistema: mostrar opções, ler dados e apresentar resultados. | Um menu no terminal para cadastrar ou consultar eventos. |
| `SistemaGestaoEventosAcademicos.Negocio` | Representar conceitos e regras do domínio acadêmico. | Classes como `Evento`, `Participante` e regras como validar se a data de um evento é válida. |
| `SistemaGestaoEventosAcademicos.Persistencia` | Guardar e recuperar dados. | Gravar e ler eventos de um arquivo ou banco de dados, conforme o trabalho pedir. |

Para o primeiro exercício, uma aplicação de **console** é uma opção simples para a apresentação. Se o professor tiver exigido uma interface diferente, use a exigência do trabalho no lugar dessa sugestão.

## 2. O que significa “camadas”

Camadas são divisões de responsabilidade. Não são, por si só, telas ou computadores separados: neste projeto, serão projetos C# diferentes reunidos na mesma solução.

Uma comparação: em um evento, há quem receba as solicitações, quem decida as regras e quem mantenha os registros. A separação ajuda a localizar cada responsabilidade e evita que todo o código fique misturado.

- **Apresentação:** recebe uma ação da pessoa usuária e mostra uma resposta.
- **Negócio:** aplica as regras do sistema e representa os conceitos sobre os quais essas regras atuam.
- **Persistência:** cuida da leitura e da gravação dos dados.

Um fluxo possível será: a apresentação recebe os dados de um novo evento, o negócio verifica as regras pertinentes e a persistência guarda os dados. O detalhe de como essas partes vão colaborar será decidido quando conhecermos os requisitos do trabalho.

## 3. Como as camadas se relacionam neste começo

Os projetos precisam de referências para usar tipos declarados em outros projetos. Sem interfaces ou herança — que ainda não fazem parte do conteúdo estudado — uma estrutura inicial e explícita pode ser:

```text
Apresentacao ──> Negocio
Apresentacao ──> Persistencia
Persistencia  ──> Negocio
```

Isso significa:

- A apresentação pode usar conceitos do negócio e solicitar operações à persistência.
- A persistência pode conhecer os objetos do negócio que precisa guardar ou recuperar.
- O negócio não precisa conhecer a apresentação nem a persistência.

Essa organização evita que as regras do domínio dependam da forma como os dados são mostrados. Como não usaremos interfaces agora, a apresentação pode coordenar chamadas ao negócio e à persistência. É uma estrutura didática inicial; depois, com novos conceitos, o professor poderá ensinar outras formas de desacoplar essas partes.

## 4. Preparar o VS Code

Antes de criar os projetos:

1. Instale o **SDK do .NET**. O SDK inclui as ferramentas para criar, compilar e executar programas C#.
2. No VS Code, instale a extensão **C# Dev Kit** (e as dependências que ela recomendar).
3. Abra no VS Code a pasta que contém os trabalhos da disciplina, por exemplo, `2026_S2`.
4. Abra o terminal integrado pelo menu **Terminal > New Terminal**.
5. Confira se o SDK está disponível executando:

   ```sh
   dotnet --version
   ```

   O comando deve mostrar um número de versão. Se o terminal disser que `dotnet` não foi encontrado, será necessário instalar o SDK ou corrigir a configuração do terminal antes de continuar.

## 5. Comandos para criar a estrutura quando você decidir começar

Execute os comandos abaixo no terminal, a partir da pasta que contém seus trabalhos. Eles criam uma pasta própria, uma solução e três projetos. **Não execute agora se a intenção for apenas estudar a orientação.**

```sh
mkdir SistemaGestaoEventosAcademicos
cd SistemaGestaoEventosAcademicos
mkdir docs

dotnet new sln -n SistemaGestaoEventosAcademicos
dotnet new console -n SistemaGestaoEventosAcademicos.Apresentacao
dotnet new classlib -n SistemaGestaoEventosAcademicos.Negocio
dotnet new classlib -n SistemaGestaoEventosAcademicos.Persistencia
```

O que cada comando faz:

- `mkdir` cria uma pasta.
- `cd` entra na pasta criada; os comandos seguintes serão executados dentro dela.
- `dotnet new sln` cria a solução.
- `dotnet new console` cria um projeto executável de terminal.
- `dotnet new classlib` cria uma biblioteca de classes, apropriada para projetos que fornecem código a outro projeto.

Adicione os projetos à solução:

```sh
dotnet sln add SistemaGestaoEventosAcademicos.Apresentacao/SistemaGestaoEventosAcademicos.Apresentacao.csproj
dotnet sln add SistemaGestaoEventosAcademicos.Negocio/SistemaGestaoEventosAcademicos.Negocio.csproj
dotnet sln add SistemaGestaoEventosAcademicos.Persistencia/SistemaGestaoEventosAcademicos.Persistencia.csproj
```

Depois, registre as referências descritas acima:

```sh
dotnet add SistemaGestaoEventosAcademicos.Apresentacao/SistemaGestaoEventosAcademicos.Apresentacao.csproj reference SistemaGestaoEventosAcademicos.Negocio/SistemaGestaoEventosAcademicos.Negocio.csproj
dotnet add SistemaGestaoEventosAcademicos.Apresentacao/SistemaGestaoEventosAcademicos.Apresentacao.csproj reference SistemaGestaoEventosAcademicos.Persistencia/SistemaGestaoEventosAcademicos.Persistencia.csproj
dotnet add SistemaGestaoEventosAcademicos.Persistencia/SistemaGestaoEventosAcademicos.Persistencia.csproj reference SistemaGestaoEventosAcademicos.Negocio/SistemaGestaoEventosAcademicos.Negocio.csproj
```

Os arquivos `.csproj` descrevem os projetos. O comando `dotnet add ... reference ...` registra que um projeto pode usar os tipos públicos do outro. Não é necessário editar esses arquivos manualmente para fazer essas referências.

## 6. Conferir se deu certo

Na pasta da solução, você poderá verificar e compilar a estrutura com:

```sh
dotnet sln list
dotnet build
```

`dotnet sln list` mostra os projetos incluídos. `dotnet build` compila a solução e informa erros que precisam ser corrigidos. Como os projetos ainda estarão quase vazios, esta primeira compilação serve principalmente para verificar se a estrutura e as referências foram criadas corretamente.

## 7. O que ainda não estamos fazendo

Por enquanto, não estamos definindo classes, atributos, métodos, regras, telas, banco de dados ou formato de arquivo. Também não estamos introduzindo interfaces nem herança. Esses assuntos dependem dos requisitos do professor e serão estudados no momento adequado.

Ao avançar, vale identificar primeiro os conceitos do problema — por exemplo, o que é um evento e quem pode participar — e então decidir quais classes e associações representam esses conceitos. Composição e agregação podem ser usadas quando fizerem sentido para as relações encontradas.

## Vocabulário deste documento

- **SDK:** ferramentas de desenvolvimento do .NET.
- **Solução (`.sln` ou formato de solução compatível):** agrupador de projetos relacionados.
- **Projeto (`.csproj`):** unidade C# que pode ser compilada, como um executável ou uma biblioteca.
- **Console:** forma de interação por texto no terminal.
- **Referência de projeto:** permissão para um projeto usar os tipos públicos de outro.
- **Compilar (`build`):** transformar e verificar o código para confirmar que ele pode ser construído.
