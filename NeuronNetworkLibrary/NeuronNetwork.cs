using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworkProject
{
    public abstract class NeuronNetwork
    {
        private Neuron[] _neurons;
        private int[][] _outgoingNeuronsLinks;
        private int[][] _incomingNeuronsLinks;

        protected NeuronNetwork(Neuron[] neurons, int[][] incomingNeuronsLinks, int[][] outgoingNeuronsLinks)
        {
            _neurons = neurons;
            _incomingNeuronsLinks = incomingNeuronsLinks;
            _outgoingNeuronsLinks = outgoingNeuronsLinks;
        }

        public Neuron[] Neurons { get  { return _neurons;  } }

        private Neuron[] GetLinkedNeurons(Neuron neuron, int[][] neuronsLinks)
        {
            int neuronId = Array.FindIndex(_neurons, n => n.Equals(neuron));
            return GetLinkedNeurons(neuronId, neuronsLinks);
        }

        private Neuron[] GetLinkedNeurons(int neuronId, int[][] neuronsLinks)
        {
            return neuronsLinks[neuronId].Select(id => _neurons[id]).ToArray();
        }

        public Neuron[] GetOutgoingLinkedNeurons(Neuron neuron)
        {
            return GetLinkedNeurons(neuron, _outgoingNeuronsLinks);
        }

        public Neuron[] GetIncomingLinkedNeurons(Neuron neuron)
        {
            return GetLinkedNeurons(neuron, _incomingNeuronsLinks);
        }

    }
}
