using System;
using System.Linq;

namespace DeveloperCourse.FirstTask
{
    public static class Program
    {
        public static void Main()
        {
            var values = Enumerable.Range(0, 10).ToArray();

            var collection = new StructCollection<int>(values);

            Console.WriteLine($"{"Values",-42} :  {collection}");
            Console.WriteLine($"{"Length",-42} : {collection.Length,3}");
            Console.WriteLine($"{"Value on index 0",-42} : {collection[0],2}");
            Console.Write($"{"Get items in foreach",-42} :");

            foreach (var item in collection)
            {
                Console.Write($"{item,3}");
            }

            collection[0] = -1;

            Console.WriteLine(Environment.NewLine + "Change value on index 0 to -1");

            Console.Write($"{"Get items in foreach",-42} :");

            foreach (var item in collection)
            {
                Console.Write($"{item,3}");
            }

            collection[4, 5, 6] = new[] {-4, -5, -6};

            Console.WriteLine(Environment.NewLine + "Change values on indexes 4,5,6 to -4,-5,-6");

            Console.Write($"{"Get items in foreach",-42} :");

            foreach (var item in collection)
            {
                Console.Write($"{item,3}");
            }

            Console.WriteLine(Environment.NewLine + "Done.");
        }
    }
}