using MercatorioAnalyzer.Data.Client;

namespace MercatorioAnalyzer.Data.Entity;
public sealed record ProductTransactionRow(
    string Location,
    decimal UnitsBought,
    decimal UnitsSold,
    decimal BuyExpenditure,
    decimal SalesRevenue,
    decimal Cogs,
    decimal ImportFare,
    decimal ExportFare)
{
    public decimal Profit => SalesRevenue - Cogs - ExportFare;

    public static List<ProductTransactionRow> Build(
        string productName,
        BuildingOperations? storehouseOperations,
        IEnumerable<Transport> transports,
        Dictionary<string, string> transportNames)
    {
        var rows = new List<ProductTransactionRow>();
        var storehouseFlow = GetValue(storehouseOperations?.TotalFlows, productName)?.Realized;

        rows.Add(Create(
            "Storehouse",
            (storehouseFlow?.Purchase ?? 0m),
            (storehouseFlow?.Sale ?? 0m),
            (storehouseFlow?.PurchaseCost ?? 0m),
            (storehouseFlow?.SaleValue ?? 0m),
            (storehouseFlow?.SaleCost ?? 0m),
            0m,
            0m));

        foreach (var transport in transports)
        {
            var asset = GetValue(transport.Route?.Account?.Assets, productName);
            var realized = GetValue(transport.Route?.Flows, productName)?.Realized; 
            rows.Add(Create(
                transportNames.GetValueOrDefault(transport.Route?.RemoteTown,transport.Id),
                asset?.Purchase ?? 0m,
                asset?.Sale ?? 0m,
                (asset?.Purchase ?? 0m) * (asset?.PurchasePrice ?? 0m),
                (asset?.Sale ?? 0m) * (asset?.SalePrice ?? 0m),
                (asset?.Sale ?? 0m) * (asset?.UnitCost ?? 0m),
                realized?.ImportFare ?? 0m,
                realized?.ExportFare ?? 0m));
        }

        return rows;
    }

    private static ProductTransactionRow Create(
        string location,
        decimal unitsBought,
        decimal unitsSold,
        decimal buyExpenditure,
        decimal salesRevenue,
        decimal cogs,
        decimal importFare,
        decimal exportFare) => new(
            location,
            unitsBought,
            unitsSold,
            buyExpenditure,
            salesRevenue,
            cogs,
            importFare,
            exportFare);

    private static TValue? GetValue<TKey, TValue>(
        IDictionary<TKey, TValue>? values,
        TKey key)
        where TKey : notnull
        where TValue : class =>
        values is not null && values.TryGetValue(key, out var value) ? value : null;
}