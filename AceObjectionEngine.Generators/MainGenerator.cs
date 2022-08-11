using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AceObjectionEngine.Generators.Interfaces;
using AceObjectionEngine.Generators.Utils;

namespace AceObjectionEngine.Generators
{
    internal class MainGenerator
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to AceObjectionEngine C# Classes generator!");
            Console.WriteLine();

            IEnumerable<Type> generatorTypes = typeof(MainGenerator).Assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ISourceGeneratorAsync)) && !x.IsAbstract && !x.IsInterface);
            Console.WriteLine("Specify the type of generation:");
            Console.WriteLine("0. All");
            Console.WriteLine(generatorTypes.Select((x, y) => $"{y + 1}. {x.Name}").Aggregate((x, y) => $"{x}\n{y}"));
            int number;
            string userNumber;
            do
            {
                Console.Write("Number: ");
                userNumber = Console.ReadLine();
                Console.WriteLine();
            } while (!int.TryParse(userNumber, out number) && number - 1 > generatorTypes.Count());

            if (number > 0)
            {
                var userGeneratorType = generatorTypes.ElementAt(number - 1);
                generatorTypes = generatorTypes.Where(x => x == userGeneratorType || x.IsSubclassOf(userGeneratorType));
            }

            foreach(var generatorType in generatorTypes)
            {
                Console.WriteLine($"Generation code for \"{generatorType.Name}\"...");
                Console.WriteLine("Enter the name of the class: ");
                var className = Console.ReadLine();
                Console.WriteLine("Enter the file name: ");
                var fileName = Console.ReadLine();
                var generator = (ISourceGenerator)Activator.CreateInstance(generatorType, fileName, className);
                ConsoleSpinner spinner = new ConsoleSpinner();
                Console.Write("Generating...");

                var generationTask = generator.GenerateAsync();
                while (!generationTask.IsCompleted)
                {
                    Thread.Sleep(1000);
                    spinner.Animate();
                }

                generator.SaveAsFile();
                Console.WriteLine($"{generator.Name}.{generator.Extension} generated!");
            }
        }
    }
}
