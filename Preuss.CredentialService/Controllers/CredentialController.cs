using System;
using Microsoft.AspNetCore.Mvc;
using Preuss.CredentialService.Abstracts.Processors;
using Preuss.CredentialService.MongoAccess.Abstracts.DataClasses;

namespace Preuss.CredentialService.Controllers;

[ApiController]
[Route("[controller]")]
public class CredentialController : ControllerBase
{
    private readonly ILogger<CredentialController> _logger;
    private readonly ICredentialProcessor _proseccor;

    public CredentialController(ILogger<CredentialController> logger,
        ICredentialProcessor processor)
	{
        _logger = logger;
        _proseccor = processor;
	}

    [HttpGet(Name = "Login")]
    public async Task<IActionResult> Get()
    {
        var user = await _proseccor.LoginAsync("", "");

        return new OkObjectResult(user);
    }
}

