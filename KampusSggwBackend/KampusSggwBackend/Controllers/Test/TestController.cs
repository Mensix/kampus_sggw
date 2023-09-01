namespace KampusSggwBackend.Controllers.Test;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    // Constructor
    public TestController()
    {
    }

    // Methods
    [HttpGet]
    public async Task<ActionResult> TestGet(string param)
    {
        return Ok();
    }

    [HttpPost] 
    public async Task<ActionResult> Post(string param) 
    {
        return Ok();
    }
}
