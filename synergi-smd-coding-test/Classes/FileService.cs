using System.Text;

namespace synergi_smd_coding_test.Classes;

public class FileService : IFileService
{
    private readonly string _filePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "SMDb",
        "songs.txt"
    );

    public async Task SaveSongsAsync(List<Song> songs)
    {
        // Create directory if it doesn't exist
        Directory.CreateDirectory(Path.GetDirectoryName(_filePath));

        // Build the text content
        var sb = new StringBuilder();
        sb.AppendLine("SMDb Songs List");
        sb.AppendLine("---------------");
        foreach (var song in songs)
        {
            sb.AppendLine(song.ToString());
        }

        // Save to text file
        await File.WriteAllTextAsync(_filePath, sb.ToString());
    }
}