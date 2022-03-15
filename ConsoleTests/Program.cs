using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetworkProject;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            ChangeOmegasTest();
            Console.ReadLine();
        }

        public static void ShowOmegas(Neuron neuron) 
        {
            ShowArray(neuron.Omegas);
        }

        public static void ShowArray(double[] array)
        {
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }
        public static void OneNeuronTest()
        {
            var function = new LinearFunction();
            var omegas = new double[] { 1, 2, 3 };
            var neuron = new Neuron(function, omegas, 0);
            ShowOmegas(neuron);
            var data = new List<Tuple<double[], double>>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var items = new double[] { random.Next(25), random.Next(50), random.Next(100) };
                data.Add(new Tuple<double[], double>(items, 2 * items.Sum()));
            }

            neuron.Training(data.ToArray(), 1000, 0.0000001);
            ShowOmegas(neuron);
            Console.WriteLine(neuron.Calc(new double[] { 1, 2, 3 }));
            Console.ReadLine();
        }
        public static void TwoNeuronTest()     
        {
            var function = new LinearFunction();
            var omegas = new double[] { 1, 2, 3 };
            var neuron1 = new Neuron(function, omegas, 0);
            var neuron2 = new Neuron(function, omegas, 0);
            ShowOmegas(neuron1);
            ShowOmegas(neuron1);
            var data1 = new List<Tuple<double[], double>>();
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var items = new double[] { random.Next(25), random.Next(50), random.Next(100) };
                data1.Add(new Tuple<double[], double>(items, items.Sum()));
            }

            var data2 = new List<Tuple<double[], double>>();
            for (int i = 0; i < 1000; i++)
            {
                var items = new double[] { random.Next(25), random.Next(50), random.Next(100) };
                data2.Add(new Tuple<double[], double>(items, 2 * items[1] + items[0] + items[2]));
            }

            Console.WriteLine();
            neuron1.Training(data1.ToArray(), 1000, 0.0000001);
            ShowOmegas(neuron1);
            Console.WriteLine(neuron1.Calc(new double[] { 1, 2, 3 }));

            Console.WriteLine();
            neuron2.Training(data2.ToArray(), 1000, 0.0000001);
            ShowOmegas(neuron2);
            Console.WriteLine(neuron2.Calc(new double[] { 1, 2, 3 }));
            Console.ReadLine();
        }
        public static void ChangeOmegasTest()
        {
            var function = new LinearFunction();
            var omegas = new double[] { 1, 2, 3 };
            var neuron = new Neuron(function, omegas);

            ShowOmegas(neuron);
            neuron.Omegas[0] = 10;
            ShowOmegas(neuron);
        }
    }
}
