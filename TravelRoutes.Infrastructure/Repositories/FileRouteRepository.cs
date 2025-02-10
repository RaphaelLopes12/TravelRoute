using TravelRoutes.Application.Interfaces.Repositories;
using TravelRoutes.Domain.Entities;

namespace TravelRoutes.Infrastructure.Repositories;

public class FileRouteRepository : IRouteRepository
{
    private const string FilePath = "routes.csv";
    private const int MaxRetries = 5;
    private const int RetryDelay = 500;

    public FileRouteRepository()
    {
        // Garante que o arquivo exista antes da primeira leitura
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Arquivo de rotas não encontrado. Criando um novo...");
            File.WriteAllText(FilePath, "GRU,BRC,10\nBRC,SCL,5\nGRU,CDG,75\nGRU,SCL,20\nGRU,ORL,56\nORL,CDG,5\nSCL,ORL,20\n");
        }
    }

    public List<Route> LoadRoutes()
    {
        var routes = new List<Route>();

        if (!File.Exists(FilePath)) return routes;

        try
        {
            using (var reader = new StreamReader(FilePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[2], out int cost))
                    {
                        routes.Add(new Route(parts[0], parts[1], cost));
                    }
                }
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }

        return routes;
    }

    public void SaveRoute(Route route)
    {
        int attempt = 0;

        while (attempt < MaxRetries)
        {
            try
            {
                using (var writer = new StreamWriter(FilePath, true))
                {
                    writer.WriteLine($"{route.Origin},{route.Destination},{route.Cost}");
                }
                Console.WriteLine("Nova rota adicionada com sucesso!");
                return;
            }
            catch (IOException)
            {
                attempt++;
                Console.WriteLine($"Arquivo em uso. Tentando novamente ({attempt}/{MaxRetries})...");
                Thread.Sleep(RetryDelay);
            }
        }

        Console.WriteLine("Erro: Não foi possível acessar o arquivo após várias tentativas.");
    }
}
