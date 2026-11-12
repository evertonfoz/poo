# Registro de Implementação — Passo 1: Setup da Solução e Estrutura dos Projetos

Este documento formaliza o setup inicial da infraestrutura de código da solução **SistemaGestaoEventosAcademicos**, estabelecendo as bases arquiteturais, configurações de compilação e organização de pastas para as próximas sessões de desenvolvimento.

---

## 1. Contexto e Objetivo da Sessão

- **Data da Execução:** 12/11/2026
- **Ambiente de Desenvolvimento:** macOS, SDK .NET 10 (`10.0.103`), C# 13
- **Objetivo:** Estabelecer a infraestrutura da solução em três camadas estritas com dependências unidirecionais, garantindo a integridade dos arquivos de projeto, diretórios e compilação limpa (0 warnings, 0 errors).

---

## 2. Decisões Técnicas e Arquiteturais

### 2.1. Formato da Solução (`.slnx`)
- Mantido e priorizado o formato moderno **XML Solution (`SistemaGestaoEventosAcademicos.slnx`)**, padrão oficial do ecossistema .NET moderno para agrupamento limpo e declarativo de projetos.
- Qualquer arquivo `.sln` legado foi descartado, respeitando a solução já inicializada no repositório.

### 2.2. Consulta de Documentação via MCP Server `context7`
- A documentação oficial da Microsoft (`/dotnet/docs`) foi consultada para validar o suporte do .NET SDK ao formato `.slnx` e a aplicação de configurações compartilhadas via `Directory.Build.props`.

### 2.3. Configuração Centralizada com `Directory.Build.props`
Criado o arquivo `Directory.Build.props` na raiz da solução para padronizar todos os projetos sem duplicação de diretivas nos `.csproj`:

```xml
<Project>
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <EnableSourceControlManagerQueries>false</EnableSourceControlManagerQueries>
    <BuildInParallel>false</BuildInParallel>
  </PropertyGroup>
</Project>
```

- **`TargetFramework`:** `net10.0` (compatível com a versão instalada no ambiente).
- **`Nullable` & `ImplicitUsings`:** Habilitados para modernidade, segurança de tipos e redução de boilerplate.
- **`EnableSourceControlManagerQueries=false`:** Evita buscas recursivas no Git fora do escopo do repositório/workspace.
- **`BuildInParallel=false`:** Garante estabilidade nos processos de compilação e restauração em ambientes restritos/sandboxed.

---

## 3. Arquitetura em 3 Camadas e Regras de Dependência

A arquitetura respeita o princípio de referências unidirecionais estritas:

```
[ SistemaGestaoEventosAcademicos.Apresentacao ]
          │                           │
          ▼                           ▼
[ SistemaGestaoEventosAcademicos.Persistencia ]
          │
          ▼
[ SistemaGestaoEventosAcademicos.Negocio ] (Isolado / Zero dependências externas)
```

1. **`SistemaGestaoEventosAcademicos.Negocio`:**
   - Contém as entidades de domínio, enums e exceções de negócio.
   - Não faz referência a nenhuma outra camada da solução nem a pacotes externos de infraestrutura.
2. **`SistemaGestaoEventosAcademicos.Persistencia`:**
   - Responsável pelos repositórios em memória.
   - Referencia apenas `SistemaGestaoEventosAcademicos.Negocio`.
3. **`SistemaGestaoEventosAcademicos.Apresentacao`:**
   - Console CLI e entry point da aplicação.
   - Referencia `SistemaGestaoEventosAcademicos.Negocio` e `SistemaGestaoEventosAcademicos.Persistencia`.

---

## 4. Estrutura de Diretórios e Arquivos Gerados

Os arquivos de modelo padrão (`Class1.cs`) foram removidos e a seguinte estrutura de pastas foi criada com `.gitkeep`:

```text
SistemaGestaoEventosAcademicos/
├── Directory.Build.props
├── SistemaGestaoEventosAcademicos.slnx
│
├── SistemaGestaoEventosAcademicos.Negocio/
│   ├── Entidades/
│   ├── Enums/
│   ├── Excecoes/
│   └── SistemaGestaoEventosAcademicos.Negocio.csproj
│
├── SistemaGestaoEventosAcademicos.Persistencia/
│   ├── Repositorios/
│   └── SistemaGestaoEventosAcademicos.Persistencia.csproj
│
├── SistemaGestaoEventosAcademicos.Apresentacao/
│   ├── Menus/
│   ├── Utilidades/
│   ├── Program.cs
│   └── SistemaGestaoEventosAcademicos.Apresentacao.csproj
│
└── docs/
    ├── 01-primeiros-passos-e-camadas.md
    ├── requisitos/
    ├── projeto/
    └── implementacao/
        └── 01-setup-da-solucao-e-estrutura.md
```

---

## 5. Validação e Testes de Sanidade

1. **Restauração e Compilação:**
   ```sh
   dotnet clean SistemaGestaoEventosAcademicos.slnx -m:1
   dotnet build SistemaGestaoEventosAcademicos.slnx -m:1
   ```
   **Resultado:** Compilação com êxito (0 erros, 0 avisos).

2. **Execução do Entry Point (`Program.cs`):**
   ```sh
   dotnet run --project SistemaGestaoEventosAcademicos.Apresentacao
   ```
   **Saída confirmada:**
   ```text
   === Sistema de Gestão de Eventos Acadêmicos ===
   Ambiente inicializado com sucesso em .NET 10.
   ```

---

## 6. Próximos Passos (Passo 2)

Na próxima sessão de desenvolvimento, iniciaremos a implementação do núcleo de domínio em `SistemaGestaoEventosAcademicos.Negocio` com base no DCD (`docs/projeto/01-diagrama-de-classes-de-projeto.md`) e nas Regras de Negócio (`docs/requisitos/02-regras-de-negocio.md`):

1. **Enums de Domínio (`Enums/`):**
   - `TipoEvento`, `StatusEvento`, `TipoAtividade`, `StatusInscricao`.
2. **Exceções de Domínio (`Excecoes/`):**
   - `ViolacaoRegraNegocioException` e especializações (ex.: `CapacidadeExcedidaException`, `TransicaoStatusInvalidaException`, etc.).
3. **Entidades de Domínio (`Entidades/`):**
   - Implementação rica (encapsulamento estrito com métodos de negócio) de `Evento`, `Atividade`, `Participante` e `Inscricao`.

