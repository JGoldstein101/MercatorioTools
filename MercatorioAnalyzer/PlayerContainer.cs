namespace MercatorioAnalyzer;

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

    public PlayerData playerInfo {get; set;}
}

public partial class PlayerData
{

    public string username { get; set; }

    public Household household { get; set; }

    [JsonProperty("discord_id")]
    public string DiscordId { get; set; }

    [JsonProperty("settings")]
    public Settings Settings { get; set; }

    [JsonProperty("bonus_points")]
    public long BonusPoints { get; set; }

    [JsonProperty("discount_points")]
    public long DiscountPoints { get; set; }

    [JsonProperty("active")]
    public bool Active { get; set; }

    [JsonProperty("activity")]
    public MercatorioAnalyzer.Activity Activity { get; set; }
}

public partial class Activity
{
    [JsonProperty("score")]
    public double Score { get; set; }

    [JsonProperty("index")]
    public long Index { get; set; }
}

public partial class Household
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("town_id")]
    public long TownId { get; set; }

    [JsonProperty("portrait")]
    public string Portrait { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }

    [JsonProperty("account_id")]
    public string AccountId { get; set; }

    [JsonProperty("business_ids")]
    public long[] BusinessIds { get; set; }

    [JsonProperty("prestige")]
    public string Prestige { get; set; }

    [JsonProperty("prestige_impacts")]
    public PreImpact[] PrestigeImpacts { get; set; }

    [JsonProperty("legacy")]
    public string Legacy { get; set; }

    [JsonProperty("ascent")]
    public string Ascent { get; set; }

    [JsonProperty("prestige_board")]
    public PrestigeBoard PrestigeBoard { get; set; }

    [JsonProperty("workers")]
    public Worker[] Workers { get; set; }

    [JsonProperty("specialization")]
    public Specialization Specialization { get; set; }

    [JsonProperty("operations")]
    public string[] Operations { get; set; }

    [JsonProperty("caps")]
    public Caps Caps { get; set; }

    [JsonProperty("sustenance")]
    public Sustenance Sustenance { get; set; }
}

public partial class Caps
{
    [JsonProperty("transports")]
    public long Transports { get; set; }

    [JsonProperty("bonus_slots")]
    public long BonusSlots { get; set; }

    [JsonProperty("regular_management")]
    public long RegularManagement { get; set; }

    [JsonProperty("prestige_management")]
    public long PrestigeManagement { get; set; }

    [JsonProperty("tenants")]
    public long Tenants { get; set; }

    [JsonProperty("apprentices")]
    public long Apprentices { get; set; }

    [JsonProperty("contracts")]
    public long Contracts { get; set; }
}

public partial class PrestigeBoard
{
    [JsonProperty("prestige")]
    public string Prestige { get; set; }

    [JsonProperty("prestige_impacts")]
    public PreImpact[] PrestigeImpacts { get; set; }

    [JsonProperty("previous_impacts")]
    public PreImpact[] PreviousImpacts { get; set; }

    [JsonProperty("current_impacts")]
    public object CurrentImpacts { get; set; }

    [JsonProperty("legacy")]
    public string Legacy { get; set; }

    [JsonProperty("ascent")]
    public string Ascent { get; set; }

    [JsonProperty("allocated")]
    public long Allocated { get; set; }

    [JsonProperty("tenants_level")]
    public long TenantsLevel { get; set; }

    [JsonProperty("management_level")]
    public long ManagementLevel { get; set; }

    [JsonProperty("transports_level")]
    public long TransportsLevel { get; set; }

    [JsonProperty("prestige_management_level")]
    public long PrestigeManagementLevel { get; set; }

    [JsonProperty("cargo_slots_level")]
    public long CargoSlotsLevel { get; set; }

    [JsonProperty("apprentices_level")]
    public long ApprenticesLevel { get; set; }

    [JsonProperty("luxuries_level")]
    public long LuxuriesLevel { get; set; }

    [JsonProperty("scale_luxuries_level")]
    public long ScaleLuxuriesLevel { get; set; }

    [JsonProperty("contracts_level")]
    public long ContractsLevel { get; set; }
}

public partial class PreImpact
{
    [JsonProperty("factor")]
    public string Factor { get; set; }

    [JsonProperty("impact")]
    public string Impact { get; set; }
}

public partial class Specialization
{
    [JsonProperty("points")]
    public Points Points { get; set; }
}

public partial class Points
{
}

public partial class Sustenance
{
    [JsonProperty("reference")]
    public string Reference { get; set; }

