-----

## Exercícios Interativos de Fundamentos de C\#

A seguir, você encontrará 10 novos exercícios que focam na interação com o usuário, utilizando os conceitos de variáveis, tipos de dados e entrada/saída via console.

-----

#### **Enunciado 1: Coletor de Dados Pessoais**

Crie um programa que faça três perguntas ao usuário: seu primeiro nome (`string`), sua idade (`int`) e sua altura em metros (`float`). Após coletar as respostas, exiba uma mensagem única no console, usando interpolação de strings, com todas as informações coletadas.

**Resolução:**

```csharp
Console.WriteLine("Por favor, digite seu primeiro nome:");
string primeiroNome = Console.ReadLine();

Console.WriteLine("Agora, digite sua idade:");
// Para este exercício, vamos assumir que o usuário digita um número válido.
int idade = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Por fim, digite sua altura em metros (ex: 1.80):");
// E que também digita um float válido.
float altura = Convert.ToSingle(Console.ReadLine());

Console.WriteLine($"Resumo do Cadastro: Nome: {primeiroNome}, Idade: {idade} anos, Altura: {altura}m.");
```

-----

#### **Enunciado 2: Verificador de Status de Jogo**

Pergunte ao usuário o nome de seu personagem em um jogo. Em seguida, pergunte se o personagem está vivo, esperando uma resposta "true" ou "false". Armazene o status em uma variável `bool` e exiba uma mensagem confirmando o nome e o status de vida do personagem.

**Resolução:**

```csharp
Console.WriteLine("Qual é o nome do seu personagem?");
string nomePersonagem = Console.ReadLine();

Console.WriteLine($"O personagem {nomePersonagem} está vivo? (responda com 'true' ou 'false')");
// Assume-se que o usuário digitará 'true' ou 'false' corretamente.
bool estaVivo = Convert.ToBoolean(Console.ReadLine());

Console.WriteLine($"Personagem: {nomePersonagem} | Status Vivo: {estaVivo}");
```

-----

#### **Enunciado 3: Calculadora de Preço Total**

Solicite ao usuário o nome de um produto. Depois, peça o preço unitário desse produto (`double`) e a quantidade de unidades que ele deseja comprar (`int`). Calcule o preço total (preço \* quantidade) e mostre o resultado em uma frase completa, como: "O valor total para 3 unidades de 'Teclado' é R$ 299.70".

**Resolução:**

```csharp
Console.WriteLine("Digite o nome do produto:");
string nomeProduto = Console.ReadLine();

Console.WriteLine($"Digite o preço unitário do(a) {nomeProduto}:");
double precoUnitario = Convert.ToDouble(Console.ReadLine());

Console.WriteLine($"Quantas unidades de {nomeProduto} você deseja?");
int quantidade = Convert.ToInt32(Console.ReadLine());

double total = precoUnitario * quantidade;

Console.WriteLine($"O valor total para {quantidade} unidades de '{nomeProduto}' é R$ {total}");
```

-----

#### **Enunciado 4: Construtor de Frases**

Peça ao usuário para digitar, separadamente, um substantivo, um verbo e um adjetivo. Use concatenação de strings para juntar as três palavras em uma frase e exibi-la no console.

**Resolução:**

```csharp
Console.WriteLine("Digite um substantivo (ex: cachorro):");
string substantivo = Console.ReadLine();

Console.WriteLine("Digite um verbo (ex: corre):");
string verbo = Console.ReadLine();

Console.WriteLine("Digite um adjetivo (ex: rápido):");
string adjetivo = Console.ReadLine();

string frase = "O " + substantivo + " " + verbo + " " + adjetivo + ".";
Console.WriteLine(frase);
```

-----

#### **Enunciado 5: Verificação de Acesso**

Pergunte ao usuário qual é a sua idade. Armazene o valor em uma variável `int`. Crie uma variável `bool` chamada `temAcesso` que será `true` se a idade for 18 ou mais, e `false` caso contrário. Exiba no console a frase "Acesso permitido: " seguida do valor da variável `temAcesso`.

**Resolução:**

