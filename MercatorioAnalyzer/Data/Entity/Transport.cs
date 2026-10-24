using System.Collections.Generic;
using Newtonsoft.Json;

namespace MercatorioAnalyzer.Data.Entity;

public class Transport
{
	[JsonProperty("id")] public string Id { get; set; }
	[JsonProperty("reference")] public string Reference { get; set; }
	[JsonProperty("type")] public string Type { get; set; }
	[JsonProperty("size")] public decimal? Size { get; set; }
	[JsonProperty("name")] public string Name { get; set; }
	[JsonProperty("owner_id")] public string OwnerId { get; set; }
	[JsonProperty("town_id")] public string TownId { get; set; }
	[JsonProperty("hometown_id")] public string HometownId { get; set; }
	[JsonProperty("procedure")] public string Procedure { get; set; }
	[JsonProperty("location")] public MapLocation Location { get; set; }
	[JsonProperty("capacity")] public decimal? Capacity { get; set; }
	[JsonProperty("cost")] public decimal? Cost { get; set; }
	[JsonProperty("inventory")] public TransportInventory Inventory { get; set; }
	[JsonProperty("cargo")] public Cargo Cargo { get; set; }
	[JsonProperty("previous_operation")] public PreviousOperation PreviousOperation { get; set; }
	[JsonProperty("provider_id")] public string ProviderId { get; set; }
	[JsonProperty("route")] public TransportRoute Route { get; set; }
	[JsonProperty("journey")] public Journey Journey { get; set; }
}

public class MapLocation
{
	[JsonProperty("x")] public int X { get; set; }
	[JsonProperty("y")] public int Y { get; set; }
}

public class TransportInventory
{
	[JsonProperty("account")] public Account Account { get; set; }
	[JsonProperty("capacity")] public decimal? Capacity { get; set; }
	[JsonProperty("previous_flows")] public Dictionary<string, FlowValues> PreviousFlows { get; set; }
	[JsonProperty("settings")] public Dictionary<string, object> Settings { get; set; }
}

public class Cargo
{
	[JsonProperty("reference")] public string Reference { get; set; }
	[JsonProperty("inventory")] public TransportInventory Inventory { get; set; }
}

public class Account
{
	[JsonProperty("id")] public string Id { get; set; }
	[JsonProperty("name")] public string Name { get; set; }
	[JsonProperty("owner_id")] public string OwnerId { get; set; }
	[JsonProperty("master_id")] public string MasterId { get; set; }
	[JsonProperty("sponsor_id")] public string SponsorId { get; set; }
	[JsonProperty("assets")] public Dictionary<string, TransportAsset> Assets { get; set; }
	[JsonProperty("_ts")] public long? Timestamp { get; set; }
}

public class TransportAsset
{
	[JsonProperty("balance")] public decimal? Balance { get; set; }
	[JsonProperty("reserved")] public decimal? Reserved { get; set; }
	[JsonProperty("capacity")] public decimal? Capacity { get; set; }
	[JsonProperty("reserved_capacity")] public decimal? ReservedCapacity { get; set; }
	[JsonProperty("purchase")] public decimal? Purchase { get; set; }
	[JsonProperty("purchase_price")] public decimal? PurchasePrice { get; set; }
	[JsonProperty("sale")] public decimal? Sale { get; set; }
	[JsonProperty("sale_price")] public decimal? SalePrice { get; set; }
	[JsonProperty("unit_cost")] public decimal? UnitCost { get; set; }
}

public class PreviousOperation
{
	[JsonProperty("target")] public decimal? Target { get; set; }
	[JsonProperty("production")] public decimal? Production { get; set; }
	[JsonProperty("projection")] public decimal? Projection { get; set; }
}

