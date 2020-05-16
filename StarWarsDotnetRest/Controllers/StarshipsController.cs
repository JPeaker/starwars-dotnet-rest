namespace StarWarsDotnetRest.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StarWarsApiCSharp;

    [ApiController]
    [Route("[controller]")]
    public class StarshipsController : ControllerBase
    {
        private readonly ILogger<StarshipsController> _logger;
        private readonly IRepository<Starship> repository;

        public StarshipsController(ILogger<StarshipsController> logger, IRepository<Starship> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public ICollection<Starship> GetStarships([FromQuery]int page = 1, [FromQuery]int limit = 10)
        {
            var res = this.repository.GetEntities(page, limit);
            return res;
        }

        [HttpGet("{id}")]
        public Starship Get(int id)
        {
            var res = this.repository.GetById(id);
            return res;
        }
    }
}
