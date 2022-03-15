namespace NeuronNetworkProject
{
    public class FullyConnectedNetwork : NeuronNetwork
    {
        private FullyConnectedNetwork(Neuron[] neurons, int[][] incomingNeuronsLinks, int[][] outgoingNeuronsLinks) 
            : base(neurons, incomingNeuronsLinks, outgoingNeuronsLinks)
        {
        }

        public static FullyConnectedNetwork Create(Neuron[] neurons)
        {
            int[][] neuronsLinks = new int[neurons.Length][];
            for (int i = 0; i < neurons.Length; i++)
            {
                neuronsLinks[i] = new int[neurons.Length];
                for (int j = 0; j < neurons.Length; j++)
                {
                    neuronsLinks[i][j] = j;
                }
            }
            return new FullyConnectedNetwork(neurons, neuronsLinks, neuronsLinks);
        }

    }
}
