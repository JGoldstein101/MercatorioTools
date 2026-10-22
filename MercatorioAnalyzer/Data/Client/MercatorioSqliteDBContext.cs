namespace MercatorioAnalyzer.Data.Client;

using MercatorioAnalyzer.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
public class MercatorioSqliteDBContext(DbContextOptions<MercatorioSqliteDBContext> options) : DbContext(options)
{
    public DbSet<MercatorioAnalyzer.Data.Entity.Product> Product { get; set; } = default!;
    public DbSet<MercatorioAnalyzer.Data.Entity.Recipe> Recipe { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureRecipes(modelBuilder);
        ConfigureProducts(modelBuilder);
    }

    private static void ConfigureRecipes(ModelBuilder modelBuilder)
    {
        var converter = new ValueConverter<List<RecipeItem>, string>(
            items => JsonSerializer.Serialize(items,(JsonSerializerOptions?)null),
            json => JsonSerializer.Deserialize<List<RecipeItem>>(
                json, (JsonSerializerOptions?)null) ?? new List<RecipeItem>()
        );
        var comparer = new ValueComparer<List<RecipeItem>>(
            (a, b) => JsonSerializer.Serialize(a,(JsonSerializerOptions?)null) == JsonSerializer.Serialize(b,(JsonSerializerOptions?)null),
            value => JsonSerializer.Serialize(value,(JsonSerializerOptions?)null).GetHashCode(),
            value => JsonSerializer.Deserialize<List<RecipeItem>>(
                JsonSerializer.Serialize(value,(JsonSerializerOptions?)null),(JsonSerializerOptions?)null
            ) ?? new List<RecipeItem>()
        );
        var recipe = modelBuilder.Entity<Recipe>();

        recipe.Property(r => r.Inputs).HasConversion(converter).Metadata.SetValueComparer(comparer);
        recipe.Property(r => r.Outputs).HasConversion(converter).Metadata.SetValueComparer(comparer);

    }

    private static void ConfigureProducts(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().OwnsOne(product => product.Price);
    }
}
