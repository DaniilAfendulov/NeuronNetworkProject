using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworkProject
{
    public class MultilayerNetwork2 : NeuronNetwork
    {
        private readonly Neuron[][] _layers;

        private MultilayerNetwork2(
            Neuron[][] neuronsLayers,
            Neuron[] neurons, int[][] incomingNeuronsLinks, int[][] outgoingNeuronsLinks)
            : base(neurons, incomingNeuronsLinks, outgoingNeuronsLinks)
        {
            _layers = neuronsLayers;
        }


        public static MultilayerNetwork2 Create(params Neuron[][] neuronsLayers)
        {
            Neuron[] neurons = neuronsLayers.SelectMany(nl => nl).ToArray();
            int[][] incomingNeuronsLinks = new int[neurons.Length][];
            int[][] outgoingNeuronsLinks = new int[neurons.Length][];


            int[] previosNeuronsLinks = new int[neuronsLayers[0].Length];

            for (int i = 0; i < neuronsLayers[0].Length; i++)
            {
                incomingNeuronsLinks[i] = new int[0];
                previosNeuronsLinks[i] = i;
            }

            for (int i = 1; i < neuronsLayers.Length; i++)
            {
                int[] currentNeuronsLinks = new int[neuronsLayers[i].Length];
                for (int j = 0; j < neuronsLayers[i].Length; j++)
                {
                    incomingNeuronsLinks[i + j] = previosNeuronsLinks;
                    currentNeuronsLinks[i + j] = i + j;
                }
                for (int j = 0; j < previosNeuronsLinks.Length; j++)
                {

                }
                previosNeuronsLinks = currentNeuronsLinks;
            }

            return new MultilayerNetwork2(
                neuronsLayers,
                neurons, incomingNeuronsLinks, outgoingNeuronsLinks);
        }

        public double[] Calc(double[] input)
        {
            return _layers.Aggregate(input, (total, next) 
                => total = next.Select(n => n.Calc(total)).ToArray());
        }

    }
}
