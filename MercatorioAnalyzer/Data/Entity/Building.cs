using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MercatorioAnalyzer.Data.Entity;

public class Building
{
    [JsonProperty("name")] public string Name { get; set; } = "";
    [JsonProperty("id")] public string Id { get; set; } = "";
    [JsonProperty("type")] public string BuildingType { get; set; } = "";
    [JsonProperty("size")] public int Size { get; set; }
    [JsonProperty("capacity")] public string? Capacity { get; set; }
    [JsonProperty("town_id")] public string? TownId { get; set; }
    [JsonProperty("owner_id")] public string? OwnerId { get; set; }
    [JsonProperty("location")] public string? Location { get; set; }
    [JsonProperty("location_legacy")] public BuildingPoint? LocationLegacy { get; set; }
    [JsonProperty("sublocation")] public BuildingPoint? Sublocation { get; set; }
    [JsonProperty("delivery_cost")] public BuildingDeliveryCost? DeliveryCost { get; set; }
    [JsonProperty("storage")] public BuildingStorageData? Storage { get; set; }
    [JsonProperty("operations")] public List<string> Operations { get; set; } = [];
    [JsonProperty("cost")] public string? Cost { get; set; }
}

public class BuildingPoint
{
    [JsonProperty("x")] public int X { get; set; }
    [JsonProperty("y")] public int Y { get; set; }
}

public class BuildingDeliveryCost
{
    [JsonProperty("land_distance")] public decimal LandDistance { get; set; }
}

public class BuildingStorageData
{
    [JsonProperty("reference")] public string? Reference { get; set; }
    [JsonProperty("inventory")] public BuildingInventoryData? Inventory { get; set; }
}

public class BuildingInventoryData
{
    [JsonProperty("account")] public BuildingAccountData? Account { get; set; }
    [JsonProperty("capacity")] public int Capacity { get; set; }
    [JsonProperty("reserved")] public int Reserved { get; set; }
    [JsonProperty("used")] public string? Used { get; set; }
    [JsonProperty("holdings")] public Dictionary<string, BuildingHoldingData> Holdings { get; set; } = [];
    [JsonProperty("flows")] public Dictionary<string, JObject>? Flows { get; set; }
    [JsonProperty("previous_flows")] public Dictionary<string, BuildingPreviousFlow>? PreviousFlows { get; set; }
    [JsonProperty("settings")] public JObject? Settings { get; set; }
}

public class BuildingAccountData
{
    [JsonProperty("id")] public string? Id { get; set; }
    [JsonProperty("name")] public string? Name { get; set; }
    [JsonProperty("owner_id")] public string? OwnerId { get; set; }
    [JsonProperty("sponsor_id")] public string? SponsorId { get; set; }
    [JsonProperty("assets")] public Dictionary<string, BuildingAssetData> Assets { get; set; } = [];
    [JsonProperty("_ts")] public long Timestamp { get; set; }
}

public class BuildingAssetData
{
    [JsonProperty("balance")] public decimal Balance { get; set; } = 0.0m;
    [JsonProperty("reserved")] public decimal Reserved { get; set; } = 0.0m;
    [JsonProperty("capacity")] public decimal Capacity { get; set; } = 0.0m;
    [JsonProperty("reserved_capacity")] public decimal ReservedCapacity { get; set; } = 0.0m;
    [JsonProperty("purchase")] public decimal Purchase { get; set; } = 0.0m;
    [JsonProperty("purchase_price")] public decimal PurchasePrice { get; set; } = 0.0m;
    [JsonProperty("sale")] public decimal Sale { get; set; } = 0.0m;
    [JsonProperty("sale_price")] public decimal SalePrice { get; set; } = 0.0m;
    [JsonProperty("unit_cost")] public decimal UnitCost { get; set; } = 0.0m;
}

public class BuildingHoldingData
{
    [JsonProperty("managers")] public List<BuildingManagerData>? Managers { get; set; }
    [JsonProperty("capacity")] public int? Capacity { get; set; }
    [JsonProperty("alerts")] public BuildingAlerts? Alerts { get; set; }
    [JsonProperty("overspill_level")] public int? OverspillLevel { get; set; }
}

public class BuildingManagerData
{
    [JsonProperty("min_holding")] public decimal? MinHolding { get; set; }
    [JsonProperty("max_holding")] public decimal? MaxHolding { get; set; }
    [JsonProperty("buy_volume")] public decimal? BuyVolume { get; set; }
    [JsonProperty("buy_price")] public string? BuyPrice { get; set; }
    [JsonProperty("sell_volume")] public decimal? SellVolume { get; set; }
    [JsonProperty("sell_price")] public string? SellPrice { get; set; }
    [JsonProperty("sell_markup")] public string? SellMarkup { get; set; }
    [JsonProperty("markup_price")] public bool? MarkupPrice { get; set; }
    [JsonProperty("result")] public JObject? Result { get; set; }
}

public class BuildingAlerts
{
    [JsonProperty("low")] public decimal? Low { get; set; }
}

public class BuildingPreviousFlow
{
    [JsonProperty("production")] public decimal Production { get; set; } = 0.0m;
    [JsonProperty("production_cost")] public decimal ProductionCost { get; set; } = 0.0m;
    [JsonProperty("consumption")] public decimal Consumption { get; set; } = 0.0m;
    [JsonProperty("consumption_cost")] public decimal ConsumptionCost { get; set; } = 0.0m;
    [JsonProperty("import")] public decimal Import { get; set; } = 0.0m;
    [JsonProperty("import_cost")] public decimal ImportCost { get; set; } = 0.0m;
    [JsonProperty("import_fare")] public decimal ImportFare { get; set; } = 0.0m;
    [JsonProperty("inbound")] public decimal Inbound { get; set; } = 0.0m;
    [JsonProperty("inbound_cost")] public decimal InboundCost { get; set; } = 0.0m;
    [JsonProperty("outbound")] public decimal Outbound { get; set; } = 0.0m;
    [JsonProperty("outbound_value")] public decimal OutboundValue { get; set; } = 0.0m;
    [JsonProperty("outbound_cost")] public decimal OutboundCost { get; set; } = 0.0m;
    [JsonProperty("purchase")] public decimal Purchase { get; set; } = 0.0m;
    [JsonProperty("purchase_cost")] public decimal PurchaseCost { get; set; } = 0.0m;
    [JsonProperty("sale")] public decimal Sale { get; set; } = 0.0m;
    [JsonProperty("sale_value")] public decimal SaleValue { get; set; } = 0.0m;
    [JsonProperty("sale_cost")] public decimal SaleCost { get; set; } = 0.0m;
    [JsonProperty("export")] public decimal Export { get; set; } = 0.0m;
    [JsonProperty("export_value")] public decimal ExportValue { get; set; } = 0.0m;
    [JsonProperty("export_cost")] public decimal ExportCost { get; set; } = 0.0m;
    [JsonProperty("export_fare")] public decimal ExportFare { get; set; } = 0.0m;
    [JsonProperty("expiration")] public decimal Expiration { get; set; } = 0.0m;

}