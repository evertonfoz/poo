-----

## Exercícios de Fundamentos de Programação Estruturada em C\#

A seguir, você encontrará 10 exercícios elaborados com base no material de estudo fornecido, focados em variáveis, tipos de dados primitivos e interação com o console.

-----

#### **Enunciado 1: Cadastro Simples**

Declare quatro variáveis para armazenar as seguintes informações de uma pessoa: o nome completo, a idade, a letra inicial do nome e o seu salário. Inicialize-as com dados de exemplo e, em seguida, exiba cada uma dessas informações no console.

**Resolução:**

```csharp
// Declaração e inicialização das variáveis
string nomeCompleto = "Maria Silva";
int idade = 35;
char letraInicial = 'M';
double salario = 4550.75;

// Exibindo os valores no console
Console.WriteLine("Dados do Funcionário:");
Console.WriteLine(nomeCompleto);
Console.WriteLine(idade);
Console.WriteLine(letraInicial);
Console.WriteLine(salario);
```

-----

#### **Enunciado 2: Mensagem de Boas-Vindas Interativa**

Crie um programa que solicite ao usuário que digite seu nome. O programa deve ler o nome digitado e, em seguida, exibir uma mensagem de boas-vindas personalizada usando o nome fornecido. Utilize a interpolação de strings para formar a mensagem final.

**Resolução:**

```csharp
// Pede o nome ao usuário
Console.WriteLine("Qual é o seu nome?");

// Lê o que o usuário digita e guarda na variável
string nomeUsuario = Console.ReadLine();

// Exibe a mensagem de boas-vindas com interpolação de string
Console.WriteLine($"Olá, {nomeUsuario}! Seja bem-vindo(a) ao sistema.");
```

-----

#### **Enunciado 3: Informações de Produto**

Declare variáveis para armazenar as informações de um produto em um estoque: o nome do produto (`string`), a quantidade de itens (`int`), o preço unitário (`double`) e se o produto está ativo para venda (`bool`). Atribua valores e exiba-os no console usando concatenação.

**Resolução:**

```csharp
// Declaração e inicialização das variáveis do produto
string nomeProduto = "Mouse sem fio";
int quantidade = 150;
double precoUnitario = 89.90;
bool estaAtivo = true;

// Exibindo as informações do produto usando concatenação
Console.WriteLine("Detalhes do Produto:");
Console.WriteLine("Nome: " + nomeProduto);
Console.WriteLine("Quantidade em estoque: " + quantidade);
Console.WriteLine("Preço: " + precoUnitario);
Console.WriteLine("Disponível para venda: " + estaAtivo);
```

-----

#### **Enunciado 4: Escolha de Tipos de Dados**

Para cada item da lista abaixo, declare uma variável em C\# com o nome apropriado e o tipo de dado primitivo mais adequado, conforme apresentado nos materiais.

  * Nota de uma prova (pode ter casas decimais, ex: 8.5)
  * Quantidade de alunos em uma sala (não pode ser um número quebrado)
  * O nome de um curso
  * Status de pagamento (pago ou não)
  * Um único dígito verificador de um documento

**Resolução:**

```csharp
// Declaração das variáveis com os tipos de dados adequados
float notaProva = 8.5f;
int quantidadeAlunos = 42;
string nomeCurso = "Fundamentos de C#";
bool pagamentoEfetuado = false;
char digitoVerificador = '7';

// Apenas para demonstrar a declaração, a exibição é opcional.
Console.WriteLine(notaProva);
Console.WriteLine(quantidadeAlunos);
Console.WriteLine(nomeCurso);
Console.WriteLine(pagamentoEfetuado);
Console.WriteLine(digitoVerificador);
```

-----

#### **Enunciado 5: Formas de Exibição**

Declare duas variáveis do tipo `string`, uma para o primeiro nome e outra para o sobrenome de uma pessoa. Exiba o nome completo no console de duas maneiras diferentes:

1.  Usando o operador de concatenação (`+`).
2.  Usando a interpolação de strings (`$"{}"`).

