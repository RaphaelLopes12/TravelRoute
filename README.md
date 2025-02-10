# Documentação - Rota de Viagem

## Visão Geral
O sistema **Rota de Viagem** permite registrar rotas de viagem entre cidades e encontrar a rota mais econômica entre dois pontos. Ele leva em consideração todas as opções possíveis e retorna a de menor custo, independentemente da quantidade de conexões.

O sistema **NÃO** utiliza o algoritmo de Dijkstra, atendendo ao requisito de exploração de todas as possibilidades sem priorização heurística.

---

## Arquivo de Entrada

O sistema recebe como entrada um arquivo contendo as rotas disponíveis no seguinte formato:

```
Origem,Destino,Valor
```

Exemplo:
```
GRU,BRC,10
BRC,SCL,5
GRU,CDG,75
GRU,SCL,20
GRU,ORL,56
ORL,CDG,5
SCL,ORL,20
```

O arquivo `routes.csv` é utilizado para armazenar as rotas registradas.

---

## Funcionalidades

### 1. Registro de novas rotas
Permite adicionar novas rotas de viagem ao sistema, armazenando-as no arquivo `routes.csv` para futuras consultas.

### 2. Consulta da melhor rota
Recebe uma origem e um destino e retorna a rota mais barata entre os dois pontos, considerando todas as possibilidades de conexão.

Exemplo de entrada:
```
Digite a rota: GRU-CDG
```
Saída esperada:
```
Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
```

### 3. Persistência de dados
As rotas são armazenadas no arquivo `routes.csv`, permitindo que o sistema mantenha os dados registrados para consultas futuras.

---

## Estrutura do Projeto

A aplicação segue um padrão modular, separando as responsabilidades entre diferentes camadas:

- **TravelRoutes.ConsoleApp**: Interface do usuário via linha de comando para execução do sistema.
- **TravelRoutes.Application**: Contém as regras de negócio e a lógica da aplicação.
- **TravelRoutes.Domain**: Define as entidades e interfaces do sistema.
- **TravelRoutes.Infrastructure**: Responsável pelo armazenamento e recuperação de dados.
- **TravelRoutes.Tests**: Contém os testes unitários.
- **routes.csv**: Arquivo contendo as rotas de viagem registradas.
- **TravelRoutes.sln**: Solução principal do projeto.

---

## Como Executar a Aplicação

### Requisitos
- .NET 8 instalado
- Editor de código (Visual Studio ou Visual Studio Code)

### Passos
1. Clone o repositório ou extraia os arquivos do projeto.
2. Navegue até o diretório raiz do projeto.
3. Execute o comando para restaurar dependências:
   ```bash
   dotnet restore
   ```
4. Compile o projeto:
   ```bash
   dotnet build
   ```
5. Execute a aplicação:
   ```bash
   dotnet run --project TravelRoutes.ConsoleApp
   ```

---

## Fluxo da Aplicação

A interface do sistema é baseada em console e permite as seguintes interações:

1. **Buscar Melhor Rota**: O usuário insere a origem e o destino no formato `Origem-Destino`, e o sistema retorna a rota mais barata.
2. **Adicionar Nova Rota**: Permite cadastrar novas rotas no formato `Origem,Destino,Valor`, que são persistidas no arquivo `routes.csv`.
3. **Sair**: Encerra o programa.

Exemplo de funcionamento:
```
1 - Buscar Melhor Rota
2 - Adicionar Nova Rota
3 - Sair
Escolha uma opção: 1
Digite a rota (Origem-Destino): GRU-CDG
Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
```

---

## Testes Unitários

Os testes estão localizados em `TravelRoutes.Tests`.

Para rodar os testes:
```bash
dotnet test
```

---

## Decisões de Design

- **Persistência em arquivo CSV**: Optamos por armazenar as rotas em um arquivo `routes.csv` para simplicidade e persistência sem necessidade de banco de dados.
- **Busca exaustiva**: O sistema percorre todas as combinações possíveis para encontrar a menor rota sem usar Dijkstra.
- **Modularização**: O código foi dividido em camadas para facilitar manutenção e expansão futura.

---

## Considerações Finais
Este sistema atende à proposta de buscar a rota mais econômica independente da quantidade de conexões, garantindo persistência e testes para validação de funcionalidade.

Para futuras melhorias, poderá ser integrada uma interface gráfica ou uma API para interação com outros sistemas.