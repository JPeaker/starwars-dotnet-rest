namespace StarWarsDotnetRest.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Linq;
    using StarWarsApiCSharp;
    using StarWarsDotnetRest.Models;
    using StarWarsDotnetRest.Services;

    [ApiController]
    [Route("[controller]")]
    public class RelationshipsController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IRepository<Person> peopleRepository;
        private readonly IRelationshipHandler relationshipHandler;

        public RelationshipsController(
            ILogger<VehiclesController> logger,
            IRepository<Person> peopleRepository,
            IRelationshipHandler relationshipHandler)
        {
            _logger = logger;
            this.relationshipHandler = relationshipHandler;
            this.peopleRepository = peopleRepository;
        }

        [HttpGet]
        public ICollection<Relationship> GetRelationships([FromQuery]int page = 1, [FromQuery]int limit = 10) =>
            this.relationshipHandler.GetRelationships();

        [HttpGet("{id}")]
        public Relationship? Get(int id) => this.relationshipHandler.GetRelationshipById(id);

        [HttpPost]
        public Relationship? CreateRelationship([FromBody]JObject body)
        {
            var person1 = body["person1"];
            var person2 = body["person2"];

            if (person1 == null || person2 == null)
            {
                throw new ArgumentException("AgH");
            }

            var p1 = this.peopleRepository.GetById((int)person1);
            var p2 = this.peopleRepository.GetById((int)person2);

            if (p1 == null || p2 == null)
            {
                throw new ArgumentException("person1 or person2 does't exist");
            }

            return this.relationshipHandler.CreateRelationship(p1, p2);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteRelationship(int id) => this.relationshipHandler.DeleteRelationship(id);
    }
}
