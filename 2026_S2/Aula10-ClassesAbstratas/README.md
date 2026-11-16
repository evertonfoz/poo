# Aula 10 - Herança e Polimorfismo em C# (Partes 2 e 3)

Este repositório contém a implementação completa das atividades práticas da **Aula 10 de Programação Orientada a Objetos em C#/.NET**, abrangendo:
- **Parte 2:** Classes e métodos abstratos, modificadores `virtual`, `override`, `new` e reaproveitamento com `base(...)`.
- **Parte 3:** Coleções tipadas (`List<T>`), percursos (`foreach`), buscas (`Find`), filtros (`FindAll`), remoções (`Remove`), ordenação (`Sort`), contagens de tipos (`is`) e despacho polimórfico.

---

## 📁 Estrutura de Diretórios e Soluções

```text
Aula10-ClassesAbstratas/
│
├── Atividade01-ReservasEspacos/
│   ├── Atividade01-ReservasEspacos.sln
│   ├── Atividade01-ReservasEspacos.csproj
│   ├── Program.cs
│   └── Modelo/
│       ├── Espaco.cs                 (Classe Abstrata com método abstrato CalcularCusto)
│       ├── SalaEstudo.cs             (Override: horas * ValorHora)
│       ├── Auditorio.cs              (Override: horas * ValorHora + TaxaPreparacao)
│       └── LaboratorioInformatica.cs (Override: horas * QtdComputadores * ValorHoraPorComputador)
│
├── Atividade02-PersonagensJogo/
│   ├── Atividade02-PersonagensJogo.sln
│   ├── Atividade02-PersonagensJogo.csproj
│   ├── Program.cs
│   └── Modelo/
│       ├── Personagem.cs             (Classe Concreta com método virtual Atacar)
│       ├── Guerreiro.cs              (Override somando BonusEspada)
│       ├── Mago.cs                   (Override com consumo de mana e bônus mágico)
│       ├── Arqueiro.cs               (Override e sobrecarga Atacar(bool ataqueCritico))
│       └── Paladino.cs               (Herda de Guerreiro com BonusSagrado)
│
└── Atividade03-ProcessamentoDocumentos/
    ├── Atividade03-ProcessamentoDocumentos.sln
    ├── Atividade03-ProcessamentoDocumentos.csproj
    ├── Program.cs
    └── Modelo/
        ├── Documento.cs              (Classe Abstrata com Processar() abstrato e ExibirResumo() virtual)
        ├── RelatorioFinanceiro.cs    (Override estendendo base.ExibirResumo())
        ├── DocumentoTexto.cs         (Herda ExibirResumo() diretamente sem sobrescrever)
        └── Planilha.cs               (Ocultação de ExibirResumo() via modificador 'new')
```

---

## 📋 Resumo das Implementações da Parte 3

### Atividade 1 — Reservas de Espaços: Administrando uma Coleção
- **Coleção:** `List<Espaco>` inicializada com 6 espaços (2 salas de estudo, 2 auditórios e 2 laboratórios de informática com identificações e valores distintos).
- **Operações:**
  - `Count`: exibição da quantidade total de espaços cadastrados.
  - `foreach`: exibição de informações gerais e cálculo individual de 4 horas para cada espaço.
  - **Totalização:** acumulador `decimal` somando o custo de 4 horas de todos os espaços.
  - `Find()`: busca de espaço por `Identificacao` (com demonstração de caso existente e caso inexistente tratando `null`).
  - **Maior custo:** algoritmo iterativo determinando o espaço de maior custo para 4 horas.

### Atividade 2 — Personagens de Jogo: Filtrar, Buscar e Remover
- **Coleção:** `List<Personagem>` com 8 personagens distribuídos entre todas as subclasses (`Personagem`, `Guerreiro`, `Mago`, `Arqueiro` e `Paladino`).
- **Operações:**
  - `foreach`: listagem de status e execução polimórfica de `Atacar()` em lote (com observação do consumo de mana dos magos).
  - `Find()`: busca de personagem por nome (caso positivo e negativo com verificação segura de `null`).
  - **Comparar:** localização do personagem com maior `ForcaAtaque` básica (distinguindo-a do dano especializado).
  - `FindAll()`: filtragem de sobreviventes (`PontosVida > 0`), demonstrando que uma nova lista com referências é criada.
  - `Remove()`: remoção fora do loop por nome com exibição da contagem antes e depois, provando que o objeto removido da lista principal continua vivo na lista de sobreviventes.
  - `is`: contagem e discriminação das classes concretas no grupo.

### Atividade 3 — Documentos Digitais: Processamento em Lote e Investigação Override vs New
- **Coleção:** `List<Documento>` com 9 documentos (3 relatórios financeiros, 3 documentos de texto e 3 planilhas), com repetição de autor e datas variadas.
- **Operações:**
  - `foreach`: processamento em lote chamando `Processar()` e `ExibirResumo()`.
  - `Find()`: localização de documento por título com tratamento de nulo.
  - `FindAll()`: extração de lista de documentos de um autor específico.
  - `is`: contagem de cada tipo concreto de documento.
  - `Remove()`: remoção por título com atualização de `Count`.
  - `Sort()`: ordenação cronológica da lista original por `DataCriacao` (mais antigo ao mais recente) com expressão lambda comparando datas.
  - **Experimento Override e New em Coleção:**
    - **Etapa A:** chamada via referência da classe base (`Documento`): `Planilha` executa o método da base devido ao `new`.
    - **Etapa B:** chamada via referência concreta (`Planilha`): `Planilha` executa seu método próprio especializado.
    - **Etapa C:** comparativo teórico e conceitual do efeito de `override` vs `new`.

