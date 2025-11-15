using System.Data.Common;

namespace API.Tests.helpers
{   
    public class User
    {
        public string? Name { get; private set; }
        public string? Email { get; private set; }
        public string? Job { get; private set; }
        public string? Id { get; private set; }
        public User(string? name, string? email, string? job, string? id)
        {
            Name = name;
            Email = email;
            Job = job;
            Id = id;           
        }
    }
}