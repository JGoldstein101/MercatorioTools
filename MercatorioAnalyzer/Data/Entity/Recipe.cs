using System.ComponentModel.DataAnnotations;

namespace MercatorioAnalyzer.Data.Entity;

public class Recipe
{
    [Key]
    public string Name { get; set; } = "";
    public int Tier { get; set; }
    public string Site { get; set; } = "";
    public string Class { get; set; } = "";
    public decimal Points { get; set; }
    public List<RecipeItem> Inputs { get; set; } = [];
    public List<RecipeItem> Outputs { get; set; } = [];

}

public class RecipeItem
{
    public string Product { get; set; } = "";
    public decimal Amount { get; set; }
}
