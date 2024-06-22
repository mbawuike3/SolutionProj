using Microsoft.AspNetCore.Mvc;
using ProjectKpi.Helpers;
using ProjectKpi.Models;

namespace ProjectKpi.Controllers
{
    [Route("api/v1/cryptic")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpConnection _connection;

        public SolutionController(IConfiguration configuration, IHttpConnection connection)
        {
            _configuration = configuration;
            _connection = connection;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           return Ok( await _connection.WebConnection<ClassResponse>(_configuration["Endpoint:url"]!, "get"));
        }
        [HttpGet("rank")]
        public async Task<IActionResult> Get(string rank)
        {
            var cyrpto = await _connection.WebConnection<ClassResponse>(_configuration["Endpoint:url"]!, "get");
            if(cyrpto == null)
            {
                return NotFound();
            }
            var filteredRank = cyrpto.data!.Where(x => x.rank == rank).FirstOrDefault();
            if (filteredRank == null)
            {
                return NotFound("No rank found");
            }
            return Ok(filteredRank);
        }
    }
}
