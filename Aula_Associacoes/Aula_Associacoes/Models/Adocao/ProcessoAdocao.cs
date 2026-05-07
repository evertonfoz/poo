public class ProcessoAdocao
{
    public Adotante Adotante { get; init; }
    private  readonly List<DocumentoAdocao> _documentosAdocao = [];
    public IReadOnlyList<DocumentoAdocao> DocumentosAdocacao => _documentosAdocao;
    public DocumentoAdocao Documento { get; init; }

    public ProcessoAdocao(Adotante adotante, DocumentoAdocao documento)
    {
        Adotante = adotante ?? throw new ArgumentNullException(nameof(adotante), "O adotante é obrigatório.");
        Documento = _documentosAdocao.Add(documento) ?? throw new ArgumentNullException(nameof(documento), "O documento de adoção é obrigatório.");
    }

    public void AdicionarDocumento(DocumentoAdocao documento)
    {
        if (documento == null)
            throw new ArgumentNullException(nameof(documento), "O documento de adoção é obrigatório para adicionar ao processo.");

        _documentosAdocao.Add(documento);
    }
}