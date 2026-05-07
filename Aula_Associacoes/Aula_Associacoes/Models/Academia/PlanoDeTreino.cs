// public class PlanoDeTreino
// {
//     private readonly List<Exercicio> _exercicios = [];
//     public Guid PlanoDeTreinoId { get; init; } = Guid.NewGuid();
//     public Aluno Aluno { get; init; } = Aluno ?? throw new ArgumentNullException(nameof(Aluno), "O aluno é obrigatório para criar um plano de treino.");
//     public IReadOnlyList<Exercicio> Exercicios => _exercicios;

//     // public Dictionary<string, (int Series, int Repeticoes)> DetalhesExercicios { get; } = new Dictionary<string, (int Series, int Repeticoes)>();

//     // public void RegistrarExercicio(Exercicio exercicio, int series, int repeticoes)
//     // {
//     //     if (exercicio == null)
//     //         throw new ArgumentNullException(nameof(exercicio), "O exercício é obrigatório para registrar um plano de treino.");

//     //     _exercicios.Add(exercicio);
//     //     DetalhesExercicios[exercicio.Nome] = (series, repeticoes);
//     // }
// }