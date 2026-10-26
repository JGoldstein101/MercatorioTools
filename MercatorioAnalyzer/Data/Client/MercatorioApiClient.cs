using System.Net.Http.Headers;
using MercatorioAnalyzer.Data.Entity;
using Newtonsoft.Json;

namespace MercatorioAnalyzer.Data.Client;

public class MercatorioApiClient(
    HttpClient httpClient,
    PlayerContainer playerContainer)
{
    private async Task<String> makeRequest(
        string _dataPath,
        HttpMethod? _method = null)
    {
        //Make sure the player info is set up
        if(String.IsNullOrWhiteSpace(playerContainer.apiEmail) || String.IsNullOrWhiteSpace(playerContainer.apiKey))
        {
            throw new InvalidOperationException("Mercatorio creds not set");
        }

        var requestmethod = _method ?? HttpMethod.Get;
        using (var request = new HttpRequestMessage
        {
            Method = requestmethod,
            RequestUri = new Uri(_dataPath, UriKind.Relative),
            Headers =
                {
                    {"X-Merc-User", playerContainer.apiEmail},
                    {"Authorization", "Bearer " + playerContainer.apiKey},
                },   
        })
        {
            using (var response = await httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }   
        }
    }

    public async Task<PlayerData> GetPlayerDataAsync()
    {

        var body = await makeRequest("player");

        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(body)
                    ?? throw new JsonException("The player response was empty.");

        return playerData;

    }
    
    public async Task<List<Recipe>> GetRecipesAsync()
    {
        var body = await makeRequest("config/recipes");
        var recipeDictionary = JsonConvert.DeserializeObject<Dictionary<string, Recipe>>(body) ?? throw new JsonException("The recipe response was empty");
        List<Recipe> recipes = recipeDictionary.Values.ToList<Recipe>(); 
        return recipes;
    }

    public async Task<Building> GetBuildingAsync(string buildingId)
    {
        var body = await makeRequest("buildings/" + buildingId);
        var buildingObject = JsonConvert.DeserializeObject<Building>(body) ?? throw new InvalidDataException("Building response was invalid");
        return buildingObject;
    }

    public async Task<BuildingOperations> GetBuildingOperationsAsync(string buildingId)
    {
        var body = await makeRequest("buildings/" + buildingId + "/operations");
        return JsonConvert.DeserializeObject<BuildingOperations>(body) ?? throw new InvalidDataException("Building operations response was invalid");
    }

    public async Task<(Building Building, BuildingOperations Operations)> GetStorehouseDataAsync()
    {
        var storehouseId = GetStorehouseId();
        var buildingTask = GetBuildingAsync(storehouseId);
        var operationsTask = GetBuildingOperationsAsync(storehouseId);
        await Task.WhenAll(buildingTask, operationsTask);
        return (await buildingTask, await operationsTask);
    }

    public async Task<Building> GetStorehouseAsync()
    {
        return await GetBuildingAsync(GetStorehouseId());
    }

    private string GetStorehouseId()
    {
        var providerId = playerContainer.playerData?.household?.sustenance?.providerId;
        if (string.IsNullOrEmpty(providerId))
        {
            throw new InvalidDataException("Player data is not loaded or does not have a valid storehouse");
        }
        return providerId;
    }

    public async Task<Business> GetBusinessAsync(string businessId)
    {
        var body = await makeRequest("businesses/" + businessId);
        return JsonConvert.DeserializeObject<Business>(body) ?? throw new InvalidDataException("Business response was invalid");
    }

    public async Task<Transport> GetTransportAsync(string transportId)
    {
        var body = await makeRequest("transports/" + transportId);
        return JsonConvert.DeserializeObject<Transport>(body) ?? throw new InvalidDataException("Transport response was invalid");
    }

    public async Task<List<Transport>> GetPlayerTransportsAsync()
    {
        var businessId = playerContainer.playerData?.household?.businessIds.FirstOrDefault();
        if (string.IsNullOrEmpty(businessId))
        {
            throw new InvalidDataException("Player data is not loaded or does not have a business");
        }
        var business = await GetBusinessAsync(businessId);
        var transports = await Task.WhenAll(business.TransportIds.Select(GetTransportAsync));
        return transports.ToList();
    }

    public async Task<List<Product>> GetProductAsync()
    {
        var body = await makeRequest("config/products");
        List<Product> productList = JsonConvert.DeserializeObject<List<Product>>(body) ?? throw new JsonException("The product response was empty");
        return productList;
    }

    public async Task<Town> GetTownAsync(string townId)
    {
        var body = await makeRequest("towns/" + townId);
        Town town = JsonConvert.DeserializeObject<Town>(body) ?? throw new JsonException("The town response was empty or invalid");
        return town;
    }
}
