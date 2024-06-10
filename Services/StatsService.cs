using Newtonsoft.Json;
using vgt_api.Models.Common;
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

    public async Task<StatsResponse> GetStats()
    {
        return new StatsResponse
        {
            PopularDirections = new[] { PopularDirection.GetExample() },
            PopularAccommodations = new[] { PopularAccommodation.GetExample() }
        };
        
        var response = await _httpClient.GetAsync(_configurationService.StatsApiUrl + "/stats");
        var content = await response.Content.ReadAsStringAsync();
        
        return JsonConvert.DeserializeObject<StatsResponse>(content);
    }
}