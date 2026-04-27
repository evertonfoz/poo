// Aluno aluno = new();
// aluno.Nome = "João Silva";
// aluno.Matricula = "2023001";
// aluno.Ativar();
// aluno.ExibirDados();

// var enderecoEverton = new Endereco() { Rua = "Rua das Flores, 123" };
// var enderecoMaria = new Endereco() { Rua = "Rua das Flores, 123" };

// Console.WriteLine($"Endereço do Everton: {enderecoEverton.Rua}");
// Console.WriteLine($"Endereço da Maria: {enderecoMaria.Rua}");

// Console.WriteLine($"Os endereços são iguais? {enderecoEverton == enderecoMaria}");
// Console.WriteLine($"Os endereços são iguais? {enderecoEverton.Equals(enderecoMaria)}");

// Conta conta = new Conta("12345-6");
// Console.WriteLine($"Saldo inicial: {conta.Saldo}");

// TipoTransacao tipoCredito = new TipoTransacao("Crédito", 'C');
// TipoTransacao tipoDebito = new TipoTransacao("Débito", 'D');

// Transacao transacao = new Transacao(conta, tipoCredito, 100.00M);
// Console.WriteLine($"Saldo após transação: {conta.Saldo}");

// Transacao transacao2 = new Transacao(conta, tipoDebito, 50.00M);
// Console.WriteLine($"Saldo após transação: {conta.Saldo}");

// using Models.Academico;

Models.Academico.Aluno aluno1 = new Models.Academico.Aluno("2023001", "João Silva", new DateTime(2000, 5, 15), "joao.silva@email.com");
Models.Academico.Disciplina disciplina1 = new Models.Academico.Disciplina("Matemática", "Prof. Carlos");
Models.Academico.Matricula matricula1 = new Models.Academico.Matricula(aluno1, disciplina1, DateTime.Now);

Console.WriteLine(matricula1);
// Console.WriteLine($"Aluno: {aluno1.Nome}, RA: {aluno1.RegistroAcademico}, Email: {aluno1.Email}");
// Console.WriteLine($"Disciplina: {disciplina1.Nome}, Professor: {disciplina1.Professor}");
// Console.WriteLine($"Data da Matrícula: {matricula1.DataMatricula}");