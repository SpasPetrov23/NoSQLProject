using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace No_SQL_Project.Models;

public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int Runtime { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Rating { get; set; }
    public List<string> Cast { get; set; } = new ();
    public List<string> Directors { get; set; } = new ();
    public List<string> Genres { get; set; } = new ();
}