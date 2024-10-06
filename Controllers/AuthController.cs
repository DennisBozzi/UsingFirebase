using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using UsingFirebase.Services;

namespace UsingFirebase.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost ("Register")]
    public async Task<string> Register(string email, string password)
    {
        return await _authService.RegisterAsync(email, password);
    }
    
    [HttpPost ("Login")]
    public async Task<string> Login(string email, string password)
    {
        return await _authService.LoginAsync(email, password);
    }
    
    [HttpGet ("User")]
    public async Task<UserRecord> GetUser(string uid)
    {
        return await _authService.GetUserAsync(uid);
    }
}