```csharp
Console.WriteLine("Qual é a sua idade?");
int idade = Convert.ToInt32(Console.ReadLine());

bool temAcesso = idade >= 18;

Console.WriteLine("Acesso permitido: " + temAcesso);
```

-----

#### **Enunciado 6: Cadastro de Caractere Inicial**

Solicite ao usuário que digite seu nome completo. Em seguida, peça para que ele digite a letra inicial do seu primeiro nome. Armazene a letra em uma variável do tipo `char` e exiba uma mensagem confirmando o nome e a inicial informada.

**Resolução:**

```csharp
Console.WriteLine("Digite seu nome completo:");
string nomeCompleto = Console.ReadLine();

Console.WriteLine("Agora, por favor, digite a letra inicial do seu primeiro nome:");
// Assume-se que o usuário digitará um único caractere.
char letraInicial = Convert.ToChar(Console.ReadLine());

Console.WriteLine($"Cadastro confirmado para {nomeCompleto}. A inicial informada foi '{letraInicial}'.");
```

-----

#### **Enunciado 7: Duas Formas de Apresentação**

Peça ao usuário seu nome e sua cidade natal. Apresente essas informações no console de duas maneiras: primeiro, usando concatenação com o operador `+`, e segundo, usando interpolação de strings com `$`.

**Resolução:**

```csharp
Console.WriteLine("Qual é o seu nome?");
string nome = Console.ReadLine();

Console.WriteLine("Qual é a sua cidade natal?");
string cidade = Console.ReadLine();

// Usando concatenação
Console.WriteLine("Apresentação 1: " + nome + " é de " + cidade + ".");

// Usando interpolação
Console.WriteLine($"Apresentação 2: {nome} é de {cidade}.");
```

-----

#### **Enunciado 8: Simulação de Login Simples**

Peça um nome de usuário e, em seguida, uma senha. Armazene ambos em variáveis do tipo `string`. Exiba uma mensagem no console dizendo "Login recebido para o usuário [nome do usuário]. A senha será processada com segurança."

**Resolução:**

```csharp
Console.WriteLine("Digite seu nome de usuário:");
string usuario = Console.ReadLine();

Console.WriteLine("Digite sua senha:");
string senha = Console.ReadLine();

Console.WriteLine($"Login recebido para o usuário {usuario}. A senha será processada com segurança.");
```

-----

#### **Enunciado 9: Coletor de Preferências**

Pergunte ao usuário o nome de sua banda favorita (`string`). Depois, pergunte sua música favorita da banda (`string`). Por fim, pergunte a nota que ele dá para a música, de 0.0 a 10.0 (`float`). Exiba um resumo das preferências.

**Resolução:**

```csharp
Console.WriteLine("Qual é a sua banda favorita?");
string bandaFavorita = Console.ReadLine();

Console.WriteLine($"E qual a sua música favorita de {bandaFavorita}?");
string musicaFavorita = Console.ReadLine();

Console.WriteLine($"Que nota você dá para '{musicaFavorita}' (de 0.0 a 10.0)?");
float notaMusica = Convert.ToSingle(Console.ReadLine());

Console.WriteLine($"Resumo de Preferências: Banda: {bandaFavorita}, Música: {musicaFavorita}, Nota: {notaMusica}.");
```

-----

#### **Enunciado 10: Verificador de Condição Lógica**

Pergunte ao usuário se está chovendo (`true`/`false`). Em seguida, pergunte se ele tem um guarda-chuva (`true`/`false`). Armazene as respostas em duas variáveis `bool`. Exiba ambas as respostas no console de forma clara.

**Resolução:**

```csharp
Console.WriteLine("Está chovendo agora? (responda com 'true' ou 'false')");
bool estaChovendo = Convert.ToBoolean(Console.ReadLine());

Console.WriteLine("Você tem um guarda-chuva? (responda com 'true' ou 'false')");
bool temGuardaChuva = Convert.ToBoolean(Console.ReadLine());

Console.WriteLine("Status do tempo e preparo:");
Console.WriteLine("Está chovendo: " + estaChovendo);
Console.WriteLine("Possui guarda-chuva: " + temGuardaChuva);
```
