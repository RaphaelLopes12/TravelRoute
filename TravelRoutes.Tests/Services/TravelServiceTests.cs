using Moq;
using TravelRoutes.Application.Interfaces.Repositories;
using TravelRoutes.Application.Services;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Tests.Services;

public class TravelServiceTests
{
    private readonly Mock<IRouteRepository> _routeRepositoryMock;
    private readonly TravelService _travelService;

    public TravelServiceTests()
    {
        _routeRepositoryMock = new Mock<IRouteRepository>();

        // Configura para retornar uma lista vazia ao chamar LoadRoutes()
        _routeRepositoryMock.Setup(repo => repo.LoadRoutes()).Returns(new List<Route>());

        _travelService = new TravelService(_routeRepositoryMock.Object);
    }

    [Fact]
    public void AddRoute_ShouldCallSaveRoute()
    {
        // Arrange
        var route = new Route("GRU", "CDG", 50);

        // Act
        _travelService.AddRoute("GRU", "CDG", 50);

        // Assert
        _routeRepositoryMock.Verify(repo => repo.SaveRoute(It.Is<Route>(r =>
            r.Origin == "GRU" && r.Destination == "CDG" && r.Cost == 50)),
            Times.Once);
    }

    [Fact]
    public void GetCheapestRoute_ShouldReturnCorrectPath()
    {
        // Arrange
        _travelService.AddRoute("GRU", "BRC", 10);
        _travelService.AddRoute("BRC", "SCL", 5);
        _travelService.AddRoute("SCL", "ORL", 20);
        _travelService.AddRoute("ORL", "CDG", 5);
        _travelService.AddRoute("GRU", "CDG", 75);

        // Act
        var (path, cost) = _travelService.GetCheapestRoute("GRU", "CDG");

        // Assert
        Assert.Equal(40, cost);
        Assert.Equal(new List<string> { "GRU", "BRC", "SCL", "ORL", "CDG" }, path);
    }
}
