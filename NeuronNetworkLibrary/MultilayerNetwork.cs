using System;
using System.Linq;

namespace NeuronNetworkProject
{
    public class MultilayerNetwork
    {
        private readonly Neuron[][] _layers;

        public MultilayerNetwork(params Neuron[][] neuronsLayers)
        {
            if (neuronsLayers == null) throw new ArgumentNullException("neuronsLayers");
            _layers = neuronsLayers;
        }

        public double[] Calc(double[] input)
        {
            return _layers.Aggregate(input, (total, next)
                => total = next.Select(n => n.Calc(total)).ToArray());
        }
    }
}
