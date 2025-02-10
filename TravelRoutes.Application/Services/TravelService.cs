using TravelRoutes.Application.Interfaces.Repositories;
using TravelRoutes.Application.Interfaces.Services;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Application.Services;

public class TravelService : ITravelService
{
    private readonly Graph _graph;
    private readonly IRouteRepository _routeRepository;

    public TravelService(IRouteRepository routeRepository)
    {
        _graph = new Graph();
        _routeRepository = routeRepository;
        LoadRoutes();
    }

    public void AddRoute(string origin, string destination, int cost)
    {
        var route = new Route(origin, destination, cost);
        _graph.AddRoute(route);
        _routeRepository.SaveRoute(route);
    }

    public (List<string> path, int totalCost) GetCheapestRoute(string start, string end)
    {
        return _graph.FindCheapestRoute(start, end);
    }

    private void LoadRoutes()
    {
        foreach (var route in _routeRepository.LoadRoutes())
        {
            _graph.AddRoute(route);
        }
    }
}
