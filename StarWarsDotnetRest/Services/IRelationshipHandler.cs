namespace StarWarsDotnetRest.Services
{
    using System.Collections.Generic;
    using StarWarsApiCSharp;
    using StarWarsDotnetRest.Models;

    public interface IRelationshipHandler
    {
        ICollection<Relationship> GetRelationships();

        Relationship? GetRelationshipById(int id);

        Relationship CreateRelationship(Person p1, Person p2);

        bool DeleteRelationship(int id);
    }
}
