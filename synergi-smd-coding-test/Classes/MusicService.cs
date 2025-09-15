using System.Text.Json;
using synergi_smd_coding_test.Interfaces;

namespace synergi_smd_coding_test.Classes;

public class MusicService : IMusicService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://smdb.azurewebsites.net/api/songs";

    public MusicService(string bearerToken)
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
    }

    public async Task<List<Song>> GetSongsByGenreAsync(string genre)
    {
        try
        {
            Console.WriteLine($"Calling API with genre {genre}: {_baseUrl}?genre={genre}");
            var response = await _httpClient.GetAsync($"{_baseUrl}?genre={genre}");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response: {json}");

            var songs = JsonSerializer.Deserialize<List<Song>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Song>();

            Console.WriteLine($"Deserialized {songs.Count} songs");
            return songs;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API Error in GetSongsByGenreAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<List<Song>> GetAllSongsAsync()
    {
        try
        {
            Console.WriteLine($"Calling API: {_baseUrl}");
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API Response: {json}");

            var songs = JsonSerializer.Deserialize<List<Song>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Song>();

            Console.WriteLine($"Deserialized {songs.Count} songs");
            return songs;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API Error in GetAllSongsAsync: {ex.Message}");
            throw;
        }
    }
}