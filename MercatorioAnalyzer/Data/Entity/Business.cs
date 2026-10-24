using Newtonsoft.Json;

namespace MercatorioAnalyzer.Data.Entity;

public class Business
{
    [JsonProperty("id")] public string Id { get; set; } = "";
    [JsonProperty("transport_ids")] public List<string> TransportIds { get; set; } = [];
}
