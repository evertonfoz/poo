namespace CadastroAcademico.Application.Results;

public sealed class OperationResult<T> : OperationResult
{
    public T? Value { get; }

    private OperationResult(bool isSuccess, string message, T? value)
        : base(isSuccess, message)
    {
        Value = value;
    }

    public static OperationResult<T> Success(T value, string message = "Operacao realizada com sucesso.")
    {
        return new OperationResult<T>(true, message, value);
    }

    public static new OperationResult<T> Failure(string message)
    {
        return new OperationResult<T>(false, message, default);
    }
}
