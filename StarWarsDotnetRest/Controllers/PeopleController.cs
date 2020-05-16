namespace StarWarsDotnetRest.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StarWarsApiCSharp;

    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly ILogger<PeopleController> _logger;
        private readonly IRepository<Person> repository;

        public PeopleController(ILogger<PeopleController> logger, IRepository<Person> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public ICollection<Person> GetPeople([FromQuery]int page = 1, [FromQuery]int limit = 10)
        {
            var res = this.repository.GetEntities(page, limit);
            return res;
        }

        [HttpGet("{id}")]
        public Person Get(int id)
        {
            var res = this.repository.GetById(id);
            return res;
        }
    }
}
