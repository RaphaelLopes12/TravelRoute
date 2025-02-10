using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Application.Interfaces.Repositories;

public interface IRouteRepository
{
    void SaveRoute(Route route);
    List<Route> LoadRoutes();
}
