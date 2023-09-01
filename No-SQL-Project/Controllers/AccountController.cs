using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using No_SQL_Project.Controllers.Dtos;
using No_SQL_Project.Services;

namespace No_SQL_Project.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;
    
    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public string Login([FromBody] UserLoginRequest request)
    {
        var token = _accountService.Login(request);
        
        return token;
    }
}