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
        Building? storehouse,
        IEnumerable<Transport> transports,
        Dictionary<string, string> transportNames)
    {
        var rows = new List<ProductTransactionRow>();
        var inventory = storehouse?.Storage?.Inventory;
        var storehouseAsset = GetValue(inventory?.Account?.Assets, productName);
        var storehouseFlow = GetValue(inventory?.PreviousFlows, productName);

        rows.Add(Create(
            "Storehouse",
            storehouseAsset?.Purchase ?? 0m,
            storehouseAsset?.PurchasePrice ?? 0m,
            storehouseAsset?.Sale ?? 0m,
            storehouseAsset?.SalePrice ?? 0m,
            storehouseAsset?.UnitCost ?? 0m,
            storehouseFlow?.ImportFare ?? 0m,
            storehouseFlow?.ExportFare ?? 0m));

        foreach (var transport in transports)
        {
            var asset = GetValue(transport.Route?.Account?.Assets, productName);
            var realized = GetValue(transport.Route?.Flows, productName)?.Realized; 
            rows.Add(Create(
                transportNames.GetValueOrDefault(transport.Route?.RemoteTown,transport.Id),
                asset?.Purchase ?? 0m,
                asset?.PurchasePrice ?? 0m,
                asset?.Sale ?? 0m,
                asset?.SalePrice ?? 0m,
                asset?.UnitCost ?? 0m,
                realized?.ImportFare ?? 0m,
                realized?.ExportFare ?? 0m));
        }

        return rows;
    }

    private static ProductTransactionRow Create(
        string location,
        decimal unitsBought,
        decimal purchasePrice,
        decimal unitsSold,
        decimal salePrice,
        decimal unitCost,
        decimal importFare,
        decimal exportFare) => new(
            location,
            unitsBought,
            unitsSold,
            unitsBought * purchasePrice,
            unitsSold * salePrice,
            unitsSold * unitCost,
            importFare,
            exportFare);

    private static TValue? GetValue<TKey, TValue>(
        IDictionary<TKey, TValue>? values,
        TKey key)
        where TKey : notnull
        where TValue : class =>
        values is not null && values.TryGetValue(key, out var value) ? value : null;
}