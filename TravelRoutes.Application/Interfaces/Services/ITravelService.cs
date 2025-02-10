namespace TravelRoutes.Application.Interfaces.Services;

public interface ITravelService
{
    void AddRoute(string origin, string destination, int cost);
    (List<string> path, int totalCost) GetCheapestRoute(string start, string end);
}
