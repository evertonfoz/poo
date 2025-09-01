using IntroducaoPOO.Modelo;

// Produto nomeEmBranco = new Produto(" ", -1, -1);
// Produto notebook = new Produto("Notebook", 2, 1);
// Console.WriteLine($"Total do Produto {notebook.Nome}  é {notebook.ValorTotal()}");

// notebook.PrecoUnitario *= 1.1;

// notebook.ReajustarPrecoUnitario(10);
// Console.WriteLine($"Total do Produto {notebook.Nome}  é {notebook.ValorTotal()}");

Produto monitor = new("Monitor", 2);
Produto monitorLCD = new("Monitor", 3, 1);

// Console.WriteLine(monitor == monitorLCD);
Console.WriteLine(monitor.Equals(monitorLCD));
// Console.WriteLine($"Total do Produto {monitor.Nome}  é {monitor.ValorTotal()}");

// Console.WriteLine(monitor);

// Produto usandoNew = new(" ", 1, -1);

// var usandoVar = new Produto(" ", -1, -1);