    [JsonProperty("inventory")]
    public Inventory Inventory { get; set; }

    [JsonProperty("provider_id")]
    public string ProviderId { get; set; }
}

public partial class Inventory
{
    [JsonProperty("account")]
    public Account Account { get; set; }

    [JsonProperty("capacity")]
    public long Capacity { get; set; }

    [JsonProperty("holdings")]
    public Holdings Holdings { get; set; }

    [JsonProperty("flows")]
    public Dictionary<string, Flow> Flows { get; set; }

    [JsonProperty("previous_flows")]
    public Dictionary<string, PreviousFlow> PreviousFlows { get; set; }

    [JsonProperty("settings")]
    public Points Settings { get; set; }
}

public partial class Account
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("owner_id")]
    public long OwnerId { get; set; }

    [JsonProperty("master_id")]
    public long MasterId { get; set; }

    [JsonProperty("assets")]
    public Dictionary<string, Asset> Assets { get; set; }

    [JsonProperty("_ts")]
    public long Ts { get; set; }
}

public partial class Asset
{
    [JsonProperty("balance")]
    public string Balance { get; set; }

    [JsonProperty("reserved")]
    public long Reserved { get; set; }

    [JsonProperty("capacity", NullValueHandling = NullValueHandling.Ignore)]
    public long? Capacity { get; set; }

    [JsonProperty("unit_cost")]
    public string UnitCost { get; set; }
}

public partial class Flow
{
    [JsonProperty("projection")]
    public PreviousFlow Projection { get; set; }
}

public partial class PreviousFlow
{
    [JsonProperty("consumption")]
    public string Consumption { get; set; }
}

public partial class Holdings
{
    [JsonProperty("arms")]
    public Arms Arms { get; set; }

    [JsonProperty("beer")]
    public Arms Beer { get; set; }

    [JsonProperty("cheese")]
    public Arms Cheese { get; set; }

    [JsonProperty("cookware")]
    public Arms Cookware { get; set; }

    [JsonProperty("donations")]
    public Donations Donations { get; set; }

    [JsonProperty("firewood")]
    public Arms Firewood { get; set; }

    [JsonProperty("garments")]
    public Arms Garments { get; set; }

    [JsonProperty("grain")]
    public Arms Grain { get; set; }

    [JsonProperty("jewellery")]
    public Arms Jewellery { get; set; }

    [JsonProperty("light armour")]
    public Arms LightArmour { get; set; }

    [JsonProperty("meat")]
    public Donations Meat { get; set; }
}

public partial class Arms
{
    [JsonProperty("managers")]
    public Manager[] Managers { get; set; }

    [JsonProperty("capacity")]
    public long Capacity { get; set; }
}

public partial class Manager
{
    [JsonProperty("max_holding")]
    public long MaxHolding { get; set; }

    [JsonProperty("buy_volume")]
    public long BuyVolume { get; set; }

    [JsonProperty("buy_price")]
    public string BuyPrice { get; set; }
}

public partial class Donations
{
    [JsonProperty("managers")]
    public Manager[] Managers { get; set; }
}

public partial class Worker
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("skills")]
    public Skills Skills { get; set; }

    [JsonProperty("assignment")]
    public string Assignment { get; set; }

    [JsonProperty("capacity")]
    public long Capacity { get; set; }

    [JsonProperty("female", NullValueHandling = NullValueHandling.Ignore)]
    public bool? Female { get; set; }
}

public partial class Skills
{
    [JsonProperty("textile")]
    public string Textile { get; set; }

    [JsonProperty("maritime", NullValueHandling = NullValueHandling.Ignore)]
    public string Maritime { get; set; }

    [JsonProperty("mercantile", NullValueHandling = NullValueHandling.Ignore)]
    public string Mercantile { get; set; }
}

public partial class Settings
{
    [JsonProperty("sound_volume")]
    public long SoundVolume { get; set; }

    [JsonProperty("notifications")]
    public Notifications Notifications { get; set; }

    [JsonProperty("email")]
    public Email Email { get; set; }
}

public partial class Email
{
    [JsonProperty("season_notifications")]
    public bool SeasonNotifications { get; set; }

    [JsonProperty("character_export")]
    public bool CharacterExport { get; set; }

    [JsonProperty("marketing_emails")]
    public bool MarketingEmails { get; set; }
}

public partial class Notifications
{
    [JsonProperty("discord")]
    public bool Discord { get; set; }

    [JsonProperty("mutes")]
    public object Mutes { get; set; }
}
