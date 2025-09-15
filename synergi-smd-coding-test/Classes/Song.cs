using System.Text.Json.Serialization;

namespace synergi_smd_coding_test.Classes;

public class Song
{
    [JsonPropertyName("name")]
    public string? Title { get; set; }
    
    [JsonPropertyName("artistName")]
    public string? Artist { get; set; }
    
    [JsonPropertyName("genre")]
    public string? Genre { get; set; }

    public override string ToString()
    {
        return $"{Title} by {Artist} ({Genre})";
    }
}