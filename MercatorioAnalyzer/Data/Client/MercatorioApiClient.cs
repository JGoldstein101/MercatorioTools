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
        //Make sure the player info is set up
        if(String.IsNullOrWhiteSpace(playerContainer.apiEmail) || String.IsNullOrWhiteSpace(playerContainer.apiKey))
        {
            throw new InvalidOperationException("Mercatorio creds not set");
        }
        var body = await makeRequest("player");

        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(body)
                    ?? throw new JsonException("The player response was empty.");

        return playerData;

    }
}
