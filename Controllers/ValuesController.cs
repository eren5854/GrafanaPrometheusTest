using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrafanaPrometheusTest.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new {Message = "message"});
    }

    [HttpGet]
    public IActionResult GetById(int Id)
    {
        return Ok(new { Message = Id });
    }
}
