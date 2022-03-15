namespace NeuronNetworkProject
{
    public class LinearFunction : IActivationFunction
    {
        public double Calc(double input)
        {
            return input;
        }


        public double GetDelta(double y, double o)
        {
            return y - o; 
        }
    }
}
