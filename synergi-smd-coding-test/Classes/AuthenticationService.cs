using synergi_smd_coding_test.Interfaces;

namespace synergi_smd_coding_test.Classes;

public class AuthenticationService : IAuthenticationService
{
    public async Task<IMusicService> AuthenticateAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Synergi SMDb Music Library!");
            Console.WriteLine("------------------------------------------");
            
            Console.Write("Please enter your SMDb bearer token: ");
            string? bearerToken = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(bearerToken))
            {
                DisplayError("Bearer token cannot be empty. Please try again.");
                continue; 
                //Continues the loop to ask for the token again
            }
    
            // Creates a new instance of MusicService using the validated bearer token
            var musicService = new MusicService(bearerToken);
            
            // Attempts to fetch all songs to validate the token
            try
            {
                await musicService.GetAllSongsAsync();
                return musicService;
            }
            catch (UnauthorizedAccessException)
            {
                DisplayError("Invalid bearer token. Please try again.");
            }
            catch (Exception ex)
            {
                DisplayError($"Error: {ex.Message}");
            }
        }
    }

    private void DisplayError(string message)
    {
        Console.WriteLine(message);
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}