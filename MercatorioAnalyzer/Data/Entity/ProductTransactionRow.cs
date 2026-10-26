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
    decimal ExportFare,
    decimal ContractedInbound,
    decimal ContractedInboundCost,
    decimal ContractedOutbound,
    decimal ContractedOutboundValue,
    decimal ContractedOutboundCost)
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
        var contractedFlow = GetValue(storehouseOperations?.TotalFlows, productName)?.Contracted;

        rows.Add(Create(
            "Storehouse",
            (storehouseFlow?.Purchase ?? 0m),
            (storehouseFlow?.Sale ?? 0m),
            (storehouseFlow?.PurchaseCost ?? 0m),
            (storehouseFlow?.SaleValue ?? 0m),
            (storehouseFlow?.SaleCost ?? 0m),
            0m,
            0m,
            contractedFlow?.Inbound ?? 0m,
            contractedFlow?.InboundCost ?? 0m,
            contractedFlow?.Outbound ?? 0m,
            contractedFlow?.OutboundValue ?? 0m,
            contractedFlow?.OutboundCost ?? 0m));

        foreach (var transport in transports)
        {
            var asset = GetValue(transport.Route?.Account?.Assets, productName);
            var realized = GetValue(transport.Route?.Flows, productName)?.Realized; 
            rows.Add(Create(
                transport.Route?.RemoteTown is string remoteTown && transportNames.TryGetValue(remoteTown, out var townName)
                    ? townName
                    : transport.Id,
                asset?.Purchase ?? 0m,
                asset?.Sale ?? 0m,
                realized?.ImportCost ?? 0m,
                realized?.ExportValue ?? 0m,
                realized?.ExportCost ?? 0m,
                realized?.ImportFare ?? 0m,
                realized?.ExportFare ?? 0m,
                0m,
                0m,
                0m,
                0m,
                0m));
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
        decimal exportFare,
        decimal contractedInbound,
        decimal contractedInboundCost,
        decimal contractedOutbound,
        decimal contractedOutboundValue,
        decimal contractedOutboundCost) => new(
            location,
            unitsBought,
            unitsSold,
            buyExpenditure,
            salesRevenue,
            cogs,
            importFare,
            exportFare,
            contractedInbound,
            contractedInboundCost,
            contractedOutbound,
            contractedOutboundValue,
            contractedOutboundCost);

    private static TValue? GetValue<TKey, TValue>(
        IDictionary<TKey, TValue>? values,
        TKey key)
        where TKey : notnull
        where TValue : class =>
        values is not null && values.TryGetValue(key, out var value) ? value : null;
}