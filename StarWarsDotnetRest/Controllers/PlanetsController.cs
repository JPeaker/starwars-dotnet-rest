namespace StarWarsDotnetRest.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StarWarsApiCSharp;

    [ApiController]
    [Route("[controller]")]
    public class PlanetsController : ControllerBase
    {
        private readonly ILogger<PlanetsController> _logger;
        private readonly IRepository<Planet> repository;

        public PlanetsController(ILogger<PlanetsController> logger, IRepository<Planet> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public ICollection<Planet> GetPlanets([FromQuery]int page = 1, [FromQuery]int limit = 10)
        {
            var res = this.repository.GetEntities(page, limit);
            return res;
        }

        [HttpGet("{id}")]
        public Planet Get(int id)
        {
            var res = this.repository.GetById(id);
            return res;
        }
    }
}
