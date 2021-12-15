using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace PlanADevOpsChallenge1API.Controllers;

[ApiController]
[Route("/")]
[Produces("application/json")]
public class SampleDataController : ControllerBase
{

    private readonly ILogger<SampleDataController> _logger;

    public SampleDataController(ILogger<SampleDataController> logger)
    {
        _logger = logger;
    }

    private Dictionary<string, string> LoadResponse(SampleData sdata)
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        result.Add("timestamp",sdata.timestamp);
        result.Add("hostname",sdata.hostname);
        result.Add("engine",sdata.engine);
        result.Add("visitor ip",sdata.visitorip);
        return result;
    }

    private async Task<string> GetDockerVersion()
    {
        DockerClient dkrclient = new DockerClientConfiguration().CreateClient();
        VersionResponse returndockerversion =  await dkrclient.System.GetVersionAsync();

        return returndockerversion.Version;
    }

    [HttpGet]
    public Dictionary<string, string> Get()
    {   //WARNING: Uncomment dockerversion if running the application in your workstation (with or without debugging) 
        //and NOT in a Docker container.
        //When inside a container, is losing context so the Docker Client to check on the engine version is not reachable.
        //var dockerversion = GetDockerVersion();        
        SampleData sdata = new SampleData
        {            
            timestamp = DateTime.Now.ToString(),
            hostname = HttpContext.Request.Host.Value,
            engine = "TBD",//dockerversion.Result,
            visitorip = HttpContext.Connection.RemoteIpAddress?.ToString()
        };

        Dictionary<string, string> response = LoadResponse(sdata);

        return response;
    }

    [Route("/error")]
    public IActionResult HandleError()
    {            
        return StatusCode(500, "Estallado!");
    }

}
