using System;
using Microsoft.AspNetCore.Mvc;
using Preuss.CredentialService.Abstracts.Processors;
using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;
using Preuss.CredentialService.Validation.Abstracts;

namespace Preuss.CredentialService.Controllers;

[ApiController]
[Route("[controller]")]
public class CredentialController : ControllerBase
{
    private readonly ILogger<CredentialController> _logger;
    private readonly ICredentialProcessor _processor;
    private readonly ICredentialsValidator _validator;

    public CredentialController(ILogger<CredentialController> logger,
        ICredentialProcessor processor, ICredentialsValidator validator)
	{
        _logger = logger;
        _processor = processor;
        _validator = validator;
	}

    [HttpPost(Name = "Login")]
    [Route("[controller]/login")]
    public async Task<IActionResult> Login([FromBody] Credentials credentials)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(credentials.Username))
                return new BadRequestObjectResult("User name is not valid.");

            if (string.IsNullOrWhiteSpace(credentials.HashedPassword))
                return new BadRequestObjectResult("User password is not valid.");

            var user = await _processor.LoginAsync(credentials.Username,
                credentials.HashedPassword);

            if (user is null)
                return new BadRequestObjectResult("User not found.");

            return new OkObjectResult(user);
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }

    [HttpPost(Name = "CreateUser")]
    [Route("[controller]/create-user")]
    public async Task<IActionResult> CreateUser([FromBody] Credentials credentials)
    {
        try
        {
            if (!_validator.IsValidEmail(credentials.Email))
                return new BadRequestObjectResult("User email is not valid.");

            if (!_validator.IsValidUsername(credentials.Username))
                return new BadRequestObjectResult("User name is not valid.");

            if (!_validator.IsValidPassword(credentials.HashedPassword))
                return new BadRequestObjectResult("User password is not valid.");

            await _processor.CreateUserAsync(credentials, credentials.HashedPassword);

            return new OkResult();
        }
        catch (Exception ex)
        {
            return new BadRequestObjectResult(ex);
        }
    }
}

