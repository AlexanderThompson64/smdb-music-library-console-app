using synergi_smd_coding_test.Classes;

namespace synergi_smd_coding_test.Interfaces;

public interface IMusicService
{
    Task<List<Song>> GetSongsByGenreAsync(string genre);
    Task<List<Song>> GetAllSongsAsync();
}