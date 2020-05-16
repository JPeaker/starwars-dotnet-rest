namespace StarWarsDotnetRest.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using StarWarsApiCSharp;

    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IRepository<Vehicle> repository;

        public VehiclesController(ILogger<VehiclesController> logger, IRepository<Vehicle> repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        public ICollection<Vehicle> GetVehicles([FromQuery]int page = 1, [FromQuery]int limit = 10)
        {
            var res = this.repository.GetEntities(page, limit);
            return res;
        }

        [HttpGet("{id}")]
        public Vehicle Get(int id)
        {
            var res = this.repository.GetById(id);
            return res;
        }
    }
}
