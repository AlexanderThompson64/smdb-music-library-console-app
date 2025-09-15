namespace synergi_smd_coding_test.Classes;

public interface IFileService
{
    Task SaveSongsAsync(List<Song> songs);
}