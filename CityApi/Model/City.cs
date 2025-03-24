namespace CityApi.Model;

public class City
{
    private static int _lastId = 0;

    public City()
    {
        Id = ++_lastId;
    }
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Province { get; set; }
}