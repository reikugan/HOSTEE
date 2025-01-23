using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using HOSTEE.Models;
using System.Net.Http.Headers;

namespace HOSTEE.Services
{
    public class ChatBotService
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
        public string GtpModel { get; set; }

        private readonly HttpClient _httpClient;
        private readonly IOptions<ChatBotService> _options;

        public ChatBotService(HttpClient httpClient, IOptions<ChatBotService> options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        /*
        public async Task<string> GetResponseAsync(string userInput)
        {
            var response = await _httpClient.PostAsJsonAsync("https://api.openai.com/v1/completions", new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "user", content = userInput }
            }
            });

            var responseContent = await response.Content.ReadFromJsonAsync<JsonElement>();
            return responseContent.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
        }
        */

        public async Task<Message> CreateChatCompletion(List<Message> messages)
        {
            var request = new { model = _options.Value.GtpModel, messages = messages.ToArray() };

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _options.Value.ApiKey);
            var response = await _httpClient.PostAsJsonAsync(_options.Value.ApiUrl, request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadFromJsonAsync<ChatBotResponse>();
            return responseContent?.choices.First().message;
        }
    }
}