---

## 📊 Quadro de Previsão e Observação (Atividade 3)

| Objeto Real | ExibirResumo via `Documento` (Etapa A) | ExibirResumo via Tipo Concreto (Etapa B) |
| :--- | :--- | :--- |
| **RelatorioFinanceiro** | Resumo estendido (Subclasse via `override`) | Resumo estendido (Subclasse via `override`) |
| **DocumentoTexto** | Resumo padrão herdado da base `Documento` | Resumo padrão herdado da base `Documento` |
| **Planilha com `new`** | Resumo padrão da base `Documento` *(ocultação ignorada)* | `«Resumo da planilha: ...»` (Subclasse `Planilha`) |
| **Planilha com `override`** | `«Resumo da planilha: ...»` (Subclasse via `override`) | `«Resumo da planilha: ...»` (Subclasse via `override`) |

---

## 🧠 Questões para Reflexão Respondidas

### Atividade 1
1. **Por que uma única `List<Espaco>` pode armazenar objetos de três subclasses?**  
   *R:* Porque as três subclasses herdam diretamente de `Espaco` (relação "é-um"). Pelo princípio da substituição, uma referência de classe base pode apontar para qualquer instância de classe derivada.
2. **Por que `CalcularCusto(4)` executa a regra apropriada sem uma sequência de `if`?**  
   *R:* Devido ao polimorfismo dinâmico. O método `CalcularCusto` é abstrato na base e sobrescrito com `override` nas subclasses. O runtime consulta a tabela virtual (vtable) da instância real.
3. **Qual é a diferença entre percorrer toda a coleção e localizar apenas um elemento com `Find()`?**  
   *R:* O percurso com `foreach` percorre todos os elementos incondicionalmente. O `Find()` interrompe a busca e retorna o primeiro elemento compatível, ou `null` se não encontrar.
4. **O que pode acontecer se o resultado de `Find()` for utilizado sem verificar se é `null`?**  
   *R:* Ocorre uma exceção `NullReferenceException` em tempo de execução se nenhum item corresponder ao critério de busca.

### Atividade 2
1. **Por que um percurso de `List<Personagem>` permite executar `Atacar()` com implementações diferentes?**  
   *R:* Porque o método foi declarado como `virtual` na base e `override` nas subclasses, permitindo resolução dinâmica em tempo de execução.
2. **Qual é a diferença entre `Find()` e `FindAll()`?**  
   *R:* `Find()` retorna apenas o primeiro elemento coincidente (ou `null`). `FindAll()` percorre a lista inteira e retorna uma nova `List<T>` com todos os elementos que satisfazem o predicado.
3. **O que acontece com um objeto quando removemos sua referência de uma lista, mas ele continua referenciado em outra?**  
   *R:* O objeto continua existindo normalmente na memória Heap enquanto houver referências ativas apontando para ele; ele não é descartado pelo Garbage Collector.
4. **Por que uma verificação `is Mago` é útil para contar tipos, mas não para chamar `Atacar()`?**  
   *R:* Para contagem estatística é necessário inspecionar os tipos concretos. Para a chamada de `Atacar()`, o polimorfismo cuida do despacho automático sem necessidade de verificação manual.

### Atividade 3
1. **Por que `Documento` pode ser o tipo da lista mesmo sendo uma classe abstrata?**  
   *R:* Porque a lista armazena referências para objetos; embora `Documento` não possa ser instanciado diretamente, todas as instâncias adicionadas pertencem a classes concretas derivadas.
2. **Qual é a diferença entre o despacho de `Processar()` e a ocultação de `ExibirResumo()` em Planilha?**  
   *R:* `Processar()` é polimórfico (vtable), sempre chamando a implementação da classe real. `ExibirResumo()` em `Planilha` usa `new`, realizando ligação estática baseada no tipo da variável de referência.
3. **Quais são as diferenças de resultado entre o método ocultado com `new` e o sobrescrito com `override`?**  
   *R:* `override` altera a tabela virtual do objeto, mantendo o comportamento especializado mesmo através da classe base. `new` apenas esconde o método na subclasse, fazendo com que referências do tipo base executem o método da base.
4. **Por que a ordem dos documentos muda após `Sort()`, mas a implementação de seus métodos não muda?**  
   *R:* O `Sort()` altera unicamente a posição dos ponteiros na coleção interna da lista. Os objetos no Heap continuam idênticos, com seus tipos, atributos e tabelas de métodos virtuais inalterados.

---

## 🚀 Como Executar

Cada projeto pode ser executado individualmente via terminal:

```bash
# Atividade 1
cd Aula10-ClassesAbstratas/Atividade01-ReservasEspacos && dotnet run

# Atividade 2
cd Aula10-ClassesAbstratas/Atividade02-PersonagensJogo && dotnet run

# Atividade 3
cd Aula10-ClassesAbstratas/Atividade03-ProcessamentoDocumentos && dotnet run
```
