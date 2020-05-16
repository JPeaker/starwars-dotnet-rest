namespace StarWarsDotnetRest.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using StarWarsApiCSharp;
    using StarWarsDotnetRest.Models;

    public class RelationshipHandler : IRelationshipHandler
    {
        private readonly Dictionary<int, Relationship> RelationshipMap = new Dictionary<int, Relationship>();

        public RelationshipHandler(Repository<Person> repository)
        {
            using var file = File.OpenText("./Data/relationships.json");
            using var reader = new JsonTextReader(file);
            var jsonArray = (JArray)JToken.ReadFrom(reader);

            foreach (JObject relationship in jsonArray)
            {
                var id = relationship["id"];
                var person1 = relationship["person1"];
                var person2 = relationship["person2"];

                if (id == null || person1 == null || person2 == null)
                {
                    Console.WriteLine("Invalid relationship", relationship);
                }
                else if (repository.GetById((int)person1) == null || repository.GetById((int)person2) == null)
                {
                    Console.WriteLine("Person doesn't exist");
                }
                else
                {
                    var newRelationship = new Relationship()
                    {
                        Id = (int)id,
                        Person1 = repository.GetById((int)person1).Url,
                        Person2 = repository.GetById((int)person2).Url,
                    };
                    this.RelationshipMap.Add((int)id, newRelationship);
                }
            }
        }

        public ICollection<Relationship> GetRelationships() => this.RelationshipMap.Select(x => x.Value).ToList();

        public Relationship? GetRelationshipById(int id)
        {
            if (this.RelationshipMap.ContainsKey(id))
            {
                return this.RelationshipMap[id];
            }

            return null;
        }

        public Relationship CreateRelationship(Person p1, Person p2)
        {
            var relationshipExists = this.RelationshipMap.Where(idRelationship =>
                idRelationship.Value.Person1 == p1.Url &&
                idRelationship.Value.Person2 == p2.Url).FirstOrDefault().Value;

            if (relationshipExists != null)
            {
                return relationshipExists;
            }

            var maxIdRelationship = this.RelationshipMap.Aggregate((r1, r2) => r1.Key > r2.Key ? r1 : r2);
            var newRelationship = new Relationship()
            {
                Id = maxIdRelationship.Key + 1,
                Person1 = p1.Url,
                Person2 = p2.Url
            };

            this.RelationshipMap.Add((int)newRelationship.Id, newRelationship);

            return newRelationship;
        }

        public bool DeleteRelationship(int id)
        {
            if (this.RelationshipMap.ContainsKey(id))
            {
                this.RelationshipMap.Remove(id);
                return true;
            }

            return false;
        }
    }
}
