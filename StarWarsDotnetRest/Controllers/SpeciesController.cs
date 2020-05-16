namespace StarWarsDotnetRest.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StarWarsApiCSharp;

    [ApiController]
    [Route("[controller]")]
    public class SpeciesController : ControllerBase
    {
        private readonly ILogger<SpeciesController> _logger;
        private readonly IRepository<Specie> repository;

        public SpeciesController(ILogger<SpeciesController> logger, IRepository<Specie> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public ICollection<Specie> GetSpecies([FromQuery]int page = 1, [FromQuery]int limit = 10)
        {
            var res = this.repository.GetEntities(page, limit);
            return res;
        }

        [HttpGet("{id}")]
        public Specie Get(int id)
        {
            var res = this.repository.GetById(id);
            return res;
        }
    }
}
