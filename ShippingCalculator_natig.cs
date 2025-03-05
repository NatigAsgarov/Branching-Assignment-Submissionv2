// Package Express Shipping Calculator
// Author: Amanda White
// Version: 1.2.0
using System;
using System.Collections.Generic;

namespace ShippingExpress.Commands
{
    // Command interface
    public interface ICommand
    {
        bool Execute();
    }

    // Package data class
    public class PackageData
    {
        public double Weight { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }

    // Get weight command
    public class GetWeightCommand : ICommand
    {
        private readonly PackageData _package;

        public GetWeightCommand(PackageData package)
        {
            _package = package;
        }

        public bool Execute()
        {
            Console.WriteLine("Please enter the package weight:");
            if (!double.TryParse(Console.ReadLine(), out double weight))
            {
                Console.WriteLine("Invalid weight input.");
                return false;
            }

            if (weight > 50)
            {
                Console.WriteLine("Package too heavy to be shipped via Package Express. Have a good day.");
                return false;
            }

            _package.Weight = weight;
            return true;
        }
    }

    // Get dimensions command
    public class GetDimensionsCommand : ICommand
    {
        private readonly PackageData _package;

        public GetDimensionsCommand(PackageData package)
        {
            _package = package;
        }

        public bool Execute()
        {
            Console.WriteLine("Please enter the package width:");
            if (!double.TryParse(Console.ReadLine(), out double width))
            {
                Console.WriteLine("Invalid width input.");
                return false;
            }

            Console.WriteLine("Please enter the package height:");
            if (!double.TryParse(Console.ReadLine(), out double height))
            {
                Console.WriteLine("Invalid height input.");
                return false;
            }

            Console.WriteLine("Please enter the package length:");
            if (!double.TryParse(Console.ReadLine(), out double length))
            {
                Console.WriteLine("Invalid length input.");
                return false;
            }

            double totalSize = width + height + length;
            if (totalSize > 50)
            {
                Console.WriteLine("Package too big to be shipped via Package Express.");
                return false;
            }

            _package.Width = width;
            _package.Height = height;
            _package.Length = length;
            return true;
        }
    }

    // Calculate shipping command
    public class CalculateShippingCommand : ICommand
    {
        private readonly PackageData _package;

        public CalculateShippingCommand(PackageData package)
        {
            _package = package;
        }

        public bool Execute()
        {
            double shippingCost = (_package.Width * _package.Height * _package.Length * _package.Weight) / 100;
            Console.WriteLine($"Your estimated total for shipping this package is: ${shippingCost:F2}");
            Console.WriteLine("Thank you!");
            return true;
        }
    }

    // Command invoker
    public class ShippingCommandInvoker
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void ProcessCommands()
        {
            foreach (var command in _commands)
            {
                if (!command.Execute())
                    break;
            }
        }
    }

    // Main program
    class Program
    {
        static void Main(string[] args)
        {
            // Display welcome message
            Console.WriteLine("Welcome to Package Express. Please follow the instructions below.");

            try
            {
                var package = new PackageData();
                var invoker = new ShippingCommandInvoker();

                // Add commands to the invoker
                invoker.AddCommand(new GetWeightCommand(package));
                invoker.AddCommand(new GetDimensionsCommand(package));
                invoker.AddCommand(new CalculateShippingCommand(package));

                // Process all commands
                invoker.ProcessCommands();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
} 