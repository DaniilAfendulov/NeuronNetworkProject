using System;

namespace NeuronNetworkProject.ActivationFunctions
{
    public class Sigmoid : IActivationFunction
    {
        public double Calc(double input)
        {
            return 1.0 / (1 + Math.Exp(-input));
        }


        public double GetDelta(double y, double o)
        {
            return (y - o) * o * (1 - 0);
        }
    }
}