public class TransportRoute
{
	[JsonProperty("id")] public string Id { get; set; }
	[JsonProperty("reference")] public string Reference { get; set; }
	[JsonProperty("local_town")] public string LocalTown { get; set; }
	[JsonProperty("remote_town")] public string RemoteTown { get; set; }
	[JsonProperty("capacity")] public decimal? Capacity { get; set; }
	[JsonProperty("used_import")] public decimal? UsedImport { get; set; }
	[JsonProperty("used_export")] public decimal? UsedExport { get; set; }
	[JsonProperty("contracted_import")] public decimal? ContractedImport { get; set; }
	[JsonProperty("contracted_export")] public decimal? ContractedExport { get; set; }
	[JsonProperty("reserved_import")] public decimal? ReservedImport { get; set; }
	[JsonProperty("reserved_export")] public decimal? ReservedExport { get; set; }
	[JsonProperty("distance")] public decimal? Distance { get; set; }
	[JsonProperty("moves")] public decimal? Moves { get; set; }
	[JsonProperty("potential_trips")] public decimal? PotentialTrips { get; set; }
	[JsonProperty("actual_trips")] public decimal? ActualTrips { get; set; }
	[JsonProperty("capacity_cost")] public decimal? CapacityCost { get; set; }
	[JsonProperty("route_settings")] public RouteSettings RouteSettings { get; set; }
	[JsonProperty("provider_id")] public string ProviderId { get; set; }
	[JsonProperty("account_id")] public string AccountId { get; set; }
	[JsonProperty("account")] public Account Account { get; set; }
	[JsonProperty("holdings")] public Dictionary<string, TransportHolding> Holdings { get; set; }
	[JsonProperty("flows")] public Dictionary<string, Flow> Flows { get; set; }
	[JsonProperty("previous_flows")] public Dictionary<string, FlowValues> PreviousFlows { get; set; }
}

public class RouteSettings
{
	[JsonProperty("import_cost_share")] public decimal? ImportCostShare { get; set; }
}

public class TransportHolding
{
	[JsonProperty("managers")] public List<HoldingManager> Managers { get; set; }
}

public class HoldingManager
{
	[JsonProperty("min_holding")] public decimal? MinHolding { get; set; }
	[JsonProperty("buy_volume")] public decimal? BuyVolume { get; set; }
	[JsonProperty("buy_price")] public decimal? BuyPrice { get; set; }
	[JsonProperty("sell_volume")] public decimal? SellVolume { get; set; }
	[JsonProperty("sell_price")] public decimal? SellPrice { get; set; }
	[JsonProperty("result")] public HoldingResult Result { get; set; }
}

public class HoldingResult
{
	[JsonProperty("ask_volume")] public decimal? AskVolume { get; set; }
	[JsonProperty("bid_volume")] public decimal? BidVolume { get; set; }
	[JsonProperty("limitations")] public string Limitations { get; set; }
}

public class Flow
{
	[JsonProperty("projection")] public Dictionary<string, object> Projection { get; set; }
	[JsonProperty("realized")] public FlowValues Realized { get; set; }
}

public class FlowValues
{
	[JsonProperty("consumption")] public decimal? Consumption { get; set; }
	[JsonProperty("consumption_cost")] public decimal? ConsumptionCost { get; set; }
	[JsonProperty("production_cost")] public decimal? ProductionCost { get; set; }
	[JsonProperty("import")] public decimal? Import { get; set; }
	[JsonProperty("import_cost")] public decimal? ImportCost { get; set; }
	[JsonProperty("import_fare")] public decimal? ImportFare { get; set; }
	[JsonProperty("export")] public decimal? Export { get; set; }
	[JsonProperty("export_value")] public decimal? ExportValue { get; set; }
	[JsonProperty("export_cost")] public decimal? ExportCost { get; set; }
	[JsonProperty("export_fare")] public decimal? ExportFare { get; set; }
}

public class Journey
{
	[JsonProperty("category")] public string Category { get; set; }
	[JsonProperty("start_town_id")] public string StartTownId { get; set; }
	[JsonProperty("end_town_id")] public string EndTownId { get; set; }
	[JsonProperty("distance")] public decimal? Distance { get; set; }
	[JsonProperty("moves")] public decimal? Moves { get; set; }
	[JsonProperty("legs")] public List<JourneyLeg> Legs { get; set; }
}

public class JourneyLeg
{
	[JsonProperty("path")] public List<JourneyPoint> Path { get; set; }
}

public class JourneyPoint
{
	[JsonProperty("x")] public int X { get; set; }
	[JsonProperty("y")] public int Y { get; set; }
	[JsonProperty("c")] public decimal? Cost { get; set; }
}

