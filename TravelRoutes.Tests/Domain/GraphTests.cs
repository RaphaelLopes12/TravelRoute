using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Tests.Domain;

public class GraphTests
{
    [Fact]
    public void AddRoute_ShouldStoreRoutesCorrectly()
    {
        // Arrange
        var graph = new Graph();
        var route = new Route("GRU", "BRC", 10);

        // Act
        graph.AddRoute(route);

        // Assert
        var (path, cost) = graph.FindCheapestRoute("GRU", "BRC");
        Assert.Equal(10, cost);
        Assert.Equal(new List<string> { "GRU", "BRC" }, path);
    }

    [Fact]
    public void FindCheapestRoute_ShouldReturnCorrectPathAndCost()
    {
        // Arrange
        var graph = new Graph();
        graph.AddRoute(new Route("GRU", "BRC", 10));
        graph.AddRoute(new Route("BRC", "SCL", 5));
        graph.AddRoute(new Route("SCL", "ORL", 20));
        graph.AddRoute(new Route("ORL", "CDG", 5));
        graph.AddRoute(new Route("GRU", "CDG", 75));

        // Act
        var (path, cost) = graph.FindCheapestRoute("GRU", "CDG");

        // Assert
        Assert.Equal(40, cost);
        Assert.Equal(new List<string> { "GRU", "BRC", "SCL", "ORL", "CDG" }, path);
    }

    [Fact]
    public void FindCheapestRoute_NoRouteAvailable_ShouldReturnMinusOne()
    {
        // Arrange
        var graph = new Graph();
        graph.AddRoute(new Route("GRU", "BRC", 10));

        // Act
        var (path, cost) = graph.FindCheapestRoute("GRU", "CDG");

        // Assert
        Assert.Empty(path);
        Assert.Equal(-1, cost);
    }

    [Fact]
    public void FindCheapestRoute_SameOriginAndDestination_ShouldReturnZeroCost()
    {
        // Arrange
        var graph = new Graph();
        graph.AddRoute(new Route("GRU", "BRC", 10));

        // Act
        var (path, cost) = graph.FindCheapestRoute("GRU", "GRU");

        // Assert
        Assert.Equal(0, cost);
        Assert.Equal(new List<string> { "GRU" }, path);
    }
}
