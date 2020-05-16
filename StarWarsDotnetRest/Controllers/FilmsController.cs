namespace StarWarsDotnetRest.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StarWarsApiCSharp;

    [ApiController]
    [Route("[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly ILogger<FilmsController> _logger;
        private readonly IRepository<Film> repository;

        public FilmsController(ILogger<FilmsController> logger, IRepository<Film> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public ICollection<Film> GetFilms([FromQuery]int page = 1, [FromQuery]int limit = 10)
        {
            var res = this.repository.GetEntities(page, limit);
            return res;
        }

        [HttpGet("{id}")]
        public Film Get(int id)
        {
            var res = this.repository.GetById(id);
            return res;
        }
    }
}
