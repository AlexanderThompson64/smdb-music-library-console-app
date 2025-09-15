using synergi_smd_coding_test.Interfaces;

namespace synergi_smd_coding_test.Classes;

public class ConsoleUI
{
    private readonly IMusicService _musicService;
    private readonly IFileService _fileService;
    private bool _running = true;

    public ConsoleUI(IMusicService musicService, IFileService fileService)
    {
        _musicService = musicService;
        _fileService = fileService;
    }

    public async Task Run()
    {
        while (_running)
        {
            Console.Clear();
            DisplayMenu();

            var choice = Console.ReadLine();
            await HandleMenuChoice(choice);
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("Synergi SMDb Music Library");
        Console.WriteLine("-------------------------");
        Console.WriteLine("\nPlease select an option:");
        Console.WriteLine("1. View all songs");
        Console.WriteLine("2. Search songs by genre");
        Console.WriteLine("3. Exit");
        Console.Write("\nChoice: ");
    }

    private async Task HandleMenuChoice(string? choice)
    {
        switch (choice)
        {
            case "1":
                Console.Clear();
                await DisplayAndSaveSongs(_musicService.GetAllSongsAsync());
                break;

            case "2":
                Console.Clear();
                await HandleGenreSearch();
                break;

            case "3":
                _running = false;
                Console.Clear();
                Console.WriteLine("Exiting application. Goodbye!");
                break;

            default:
                Console.WriteLine("Invalid choice, try again.");
                WaitForKeyPress();
                break;
        }
    }

    private async Task HandleGenreSearch()
    {
        Console.Write("Enter a genre (indie/rock/pop): ");
        var genre = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(genre))
        {
            Console.WriteLine("Genre cannot be empty. Please try again.");
            WaitForKeyPress();
            return;
        }

        await DisplayAndSaveSongs(_musicService.GetSongsByGenreAsync(genre));
    }

    private async Task DisplayAndSaveSongs(Task<List<Song>> songsTask)
    {
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        try
        {
            List<Song> songs = await songsTask;


            if (songs.Count == 0)
            {
                Console.WriteLine("No songs found.");
                WaitForKeyPress();
                return;
            }

            Console.WriteLine("\nSongs - " + timestamp);
            Console.WriteLine("------");
            songs.ForEach(s => Console.WriteLine(s));

            await _fileService.SaveSongsAsync(songs);
            Console.WriteLine("\nSongs saved to file successfully.");
            WaitForKeyPress();
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("\nYour session has expired. Please restart the application.");
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            Environment.Exit(1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
            WaitForKeyPress();
        }
    }

    private void WaitForKeyPress()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}