**Resolução:**

```csharp
string primeiroNome = "Carlos";
string sobrenome = "Andrade";

// 1. Usando concatenação
Console.WriteLine("Usando concatenação: " + primeiroNome + " " + sobrenome);

// 2. Usando interpolação
Console.WriteLine($"Usando interpolação: {primeiroNome} {sobrenome}");
```

-----

#### **Enunciado 6: Corrigindo Erros Comuns**

O bloco de código abaixo contém três erros comuns discutidos nos documentos: o uso de aspas incorretas para `string` e `char` e a atribuição de um valor decimal a uma variável `int`. Identifique-os e corrija o código.

**Código com erros:**

```csharp
// string nome = 'João';
// int valorProduto = 29,99;
// char resposta = "S";
```

**Resolução:**

```csharp
// Correção 1: Strings devem usar aspas duplas.
string nome = "João";

// Correção 2: O tipo 'int' não aceita casas decimais. Usei 'double'.
double valorProduto = 29.99;

// Correção 3: O tipo 'char' armazena um único caractere e usa aspas simples.
char resposta = 'S';

// Exibindo para confirmar
Console.WriteLine(nome);
Console.WriteLine(valorProduto);
Console.WriteLine(resposta);
```

-----

#### **Enunciado 7: Operação de Ponto Flutuante**

Declare uma variável do tipo `double` para armazenar o preço de um produto e uma variável do tipo `int` para a quantidade comprada. Calcule o valor total (`preco * quantidade`) e armazene o resultado em uma variável `double`. Exiba o valor total.

**Resolução:**

```csharp
double preco = 9.99;
int unidades = 3;

double total = preco * unidades;

Console.WriteLine("O valor total da compra é: " + total);
```

-----

#### **Enunciado 8: Variáveis Lógicas (Booleanas)**

Declare uma variável `int` para a idade de um usuário. Em seguida, declare uma variável `bool` chamada `maiorDeIdade` e atribua a ela o resultado de uma comparação (`idade >= 18`). Por fim, exiba o valor da variável `maiorDeIdade`.

**Resolução:**

```csharp
int idade = 20;
bool maiorDeIdade = idade >= 18;

Console.WriteLine("A afirmação 'O usuário é maior de idade' é: " + maiorDeIdade);

// Testando com outro valor
idade = 15;
maiorDeIdade = idade >= 18;
Console.WriteLine("Agora, com 15 anos, a afirmação 'O usuário é maior de idade' é: " + maiorDeIdade);
```

-----

#### **Enunciado 9: Praticando Nomenclatura**

Seguindo a convenção de nomenclatura *CamelCase* sugerida no material, declare variáveis para armazenar as seguintes informações, sem se preocupar em inicializá-las.

  * O nome de usuário para login.
  * A idade do cliente.
  * O valor total do pedido.
  * Se o cadastro está ativo.

**Resolução:**

```csharp
// Declaração de variáveis seguindo a convenção CamelCase
string nomeUsuarioParaLogin;
int idadeCliente;
double valorTotalPedido;
bool cadastroEstaAtivo;

// O enunciado não pede para exibir, apenas declarar corretamente.
Console.WriteLine("Variáveis declaradas com sucesso seguindo as convenções.");
```

-----

#### **Enunciado 10: Precisão de Ponto Flutuante**

Com base na seção "Armadilhas e Erros Comuns", escreva um código que demonstre o erro de precisão ao somar `0.1 + 0.2` e comparar com `0.3` usando o tipo `double`. Exiba o resultado da comparação no console.

**Resolução:**

```csharp
// Demonstração do erro de precisão com double
double a = 0.1 + 0.2;

Console.WriteLine("O valor da soma de 0.1 + 0.2 é: " + a);
// A comparação direta pode resultar em 'false' inesperadamente.
Console.WriteLine("A comparação 'a == 0.3' resulta em: " + (a == 0.3));
Console.WriteLine("Isso ocorre devido à forma como números de ponto flutuante são representados na memória.");
```
