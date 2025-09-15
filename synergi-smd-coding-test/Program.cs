using synergi_smd_coding_test.Classes;
using synergi_smd_coding_test.Interfaces;

// Program.cs
class Program
{
    static async Task Main(string[] args)
    {
        //Calls authentication service to check that bearer token is valid
        IAuthenticationService authService = new AuthenticationService();
        //File service handles deserializing and saving songs to a text file
        IFileService fileService = new FileService();
        //Music service handles all interaction with the SMDb API
        IMusicService musicService = await authService.AuthenticateAsync();
        //Console UI handles all user interaction
        var ui = new ConsoleUI(musicService, fileService);
        await ui.Run();
    }
}
