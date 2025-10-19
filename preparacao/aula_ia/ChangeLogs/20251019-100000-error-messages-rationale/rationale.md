Rationale
=========

Resumo das mudanças realizadas em 2025-10-19:

- Padronizei mensagens de erro em `University.Enrollments.Domain.Models.Enrollment` e `Course` para incluir identidades (studentId, courseId) e facilitar diagnóstico em logs/erros.
- Adicionei um construtor adicional em `DomainException` que aceita `innerException` para preservar a exceção original quando re-lançamos com contexto.
- Melhorei comentários XML nas transições de estado de `Enrollment` para documentar invariantes e expectativas de transição.

Motivação:
- Melhorar a observabilidade e diagnóstico sem alterar regras de negócio.
- Permitir que callers preservem a pilha original quando re-lançarem exceções com contexto.

Verificações:
- Rodada rápida de verificação de erros do projeto: sem erros.

Próximos passos sugeridos (opcionais):
- Implementar testes unitários para transições de estado e mensagens de erro esperadas.
- Adicionar validações/factories para `Student` e `Course` para garantir invariantes (Id>0, Name non-empty, Capacity>=0) e cobrir via testes.
