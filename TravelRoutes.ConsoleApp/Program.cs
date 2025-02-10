using TravelRoutes.Application.Interfaces.Services;
using TravelRoutes.Application.Services;
using TravelRoutes.Infrastructure.Repositories;

class Program
{
    static void Main()
    {
        ITravelService service = new TravelService(new FileRouteRepository());

        while (true)
        {
            Console.WriteLine("\n1 - Buscar Melhor Rota");
            Console.WriteLine("2 - Adicionar Nova Rota");
            Console.WriteLine("3 - Sair");
            Console.Write("Escolha uma opção: ");

            string? option = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(option))
                continue;


            if (option == "1")
            {
                Console.Write("Digite a rota (Origem-Destino): ");
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Entrada inválida! Tente novamente.");
                    continue;
                }
                var cities = input.Split('-');

                if (cities.Length != 2)
                {
                    Console.WriteLine("Formato inválido! Use Origem-Destino.");
                    continue;
                }

                var (path, cost) = service.GetCheapestRoute(cities[0].Trim(), cities[1].Trim());

                if (cost == -1)
                    Console.WriteLine("Nenhuma rota encontrada.");
                else
                    Console.WriteLine($"Melhor Rota: {string.Join(" - ", path)} ao custo de ${cost}");
            }
            else if (option == "2")
            {
                Console.Write("Digite a nova rota (Origem,Destino,Valor): ");
                string input = Console.ReadLine();
                var parts = input.Split(',');

                if (parts.Length != 3 || !int.TryParse(parts[2], out int cost))
                {
                    Console.WriteLine("Formato inválido! Use Origem,Destino,Valor.");
                    continue;
                }

                service.AddRoute(parts[0].Trim(), parts[1].Trim(), cost);
                Console.WriteLine("Nova rota adicionada com sucesso!");
            }
            else if (option == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida! Tente novamente.");
            }
        }
    }
}
