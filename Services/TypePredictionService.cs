using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Linca_David_ProiectMPA.Models;

namespace Linca_David_ProiectMPA.Services
{
    public class TypePredictionService : ITypePredictionService
    {
        private readonly HttpClient _httpClient;

        public TypePredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private class ApiResponse
        {
            public string Prediction { get; set; }
        }

        public async Task<string> PredictAsync(TypePredictionInput input)
        {
            var response = await _httpClient.PostAsJsonAsync("/predict", input);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<ApiResponse>();
            return result?.Prediction;
        }
    }
}
