namespace MercatorioAnalyzer.Data.Entity;

public sealed record FinanceRow(string Item, decimal Expenditure, decimal Revenue, decimal Cogs)
{
    public decimal Profit => Revenue - Cogs;

    public static List<FinanceRow> Build(BuildingOperations? storehouseOperations, IEnumerable<Transport> transports)
    {
        var totals = new Dictionary<string, (decimal Cost, decimal Revenue, decimal Cogs)>();

        void Add(string item, decimal cost, decimal revenue, decimal cogs)
        {
            totals.TryGetValue(item, out var current);
            totals[item] = (current.Cost + cost, current.Revenue + revenue, current.Cogs + cogs);
        }

        foreach (var (item, flow) in storehouseOperations?.TotalFlows ?? [])
        {
            var realized = flow?.Realized;
            if (realized is null)
            {
                continue;
            }

            Add(item,
                (realized.PurchaseCost ?? 0m),
                (realized.SaleValue ?? 0m),
                (realized.SaleCost ?? 0m));
        }

        foreach (var transport in transports)
        {
            foreach (var (item, asset) in transport.Route?.Account?.Assets ?? [])
            {
                if (asset is null)
                {
                    continue;
                }
                var sale = asset.Sale ?? 0m;
                Add(item,
                    (asset.Purchase ?? 0m) * (asset.PurchasePrice ?? 0m),
                    sale * (asset.SalePrice ?? 0m),
                    sale * (asset.UnitCost ?? 0m));
            }

            foreach (var (item, flow) in transport.Route?.Flows ?? [])
            {
                var realized = flow?.Realized;
                if (realized is null)
                {
                    continue;
                }
                Add(item, realized.ImportFare ?? 0m, 0m, realized.ExportFare ?? 0m);
            }
        }

        return totals
            .Where(entry => entry.Key != "money" && (entry.Value.Cost != 0m || entry.Value.Revenue != 0m))
            .Select(entry => new FinanceRow(entry.Key, entry.Value.Cost, entry.Value.Revenue, entry.Value.Cogs))
            .ToList();
    }
}
