namespace TravelRoutes.Domain.Entities;

public class Graph
{
    private readonly Dictionary<string, List<Route>> _routes;

    public Graph()
    {
        _routes = new Dictionary<string, List<Route>>();
    }

    public void AddRoute(Route route)
    {
        if (!_routes.ContainsKey(route.Origin))
            _routes[route.Origin] = new List<Route>();

        _routes[route.Origin].Add(route);
    }

    public (List<string> path, int totalCost) FindCheapestRoute(string start, string end)
    {
        var priorityQueue = new PriorityQueue<List<string>, int>();
        priorityQueue.Enqueue(new List<string> { start }, 0);

        var visited = new Dictionary<string, int>();

        while (priorityQueue.Count > 0)
        {
            priorityQueue.TryDequeue(out var currentPath, out var currentCost);

            if (currentPath == null || currentPath.Count == 0)
                continue;

            string lastCity = currentPath.Last();

            if (lastCity == end)
                return (currentPath, currentCost);

            if (visited.ContainsKey(lastCity) && visited[lastCity] <= currentCost)
                continue;

            visited[lastCity] = currentCost;

            if (_routes.ContainsKey(lastCity))
            {
                foreach (var route in _routes[lastCity])
                {
                    if (!currentPath.Contains(route.Destination))
                    {
                        var newPath = new List<string>(currentPath) { route.Destination };
                        priorityQueue.Enqueue(newPath, currentCost + route.Cost);
                    }
                }
            }
        }

        return (new List<string>(), -1);
    }
}
