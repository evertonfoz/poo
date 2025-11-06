using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AsyncStreams
{
    // Public wrapper API to expose ContarAsync for tests and other callers.
    // We keep the implementation in Program (educational demo) and expose a
    // thin public adapter so tests (in a different assembly) can call it
    // without changing the original demo layout.
    public static class AsyncStreamsApi
    {
        public static IAsyncEnumerable<int> ContarAsyncPublic(int ate, int atrasoMs, CancellationToken ct = default)
        {
            return Program.ContarAsync(ate, atrasoMs, ct);
        }
    }
}
