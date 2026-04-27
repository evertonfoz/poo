public class Endereco
{
    public string? Rua { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? CEP { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Endereco other)
        {
            return Rua == other.Rua &&
                   Cidade == other.Cidade &&
                   Estado == other.Estado &&
                   CEP == other.CEP;
        }
        return false;
    }
}