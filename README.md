# About
Welcome to the Monte Carlo Simulation tool! This project is a C# script designed to perform Monte Carlo simulations, a powerful statistical technique used to understand the impact of risk and uncertainty in prediction and forecasting models.

This tool performs 10,000 different simulations providing a possible delivery scenario based on delivery historical data.  

The historical data is expected as a list of periods and the corresponding 
number of PBIs (Product Backlog Items) delivered in each period.

The outcome of this script is the calculation of the 50th, 25th, 20th, 15th and, 10th percentile for the 10,000 simulated scenarios  Additionally, a bar chart is generated as an png file
showing how many times each scenario appeared during the simulations.

![Code_1jRTcMlyHp](https://github.com/saxero/MCSimulator/assets/403185/668c1dc2-6a77-4b9b-b08b-771d692a25b8)

![MonteCarloResults](https://github.com/saxero/MCSimulator/assets/403185/9108d49b-2c71-4f5c-9137-c93a27e42be8)


## Built With
- .net 8.0
- ScottPlot

## Getting Started
1. Install .NET Core (https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial)
2. Clone this repository
3. Open a command prompt window
4. Go to the root folder of the project (where .sln is located).
5. Modify the historic.csv file by providing the historical data of your team
6. run 'dotnet restore', it will install external packages
7. run 'dotnet run', it will launch the program

## Running the program
This software shall be launched through the Command Line.

**Syntax:**

>dotnet run [historic input file] [number of simulations] [number of periods per round]

**where**

  _[historic input file]: the local path to a CSV file that contains the delivery of historical data grouped by periods (sprints, months, quarters, ...)_
  
  _[number of simulations]: the number of simulation rounds to be performed_
  
  _[number of periods per round]_ how many periods from the historical shall be taken for each simulation round_
  
**Example:**

>dotnet run historic.csv 10000 3

## Contributing
Please see our [Contribution Guide](https://github.com/ERNI-Academy/net6-automation-testware/blob/main/CONTRIBUTING.md) to learn how to contribute.

## Code of conduct
Please see our [Code of Conduct](https://github.com/ERNI-Academy/net6-automation-testware/blob/main/CODE_OF_CONDUCT.md)

## Contact
📧 [esp-services@betterask.erni](mailto:esp-services@betterask.erni)

## Contributors
