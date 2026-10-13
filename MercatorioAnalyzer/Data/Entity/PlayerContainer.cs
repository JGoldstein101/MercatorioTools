namespace MercatorioAnalyzer.Data.Entity;

using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net.Http.Headers;

public class PlayerContainer
{
    public string apiKey { get; set; } = "";
    public string apiEmail { get; set;} = "";
    public PlayerData? playerData {get; set;}
}

public class PlayerData
{
    public string username { get; set; } = "";
    public Household? household { get; set; }
    [JsonProperty("discord_id")] public string discordId { get; set; } = "";
    public PlayerSettings? settings { get; set; }
    [JsonProperty("bonus_points")] public int bonusPoints { get; set; }
    [JsonProperty("discount_points")] public int discountPoints { get; set; }
    public bool active { get; set; }
    public Activity? activity { get; set; }
}

public class Household
{
    public string id { get; set; } = "";
    public string name { get; set; } = "";
    [JsonProperty("town_id")] public string townId { get; set; } = "";
    public string portrait { get; set; } = "";
    public string gender { get; set; } = "";
    [JsonProperty("account_id")] public string accountId { get; set; } = "";
    [JsonProperty("business_ids")] public List<string> businessIds { get; set; } = new List<string>();
    public string prestige { get; set; } = "";
    [JsonProperty("prestige_impacts")] public List<PrestigeImpact> prestigeImpacts { get; set; } = new List<PrestigeImpact>();
    public string legacy { get; set; } = "";
    public string ascent { get; set; } = "";
    [JsonProperty("prestige_board")] public PrestigeBoard prestigeBoard { get; set; }
    public List<Worker> workers { get; set; } = new List<Worker>();
    public Specialization specialization { get; set; }
    public List<string> operations { get; set; } = new List<string>();
    public HouseholdCaps caps { get; set; }
    public Sustenance sustenance { get; set; }
}

public class PrestigeImpact
{
    public string factor { get; set; } = "";
    public string impact { get; set; } = "";
}

public class PrestigeBoard
{
    public string prestige { get; set; } = "";
    [JsonProperty("prestige_impacts")] public List<PrestigeImpact> prestigeImpacts { get; set; } = new List<PrestigeImpact>();
    [JsonProperty("previous_impacts")] public List<PrestigeImpact> previousImpacts { get; set; } = new List<PrestigeImpact>();
    [JsonProperty("current_impacts")] public List<PrestigeImpact> currentImpacts { get; set; }
    public string legacy { get; set; } = "";
    public string ascent { get; set; } = "";
    public string allocated { get; set; } = "";
    [JsonProperty("tenants_level")] public int tenantsLevel { get; set; }
    [JsonProperty("management_level")] public int managementLevel { get; set; }
    [JsonProperty("transports_level")] public int transportsLevel { get; set; }
    [JsonProperty("prestige_management_level")] public int prestigeManagementLevel { get; set; }
    [JsonProperty("cargo_slots_level")] public int cargoSlotsLevel { get; set; }
    [JsonProperty("apprentices_level")] public int apprenticesLevel { get; set; }
    [JsonProperty("luxuries_level")] public int luxuriesLevel { get; set; }
    [JsonProperty("scale_luxuries_level")] public int scaleLuxuriesLevel { get; set; }
    [JsonProperty("contracts_level")] public int contractsLevel { get; set; }
}

public class Worker
{
    public string name { get; set; } = "";
    public bool female { get; set; }
    public Dictionary<string, string> skills { get; set; } = new Dictionary<string, string>();
    public string assignment { get; set; } = "";
    public string capacity { get; set; } = "";
}

public class Specialization
{
    public Dictionary<string, object> points { get; set; } = new Dictionary<string, object>();
}

public class HouseholdCaps
{
    public int transports { get; set; }
    [JsonProperty("bonus_slots")] public int bonusSlots { get; set; }
    [JsonProperty("regular_management")] public int regularManagement { get; set; }
    [JsonProperty("prestige_management")] public int prestigeManagement { get; set; }
    public int tenants { get; set; }
    public int apprentices { get; set; }
    public int contracts { get; set; }
}

public class Sustenance
{
    public string reference { get; set; } = "";
    public Inventory inventory { get; set; }
    [JsonProperty("provider_id")] public string providerId { get; set; } = "";
}

public class Inventory
{
    public InventoryAccount account { get; set; }
    public int capacity { get; set; }
    public Dictionary<string, Holding> holdings { get; set; } = new Dictionary<string, Holding>();
    public Dictionary<string, ProjectedFlow> flows { get; set; } = new Dictionary<string, ProjectedFlow>();
    [JsonProperty("previous_flows")] public Dictionary<string, PreviousFlow> previousFlows { get; set; } = new Dictionary<string, PreviousFlow>();
    public Dictionary<string, object> settings { get; set; } = new Dictionary<string, object>();
}

public class InventoryAccount
{
    public string id { get; set; } = "";
    public string name { get; set; } = "";
    [JsonProperty("owner_id")] public string ownerId { get; set; } = "";
    [JsonProperty("master_id")] public string masterId { get; set; } = "";
    public Dictionary<string, Asset> assets { get; set; } = new Dictionary<string, Asset>();
    [JsonProperty("_ts")] public long timestamp { get; set; }
}

public class Asset
{
    public string balance { get; set; } = "";
    public string reserved { get; set; } = "";
    public string capacity { get; set; }
    [JsonProperty("unit_cost")] public string unitCost { get; set; } = "";
}

public class Holding
{
    public List<Manager> managers { get; set; }
    public int? capacity { get; set; }
}

public class Manager
{
    [JsonProperty("max_holding")] public int maxHolding { get; set; }
    [JsonProperty("buy_volume")] public int buyVolume { get; set; }
    [JsonProperty("buy_price")] public string buyPrice { get; set; } = "";
}

public class ProjectedFlow
{
    public FlowProjection projection { get; set; }
}

public class FlowProjection
{
    public string consumption { get; set; } = "";
}

public class PreviousFlow
{
    public string consumption { get; set; } = "";
}

public class PlayerSettings
{
    [JsonProperty("sound_volume")] public int soundVolume { get; set; }
    public NotificationSettings notifications { get; set; }
    public EmailSettings email { get; set; }
}

public class NotificationSettings
{
    public bool discord { get; set; }
    public List<string> mutes { get; set; }
}

public class EmailSettings
{
    [JsonProperty("season_notifications")] public bool seasonNotifications { get; set; }
    [JsonProperty("character_export")] public bool characterExport { get; set; }
    [JsonProperty("marketing_emails")] public bool marketingEmails { get; set; }
}

public class Activity
{
    public double score { get; set; }
    public int index { get; set; }
}

