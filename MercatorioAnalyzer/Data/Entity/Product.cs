using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace MercatorioAnalyzer.Data.Entity;

public class Product
{
	[Key]
	[JsonProperty("name")]
	public string Name { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; }

	[JsonProperty("unit")]
	public string Unit { get; set; }

	[JsonProperty("classes")]
	public List<string> Classes { get; set; }

	[JsonProperty("price")]
	public ProductPrice Price { get; set; }
}

public class ProductPrice
{
	[JsonProperty("low")]
	public decimal Low { get; set; }

	[JsonProperty("typical")]
	public decimal Typical { get; set; }

	[JsonProperty("high")]
	public decimal High { get; set; }
}

