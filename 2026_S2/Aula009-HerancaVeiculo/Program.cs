using System.Globalization;
using Aula009_HerancaVeiculo.Modelo;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("pt-BR");

Console.WriteLine("==================================================");
Console.WriteLine("   ATIVIDADE 3 - GERENCIAMENTO DE VEÍCULOS        ");
Console.WriteLine("==================================================\n");

// 1. Criação de pelo menos um objeto de cada categoria
Console.WriteLine("--- 1. Instanciando Automóvel, Motocicleta e Caminhão ---");
Automovel sedan = new(
    placa: "ABC-1D23",
    marca: "Toyota",
    modelo: "Corolla Altis",
    anoFabricacao: 2023,
    quilometragemInicial: 15400.0,
    quantidadePortas: 4,
    capacidadePassageiros: 5
);

Motocicleta moto = new(
    placa: "XYZ-9W87",
    marca: "Honda",
    modelo: "CB 500X",
    anoFabricacao: 2024,
    quilometragemInicial: 3200.5,
    cilindradas: 471,
    tipoPartida: "Elétrica"
);

Caminhao caminhao = new(
    placa: "CAM-7B89",
    marca: "Volvo",
    modelo: "FH 540 6x4",
    anoFabricacao: 2022,
    quilometragemInicial: 120500.0,
    capacidadeMaximaCargaKg: 30000.0, // 30 toneladas
    quantidadeEixos: 6
);

// 2. Exibição dos dados comuns usando o método herdado ExibirDados()
Console.WriteLine("\n--- 2. Dados Gerais (método herdado ExibirDados) ---");
sedan.ExibirDados();
moto.ExibirDados();
caminhao.ExibirDados();

// 3. Demonstração dos métodos específicos de cada subclasse
Console.WriteLine("\n--- 3. Métodos Específicos de Cada Categoria ---");
sedan.ExibirCapacidade();
moto.ExibirCaracteristicas();
caminhao.ExibirEspecificacoesCarga();

// 4. Registro de novas quilometragens e rejeição de valores inválidos
Console.WriteLine("\n--- 4. Registro de Quilometragem Acumulada ---");
Console.WriteLine($"Km anterior do Sedan: {sedan.Quilometragem:N1} km");
sedan.RegistrarQuilometragem(350.5);
Console.WriteLine($"Km após viagem de 350.5 km: {sedan.Quilometragem:N1} km");

Console.WriteLine("\nTentativa de registrar quilometragem inválida (-50 km):");
try
{
    sedan.RegistrarQuilometragem(-50.0);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Rejeição com Sucesso]: {ex.Message}");
}

// 5. Teste de diferentes pesos de carga no caminhão
Console.WriteLine("\n--- 5. Verificação de Capacidade de Carga no Caminhão ---");
double[] testesDeCarga = [15000.0, 30000.0, 35000.0, -1000.0];

foreach (var peso in testesDeCarga)
{
    bool aceito = caminhao.VerificarCapacidade(peso);
    string status = aceito ? "APROVADO para transporte" : "REJEITADO (carga excede capacidade ou é inválida)";
    Console.WriteLine($"Carga de {peso:N0} kg -> Status: {status}");
}

// 6. Demonstração de validações na instanciação
Console.WriteLine("\n--- 6. Validações na Criação (Tratamento de Exceções) ---");
try
{
    Console.WriteLine("Tentando criar veículo com ano anterior a 1886 (1800)...");
    _ = new Automovel("OLD-0001", "Carruagem", "Vapor", 1800, 0, 2, 2);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

try
{
    Console.WriteLine("\nTentando criar caminhão com apenas 1 eixo...");
    _ = new Caminhao("ERR-0002", "Mini", "Carga", 2021, 1000, 5000, 1);
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Exceção capturada com sucesso]: {ex.Message}");
}

// 7. Resolução conceitual do Desafio de Modelagem (Bicicleta)
Console.WriteLine("\n==================================================");
Console.WriteLine("    DESAFIO DE MODELAGEM - FROTA DE BICICLETAS    ");
Console.WriteLine("==================================================");
Console.WriteLine("Pergunta: Bicicleta deveria herdar diretamente de Veiculo?");
Console.WriteLine("Análise e Decisão:");
Console.WriteLine("1. Inadequações da herança direta:");
Console.WriteLine("   - Bicicletas não possuem Placa em vias públicas;");
Console.WriteLine("   - Não possuem velocímetro/hodômetro obrigatório (quilometragem);");
Console.WriteLine("   - Não compartilham as mesmas regras tributárias e fiscais.");
Console.WriteLine("2. Solução recomendada na hierarquia:");
Console.WriteLine("   - Criar uma abstração intermediária: VeiculoMotorizado : Veiculo");
Console.WriteLine("   - Ou tornar Veiculo mais genérico (marca, modelo, ano) e mover");
Console.WriteLine("     Placa e Quilometragem para VeiculoMotorizado / VeiculoAutomotor.");
Console.WriteLine("==================================================");
