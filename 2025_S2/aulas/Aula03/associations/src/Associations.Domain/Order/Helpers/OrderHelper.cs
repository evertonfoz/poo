using System;

namespace Associations.Domain.Order.Helpers;

public static class OrderHelper
{
    public static string NormalizeSku(string raw)
    {
        ArgumentNullException.ThrowIfNull(raw);
        var noWs = new string([.. raw.Where(c => !char.IsWhiteSpace(c))]);
        return noWs.ToUpperInvariant();
    }
}
