namespace MercatorioAnalyzer.Data.Entity;

public sealed record ProductOpportunityInputRow(
    string Product,
    decimal InputQuantity,
    decimal? CurrentTurnProfitPerUnit)
{
    public decimal? TotalPossibleProfit => InputQuantity * CurrentTurnProfitPerUnit;
}

public sealed record ProductOpportunityCostData(IReadOnlyList<ProductOpportunityInputRow> Inputs)
{
    public decimal TotalPossibleProfit => Inputs.Sum(input => input.TotalPossibleProfit) ?? 0m;

    public static ProductOpportunityCostData Build(
        IEnumerable<Recipe> recipes,
        BuildingOperations? operations)
    {
        if (operations is null)
        {
            return new([]);
        }

        var currentRecipeNames = operations.Operations
            .Select(operation => operation.Recipe)
            .OfType<string>()
            .ToHashSet(StringComparer.Ordinal);

        var inputs = recipes
            .Where(recipe => currentRecipeNames.Contains(recipe.Name))
            .SelectMany(recipe => recipe.Inputs)
            .GroupBy(input => input.Product, StringComparer.Ordinal)
            .Select(group => (
                Product: group.Key,
                Quantity: group.Sum(input => input.Amount),
                Realized: operations.TotalFlows.GetValueOrDefault(group.Key)?.Realized))
            .Where(input => input.Realized is { Sale: > 0 } or { Export: > 0 })
            .OrderBy(input => input.Product, StringComparer.Ordinal)
            .Select(input => new ProductOpportunityInputRow(
                input.Product,
                input.Quantity,
                GetAverageProfit(input.Realized!)))
            .ToList();

        return new(inputs);
    }

    // Callers guarantee at least one channel has positive units, so the divisor is never zero.
    private static decimal? GetAverageProfit(BuildingOperationValues realized)
    {
        decimal units = 0m;
        decimal? profit = 0m;

        if (realized is { Sale: > 0 })
        {
            units += realized.Sale.Value;
            profit += realized.SaleValue - realized.SaleCost;
        }

        if (realized is { Export: > 0 })
        {
            units += realized.Export.Value;
            profit += realized.ExportValue - realized.ExportCost - (realized.ExportFare ?? 0m);
        }

        return profit / units;
    }
}