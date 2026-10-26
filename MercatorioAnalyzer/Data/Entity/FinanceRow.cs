namespace MercatorioAnalyzer.Data.Entity;

public sealed record FinanceRow(
    string Item,
    decimal Expenditure,
    decimal Revenue,
    decimal Cogs,
    decimal ContractedInbound,
    decimal ContractedInboundCost,
    decimal ContractedOutbound,
    decimal ContractedOutboundValue,
    decimal ContractedOutboundCost)
{
    public decimal Profit => Revenue + ContractedOutboundValue - Cogs - ContractedOutboundCost;

    public static List<FinanceRow> Build(BuildingOperations? storehouseOperations, IEnumerable<Transport> transports)
    {
        var totals = new Dictionary<string, (
            decimal Cost,
            decimal Revenue,
            decimal Cogs,
            decimal ContractedInbound,
            decimal ContractedInboundCost,
            decimal ContractedOutbound,
            decimal ContractedOutboundValue,
            decimal ContractedOutboundCost)>();

        void Add(
            string item,
            decimal cost,
            decimal revenue,
            decimal cogs,
            decimal contractedInbound = 0m,
            decimal contractedInboundCost = 0m,
            decimal contractedOutbound = 0m,
            decimal contractedOutboundValue = 0m,
            decimal contractedOutboundCost = 0m)
        {
            totals.TryGetValue(item, out var current);
            totals[item] = (
                current.Cost + cost,
                current.Revenue + revenue,
                current.Cogs + cogs,
                current.ContractedInbound + contractedInbound,
                current.ContractedInboundCost + contractedInboundCost,
                current.ContractedOutbound + contractedOutbound,
                current.ContractedOutboundValue + contractedOutboundValue,
                current.ContractedOutboundCost + contractedOutboundCost);
        }

        foreach (var (item, flow) in storehouseOperations?.TotalFlows ?? [])
        {
            var realized = flow?.Realized;
            var contracted = flow?.Contracted;
            Add(item,
                (realized?.PurchaseCost ?? 0m),
                (realized?.SaleValue ?? 0m),
                (realized?.SaleCost ?? 0m),
                contracted?.Inbound ?? 0m,
                contracted?.InboundCost ?? 0m,
                contracted?.Outbound ?? 0m,
                contracted?.OutboundValue ?? 0m,
                contracted?.OutboundCost ?? 0m);
        }

        foreach (var transport in transports)
        {

            foreach (var (item, flow) in transport.Route?.Flows ?? [])
            {
                var realized = flow?.Realized;
                if (realized is null)
                {
                    continue;
                }
                Add(item, realized.ImportFare ?? 0m + realized.ImportCost ?? 0m, realized.ExportValue ?? 0m, realized.ExportCost ?? 0m + realized.ExportFare ?? 0m);
            }
        }

        return totals
            .Where(entry => entry.Key != "money" && (
                entry.Value.Cost != 0m ||
                entry.Value.Revenue != 0m ||
                entry.Value.ContractedInbound != 0m ||
                entry.Value.ContractedInboundCost != 0m ||
                entry.Value.ContractedOutbound != 0m ||
                entry.Value.ContractedOutboundValue != 0m ||
                entry.Value.ContractedOutboundCost != 0m))
            .Select(entry => new FinanceRow(
                entry.Key,
                entry.Value.Cost,
                entry.Value.Revenue,
                entry.Value.Cogs,
                entry.Value.ContractedInbound,
                entry.Value.ContractedInboundCost,
                entry.Value.ContractedOutbound,
                entry.Value.ContractedOutboundValue,
                entry.Value.ContractedOutboundCost))
            .ToList();
    }
}
