using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercatorioAnalyzer.Data.Entity;

public class Town
{
    [JsonProperty("id")] public string Id { get; set; }
    [JsonProperty("name")] public string Name { get; set; }
}