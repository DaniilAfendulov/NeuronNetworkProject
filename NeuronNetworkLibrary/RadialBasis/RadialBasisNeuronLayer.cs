using NeuronNetworkProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworkLibrary
{
    public class RadialBasisNeuronLayer
    {
        private readonly RadialBasisNeuron[] _neurons;
        private readonly Neuron _headNeuron;
        private double _S = 1;

        public RadialBasisNeuronLayer(RadialBasisNeuron[] neurons)
        {
            _neurons = neurons;
            Random random = new Random();
            _headNeuron = new Neuron(new LinearFunction(), neurons.Select(n => random.NextDouble()).ToArray());
        }

        public double Calc(double input)
        {
            double[] y = _neurons.Select(n => n.Calc(input, _S)).ToArray();
            return _headNeuron.Calc(y);
        }

        public double Calc(double[] input)
        {
            double[] y = _neurons.Select(n => n.Calc(input, _S)).ToArray();
            return _headNeuron.Calc(y);
        }

        public void Training(Tuple<double, double>[] data, int eraAmount, double eta = 0.1)
        {
            _S = data.Select(s => s.Item1).Sum() / data.Length;
            _S /= 10; // нужно подбирать
            Tuple<double[], double>[] datas = data
                .Select(d => new Tuple<double[], double>(
                    _neurons.Select(n => n.Calc(d.Item1, _S)).ToArray(),
                    d.Item2))
                .ToArray();
            _headNeuron.Training(datas, eraAmount, eta);
        }

        public void Training(Tuple<double[], double>[] data, int eraAmount, double eta = 0.1)
        {
            _S = data.Select(s => s.Item1.Sum()).Sum() / data.Length;
            _S *= 10; // нужно подбирать
            Tuple<double[], double>[] datas = data
                .Select(d => new Tuple<double[], double>(
                    _neurons.Select(n => n.Calc(d.Item1, _S)).ToArray(),
                    d.Item2))
                .ToArray();
            _headNeuron.Training(datas, eraAmount, eta);
        }
    }
}
