namespace NeuronNetworkProject
{
    public interface IActivationFunction
    {
        double Calc(double input);
        double GetDelta(double y, double o);
    }
}