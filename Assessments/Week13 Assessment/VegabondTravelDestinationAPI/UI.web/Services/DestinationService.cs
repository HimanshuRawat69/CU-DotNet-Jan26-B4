using UI.web.Models;

namespace UI.web.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly HttpClient _http;

        public DestinationService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
        {
            var result = await _http.GetFromJsonAsync<IEnumerable<Destination>>("api/destinations");
            return result;
        }
    }
}
