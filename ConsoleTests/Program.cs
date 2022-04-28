using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuronNetworkProject;
using NeuronNetworkLibrary;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            RadialBasisTest();
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

        public static void KohonenTest()
        {
            var layer = new KohonenNeuron[]
            {
                new KohonenNeuron(2),
                new KohonenNeuron(2),
                new KohonenNeuron(2),
                new KohonenNeuron(2)
            };

            Tuple<double, double>[] c = new Tuple<double, double>[]
            {
                new Tuple<double, double>(100, 100),
                new Tuple<double, double>(-100, -100),
                new Tuple<double, double>(100, -100),
                new Tuple<double, double>(-100, 100)
            };

            var r = new Random();
            var datalen = 1000;
            var data = new double[datalen][];
            for (int i = 0; i < datalen; i++)
            {
                int index = r.Next(c.Length);
                data[i] = new double[] { c[index].Item1 + r.NextDouble() - 0.5, c[index].Item2 + r.NextDouble() - 0.5 };
            }

            layer.KohonenLayerTraining(data, 1000);



        }

        public static void RadialBasisTest()
        {
            List<RadialBasisNeuron> neurons = new List<RadialBasisNeuron>();            
            List<Tuple<double, double>> data = new List<Tuple<double, double>>();
            for (double i = 0; i < 10; i++)
            {
                data.Add(new Tuple<double, double>(i, i * 2));
                neurons.Add(new RadialBasisNeuron(new double[] { i }));
            }
            RadialBasisNeuronLayer neuronLayer = new RadialBasisNeuronLayer(neurons.ToArray());
            neuronLayer.Training(data.ToArray(), 100000, 0.005);

            for (double i = 5; i < 10; i+=0.1)
            {
                Console.WriteLine("i=" + i + "\tr=" + neuronLayer.Calc(i));
            }

        }


    }
}
