using PatrickAssFucker;
using System.Text;

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var game = new Game();
        game.Run();
    }
}

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//
//namespace RescaleValues
//{
//    class Program
//    {
//        private const int NewMax = 10000;
//        private const int NewMin =  1500;
//
//        static void Main(string[] args)
//        {
//            if (args.Length < 1)
//            {
//                Console.WriteLine("Bitte geben Sie einen Dateipfad als Argument an.");
//                return;
//            }
//
//            string path = args[0];
//            var values = File.ReadAllLines(path).Select(line => int.Parse(line)).ToList();
//
//            int oldMax = values.Max();
//            int oldMin = values.Min();
//
//            var rescaledValues = values.Select(v => Rescale(v, oldMax, oldMin)).ToList();
//
//            Console.WriteLine(string.Join(", ", rescaledValues));
//            Console.WriteLine($"{rescaledValues.Min()}, {rescaledValues.Max()}");
//
//            File.WriteAllLines($"rescaled_{path}", rescaledValues.Select(v => v.ToString()));
//        }
//
//        static int Rescale(int old_value, int old_max, int old_min)
//        {
//            int oldRange = old_max - old_min;
//            int newRange = NewMax - NewMin;
//
//            return (((old_value - old_min) * newRange) / oldRange) + NewMin;
//        }
//    }
//}
