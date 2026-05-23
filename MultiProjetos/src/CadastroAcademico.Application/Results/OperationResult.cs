namespace CadastroAcademico.Application.Results;

public class OperationResult
{
    public bool IsSuccess { get; }
    public string Message { get; }

    protected OperationResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static OperationResult Success(string message = "Operacao realizada com sucesso.")
    {
        return new OperationResult(true, message);
    }

    public static OperationResult Failure(string message)
    {
        return new OperationResult(false, message);
    }
}
