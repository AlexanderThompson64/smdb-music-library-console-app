namespace synergi_smd_coding_test.Interfaces;

public interface IAuthenticationService
{
    Task<IMusicService> AuthenticateAsync();
}