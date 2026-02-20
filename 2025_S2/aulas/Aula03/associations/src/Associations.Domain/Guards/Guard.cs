using System.Diagnostics.CodeAnalysis;

namespace Associations.Domain.Guards;

/// <summary>
/// Classe estática que fornece métodos de validação (Guard Clauses) para proteger o código contra estados inválidos.
/// </summary>
/// <remarks>
/// <para>
/// <b>📚 Para iniciantes:</b> Um "Guard" (guardião) é um padrão de programação defensiva que valida 
/// condições antes de executar uma operação. Pense em um segurança na entrada de um evento que verifica 
/// se você tem ingresso válido antes de permitir a entrada.
/// </para>
/// <para>
/// <b>🎯 Objetivo:</b> Centralizar a validação de argumentos nulos, evitando código repetitivo de 
/// verificação em vários lugares da aplicação e garantindo falhas rápidas (fail-fast) quando dados 
/// inválidos são detectados.
/// </para>
/// <para>
/// <b>💡 Por que usar Guard Clauses?</b>
/// </para>
/// <list type="bullet">
/// <item><description>Detecta erros imediatamente no ponto de entrada, não após várias operações</description></item>
/// <item><description>Torna o código mais limpo e legível, eliminando if's repetitivos</description></item>
/// <item><description>Fornece mensagens de erro mais claras sobre qual parâmetro causou o problema</description></item>
/// <item><description>Segue o princípio de "falhar rápido" (Fail Fast), evitando propagação de erros</description></item>
/// </list>
/// <para>
/// <b>🔧 Técnico:</b> Esta classe implementa o padrão Guard Clause usando métodos genéricos com 
/// anotações de nullability do C# moderno. Os atributos <see cref="DynamicallyAccessedMembersAttribute"/> 
/// e <see cref="NotNullAttribute"/> auxiliam a análise estática do compilador e ferramentas de análise 
/// de código para detectar possíveis problemas em tempo de compilação.
/// </para>
/// </remarks>
/// <example>
/// <b>Exemplo de uso básico:</b>
/// <code>
/// public class Pedido
/// {
///     private readonly Cliente _cliente;
///     
///     public Pedido(Cliente cliente)
///     {
///         // Valida que o cliente não seja nulo antes de usar
///         Guard.AgainstNull(ref cliente, nameof(cliente));
///         _cliente = cliente;
///     }
/// }
/// </code>
/// 
/// <b>O que acontece se passar null:</b>
/// <code>
/// // Isso lançará ArgumentNullException com mensagem clara
/// var pedido = new Pedido(null); 
/// // Exception: Value cannot be null. (Parameter 'cliente')
/// </code>
/// </example>
public static class Guard
{
    /// <summary>
    /// Valida se o valor fornecido não é nulo, lançando uma exceção caso seja.
    /// </summary>
    /// <typeparam name="T">
    /// O tipo do valor a ser validado. Pode ser qualquer tipo de referência ou tipo nullable.
    /// </typeparam>
    /// <param name="value">
    /// O valor a ser validado contra nulo. Passado por referência (ref) para permitir que o 
    /// compilador atualize informações de nullability após a validação bem-sucedida.
    /// </param>
    /// <param name="paramName">
    /// O nome do parâmetro sendo validado. Use o operador <c>nameof()</c> para obter o nome 
    /// automaticamente e evitar erros de digitação.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Lançada quando <paramref name="value"/> é nulo. A exceção incluirá o 
    /// <paramref name="paramName"/> na mensagem de erro para facilitar a depuração.
    /// </exception>
    /// <remarks>
    /// <para>
    /// <b>📚 Para iniciantes:</b> Este método verifica se um objeto é nulo (não existe/não foi criado) 
    /// antes de você tentar usá-lo. É como verificar se há água em um copo antes de tentar beber - 
    /// se estiver vazio (nulo), o método avisa imediatamente ao invés de deixar você "beber do copo vazio" 
    /// e causar um erro mais tarde.
    /// </para>
    /// <para>
    /// <b>🔍 O que significa "ref"?</b> O parâmetro <c>ref</c> permite que o método informe ao compilador 
    /// que, após esta verificação passar, o valor definitivamente não será nulo. Isso melhora a 
    /// análise de código e evita avisos desnecessários de nullability.
    /// </para>
    /// <para>
    /// <b>🎯 Quando usar:</b> Use este método no início de construtores, métodos e propriedades sempre 
    /// que receber parâmetros que não devem ser nulos. É especialmente importante em:
    /// </para>
    /// <list type="bullet">
    /// <item><description>Construtores que recebem dependências</description></item>
    /// <item><description>Métodos públicos que recebem objetos como parâmetros</description></item>
    /// <item><description>Setters de propriedades que não aceitam valores nulos</description></item>
    /// <item><description>Métodos de validação de modelo/entidade</description></item>
    /// </list>
    /// <para>
    /// <b>🔧 Aspectos técnicos:</b>
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// O atributo <c>[DynamicallyAccessedMembers(0)]</c> no tipo genérico T indica que o método 
    /// não requer acesso reflexivo aos membros do tipo, otimizando cenários de trimming e AOT.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// O atributo <c>[NotNull]</c> no parâmetro value informa ao analisador de código que, 
    /// após este método retornar (não lançar exceção), o valor não será nulo.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// O uso de <c>ref T?</c> permite que o compilador ajuste o estado de nullability da 
    /// variável no código chamador após a verificação.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// O método é genérico para aceitar qualquer tipo de referência, evitando boxing/unboxing 
    /// e mantendo type-safety.
    /// </description>
    /// </item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <b>Exemplo 1: Validação em método público</b>
    /// <code>
    /// public void ProcessarPedido(Pedido pedido)
    /// {
    ///     Guard.AgainstNull(ref pedido, nameof(pedido));
    ///     
    ///     // Agora é seguro usar pedido
    ///     pedido.Processar();
    /// }
    /// </code>
    /// 
    /// <b>Exemplo 2: Múltiplas validações</b>
    /// <code>
    /// public void CriarRelatorio(Cliente cliente, Periodo periodo, Formato formato)
    /// {
    ///     Guard.AgainstNull(ref cliente, nameof(cliente));
    ///     Guard.AgainstNull(ref periodo, nameof(periodo));
    ///     Guard.AgainstNull(ref formato, nameof(formato));
    ///     
    ///     // Todos os parâmetros foram validados
    ///     // Prosseguir com a lógica de negócio
    /// }
    /// </code>
    /// 
    /// <b>Exemplo 3: O que acontece ao passar null</b>
    /// <code>
    /// string? texto = null;
    /// 
    /// try
    /// {
    ///     Guard.AgainstNull(ref texto, nameof(texto));
    /// }
    /// catch (ArgumentNullException ex)
    /// {
    ///     Console.WriteLine(ex.Message);
    ///     // Saída: Value cannot be null. (Parameter 'texto')
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="ArgumentNullException"/>
    /// <seealso cref="NotNullAttribute"/>
    /// <seealso cref="DynamicallyAccessedMembersAttribute"/>
    public static void AgainstNull<[DynamicallyAccessedMembers(0)] T>(
        [NotNull] ref T? value, string paramName)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static bool TryParseNonEmpty(string? s, 
        [NotNullWhen(true)] out string? result)
    {
        if (!string.IsNullOrWhiteSpace(s)) { result = s; return true; }
        result = null; return false;
    }
}