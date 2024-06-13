using ScottPlot;
using System.Data;

namespace MonteCarloSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfSimulations;
            int periodsPerRound;

            if (!CheckInputParameters(args, out numberOfSimulations, out periodsPerRound))
                return;
            
            List<int> pastDeliveredItems = LoadHistoric();
            if (pastDeliveredItems.Count==0)
                return;

            // Run Monte Carlo simulations
            List<int> simulationResults = RunSimulations(numberOfSimulations, periodsPerRound, pastDeliveredItems);

            // Create bar chart and save as image
            CreateBarChart(simulationResults, "MonteCarloResults.png");

            // Calculate and print percentiles
            PrintPercentiles(simulationResults);
        }

        static void PrintPercentiles(List<int> simulationResults){
            Console.WriteLine($"50th Percentile: {GetPercentile(simulationResults, 50)}");
            Console.WriteLine($"10th Percentile: {GetPercentile(simulationResults, 10)}");
            Console.WriteLine($"15th Percentile: {GetPercentile(simulationResults, 15)}");
            Console.WriteLine($"20th Percentile: {GetPercentile(simulationResults, 20)}");
            Console.WriteLine($"25th Percentile: {GetPercentile(simulationResults, 25)}");
        }

        static bool CheckInputParameters(string[] args, out int numberOfSimulations, out int periodsPerRound){
            numberOfSimulations=10000; // default value
            periodsPerRound =1; // default value
            if (args.Length!=3){
                Console.WriteLine("Wrong number of input parameters.");
                return false;
            }
            
            if (!Int32.TryParse(args[1], out numberOfSimulations)){
                Console.WriteLine("Wrong value for parameter 'Number of Simulations'");
                return false;
            }

            if (!Int32.TryParse(args[2], out periodsPerRound)){
                Console.WriteLine("Wrong value for parameter 'Number of Periods in each scenario'");
                return false;
            }
            return true;
        }

        static List<int> RunSimulations(int numberOfSimulations, int periodsPerRound, List<int> pastDeliveredItems){
            List<int> simulationResults = new List<int>();
            Random rand = new Random();

            // Run Monte Carlo simulations
            for (int i = 0; i < numberOfSimulations; i++){
                int totalDelivered = 0;
                for (int j = 0; j < periodsPerRound; j++){
                    int randomIndex = rand.Next(pastDeliveredItems.Count);
                    totalDelivered += pastDeliveredItems[randomIndex];
                }
                simulationResults.Add(totalDelivered);
            }
            
            // Sort results --> handy to calculate percentiles
            simulationResults.Sort();
            return simulationResults;
        }

        static int GetPercentile(List<int> sortedResults, double percentile)
        {
            int index = (int)Math.Ceiling((percentile / 100.0) * sortedResults.Count) - 1;
            return sortedResults[index];
        }

        private static List<int> LoadHistoric(){
            
            // Read historical data
            List<int> pastDeliveredItems = new List<int>();
            
            try{
                StreamReader reader = new StreamReader(@".\historic.csv");
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line?.Split(',');
                    var isNumeric = Int32.TryParse(values?[1], out int deliveredItems);
                    
                    if (isNumeric) {
                        pastDeliveredItems.Add(deliveredItems);
                        Console.WriteLine(deliveredItems);
                    }
                }
            }
            catch (Exception){
                Console.WriteLine("Error opening input file.");
            }
            return pastDeliveredItems;
        }
        static void CreateBarChart(List<int> results, string filePath)
        {
            // Count frequency of each result
            var frequency = results.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

            // Prepare the data for the chart
            var values = new List<double>();
            var positions = new List<double>();
            var labels = new List<string>();

            int i = 0;
            foreach (var item in frequency.OrderBy(x => x.Key)){
                values.Add(item.Value);
                positions.Add(i);
                labels.Add(item.Key.ToString());
                i++;
            }

            // Create the plot
            var plt = new Plot();
            plt.Resize(3000, 2000);

            // Add bar chart
            plt.AddBar(values.ToArray(), positions.ToArray());

            // Set the labels
            plt.XTicks(positions.ToArray(), labels.ToArray());
            plt.XAxis.TickLabelStyle(rotation: 45);
            plt.XLabel("Total Delivered Items");
            plt.YLabel("Frequency");
            plt.Title("Monte Carlo Simulation Results");
            plt.XAxis.TickLabelStyle(fontSize: 20);

            // Save the plot as a PNG file
            plt.SaveFig(filePath);

            Console.WriteLine($"Chart saved to {filePath}");
        }
    }
}
