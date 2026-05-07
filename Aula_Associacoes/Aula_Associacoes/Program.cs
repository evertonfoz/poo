// var poo = new Disciplina { Nome = "POO" };
// var bancoDeDados = new Disciplina { Nome = "Banco de Dados" };

// var curso = new Curso
// {
//     Nome = "Sistemas de Informação",
//     // Disciplinas = new List<Disciplina> { poo, bancoDeDados }
// };

// // curso.Disciplinas.Add(poo);
// // curso.Disciplinas.Add(bancoDeDados);

// curso.AdicionarDisciplina(poo);
// curso.AdicionarDisciplina(bancoDeDados);

// Console.WriteLine($"Curso: {curso.Nome}");
// Console.WriteLine("Disciplinas:");
// foreach (var disciplina in curso.Disciplinas)
// {    Console.WriteLine($"- {disciplina.Nome}");
// }

// curso.Disciplinas[0].Nome = "Programação Orientada a Objetos";
// Console.WriteLine($"Curso: {curso.Nome}");
// Console.WriteLine("Disciplinas:");
// foreach (var disciplina in curso.Disciplinas)
// {    Console.WriteLine($"- {disciplina.Nome}");
// }


Adotante adotante1 = new Adotante("Maria Silva", "123.456.789-08");
Console.WriteLine($"Adotante: {adotante1.Nome}, CPF: {adotante1.CPF}");