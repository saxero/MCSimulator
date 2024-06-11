using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using ScottPlot;

namespace MonteCarloSimulation
{
    class Program
    {
        static void Main()
        {
            // Input data: past days and the number of delivered items for each day
            List<int> pastDeliveredItems = new List<int> { 45, 19, 42, 37, 26, 49, 48, 31, 52};

            int numberOfSimulations = 10000;
            int periodsPerRound = 3;
            List<int> simulationResults = new List<int>();

            Random rand = new Random();

            // Run Monte Carlo simulations
            for (int i = 0; i < numberOfSimulations; i++)
            {
                int totalDelivered = 0;
                for (int j = 0; j < periodsPerRound; j++)
                {
                    int randomIndex = rand.Next(pastDeliveredItems.Count);
                    totalDelivered += pastDeliveredItems[randomIndex];
                }
                simulationResults.Add(totalDelivered);
            }

            // Sort results to calculate percentiles
            simulationResults.Sort();
            
            // Calculate and print percentiles
         Console.WriteLine($"50th Percentile: {GetPercentile(simulationResults, 50)}");
            // Console.WriteLine($"75th Percentile: {GetPercentile(simulationResults, 75)}");
            // Console.WriteLine($"80th Percentile: {GetPercentile(simulationResults, 80)}");
            // Console.WriteLine($"90th Percentile: {GetPercentile(simulationResults, 90)}");

            Console.WriteLine($"10th Percentile: {GetPercentile(simulationResults, 10)}");
            Console.WriteLine($"15th Percentile: {GetPercentile(simulationResults, 15)}");
            Console.WriteLine($"20th Percentile: {GetPercentile(simulationResults, 20)}");
            Console.WriteLine($"25th Percentile: {GetPercentile(simulationResults, 25)}");


            // Create bar chart and save as image
            CreateBarChart(simulationResults, "MonteCarloResults.png");
        }

        static int GetPercentile(List<int> sortedResults, double percentile)
        {
            int index = (int)Math.Ceiling((percentile / 100.0) * sortedResults.Count) - 1;
            return sortedResults[index];
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
            foreach (var item in frequency.OrderBy(x => x.Key))
            {
                values.Add(item.Value);
                positions.Add(i);
                labels.Add(item.Key.ToString());
                i++;
                //Console.WriteLine($"{item.Key.ToString()}:{item.Value}");
            }

            // Create the plot
            var plt = new ScottPlot.Plot();
            
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
