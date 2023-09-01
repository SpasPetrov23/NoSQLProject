using AspNetCore.Identity.Mongo.Model;

namespace No_SQL_Project.Models;

public class User : MongoUser
{
    public string UserEmail { get; set; }
    public string Password { get; set; }
}