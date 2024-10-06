using FirebaseAdmin.Auth;
using UsingFirebase.Models;

namespace UsingFirebase.Services;

public class AuthService
{
    
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
    }
    
    public async Task<string> RegisterAsync(string email, string password)
    {
        var userArgs = new UserRecordArgs
        {
            Email = email,
            Password = password
        };

        var userRecord = await FirebaseAuth.DefaultInstance.CreateUserAsync(userArgs);

        return userRecord.Uid;
    }
    
    public async Task<string> LoginAsync(string email, string password)
    {
        var requestPayload = new
        {
            email,
            password,
            returnSecureToken = true
        };

        var tokenUri = Environment.GetEnvironmentVariable("FIREBASE_TOKEN_URI");

        HttpResponseMessage response;

        response = await _httpClient.PostAsJsonAsync(tokenUri, requestPayload);

        var responseContent = await response.Content.ReadFromJsonAsync<FirebaseUser>();

        return responseContent.IdToken;
    }

    public async Task<UserRecord> GetUserAsync(string uid)
    {
        var userRecord = await FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        return userRecord;
    }
    
}