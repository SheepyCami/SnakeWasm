using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;

namespace Slangespillet.Game.Services
{
    public class LeaderboardService
    {
     
            private readonly HttpClient http;

            public LeaderboardService(HttpClient httpClient, IConfiguration config)
            {
                http = httpClient;

                string supabaseUrl = config["Supabase:Url"]!;
                string supabaseKey = config["Supabase:AnonKey"]!;

                http.BaseAddress = new Uri(supabaseUrl);
                http.DefaultRequestHeaders.Add("apikey", supabaseKey);
                http.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseKey}");
            }

            public async Task SubmitScoreAsync(string name, int score)
        {
            var entry = new { name, score };
            await http.PostAsJsonAsync("/rest/v1/highscores", entry);
        }

        public async Task<int> GetPlayerBestScoreAsync(string name)
        {
            var url = $"/rest/v1/highscores?name=eq.{name}&select=score&order=score.desc&limit=1";
            var result = await http.GetFromJsonAsync<List<ScoreEntry>>(url);
            return result?.FirstOrDefault()?.Score ?? 0;
        }

        public async Task<List<ScoreEntry>> GetTopScoresAsync()
        {
            var url = "/rest/v1/highscores?select=*&order=score.desc&limit=5";
            return await http.GetFromJsonAsync<List<ScoreEntry>>(url) ?? new();
        }

        public async Task<int?> GetPlayerRankAsync(string name, int score)
        {
            var url = "/rest/v1/rpc/get_player_rank";
            var body = new { pname = name, pscore = score };
            var response = await http.PostAsJsonAsync(url, body);

            if (response.IsSuccessStatusCode)
            {
                var rank = await response.Content.ReadFromJsonAsync<int?>();
                return rank;
            }

            return null;
        }

    }

    public class ScoreEntry
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}