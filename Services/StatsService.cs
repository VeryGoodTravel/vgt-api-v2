using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using vgt_api.Models.Responses;

namespace vgt_api.Services;

public class StatsService
{
    private readonly HttpClient _httpClient;
    private readonly ConfigurationService _configurationService;
    private readonly ILogger<StatsService> _logger;

    public StatsService(ConfigurationService configurationService, HttpClient httpClient,
        ILogger<StatsService> logger)
    {
        _configurationService = configurationService;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<StatsResults> GetStats()
    {
        var response = await _httpClient.GetAsync(_configurationService.StatsApiUrl + "/PopularOffers");
        var content = await response.Content.ReadAsStringAsync();
        
        _logger.LogInformation("Received popular offers: |{p}|", content);
        
        return JsonConvert.DeserializeObject<StatsResults>(content);
    }

    public async Task<int> CheckOfferPopularity(string id)
    {
        var httpRequest = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(_configurationService.StatsApiUrl + "/OfferPopularity"),
            Content = new StringContent(id, Encoding.UTF8, MediaTypeNames.Application.Json)
        };
        var response = await _httpClient.SendAsync(httpRequest);
        var content = await response.Content.ReadAsStringAsync();
        
        _logger.LogInformation("Received offer popularity: {p}", content);

        return int.Parse(content);
    }
}