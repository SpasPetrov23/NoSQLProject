namespace No_SQL_Project.Models;

public class MoviesDatabaseSettings
{
    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }

    public string MoviesCollectionName { get; set; }

    public string UsersCollectionName { get; set; }
}