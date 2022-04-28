using System;
using System.Linq;

namespace NeuronNetworkProject
{
    public class Neuron
    {
        private readonly IActivationFunction _activationFunction;
        private double[] _omegas;
        public double Offset;

        public IActivationFunction ActivationFunction { get { return _activationFunction; } }
        public double[] Omegas
        {
            get { return _omegas; }
        }


        public Neuron(IActivationFunction activationFunction, double[] omegas, double offset = 0)
        {
            if(activationFunction == null) throw new ArgumentNullException("activationFunction");
            _activationFunction = activationFunction;
            if(omegas == null) throw new ArgumentNullException("omegas");
            _omegas = omegas;
            Offset = offset;
        }

        public double Calc(double[] inputs)
        {
            if (inputs.Length != _omegas.Length) 
                throw new ArgumentOutOfRangeException("неверное количество входных сигналов");

            double[] changedInputs = new double[_omegas.Length];
            for (int i = 0; i < changedInputs.Length; i++)
            {
                changedInputs[i] = _omegas[i] * inputs[i];
            }

            double sum = changedInputs.Sum() + Offset;

            return _activationFunction.Calc(sum);
        }
    }
}
