using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworkProject
{
    public static class NeuronTraining
    {
        public static void Training(this Neuron neuron, Tuple<double[], double>[] data,
            double eraAmount,
            double eta = 0.4, double Emin = double.MinValue, double Emax = double.MaxValue)
        {
            if (neuron == null)
            {
                throw new ArgumentNullException("neuron");
            }

            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            for (int ei = 0; ei < eraAmount; ei++)
            {
                double E = 0;
                foreach (var dataItem in data)
                {
                    double[] x = dataItem.Item1;
                    double y = dataItem.Item2;
                    double o = neuron.Calc(x);
                    double Ek = 0.5 * Math.Pow(y - o, 2);
                    E += Ek;
                    double delta = neuron.ActivationFunction.GetDelta(y, o);
                    for (int i = 0; i < neuron.Omegas.Length; i++)
                    {
                        neuron.Omegas[i] = neuron.Omegas[i] + eta * delta * x[i];
                    }
                }

                if (E <= Emin || E >= Emax)
                {
                    break;
                }
            }

        }

        public static void Training(this Neuron[] neurons, Tuple<double[], double>[][] data,
            double eraAmount,
            double eta = 0.4, double Emin = double.MinValue, double Emax = double.MaxValue)
        {
            for (int i = 0; i < neurons.Length; i++)
            {
                neurons[i].Training(data[i], eraAmount, eta, Emin, Emax);
            }
        }
    }
}
