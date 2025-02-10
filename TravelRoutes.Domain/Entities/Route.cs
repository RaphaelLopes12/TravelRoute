namespace TravelRoutes.Domain.Entities;

public class Route
{
    public string Origin { get; }
    public string Destination { get; }
    public int Cost { get; }

    public Route(string origin, string destination, int cost)
    {
        Origin = origin;
        Destination = destination;
        Cost = cost;
    }
}
