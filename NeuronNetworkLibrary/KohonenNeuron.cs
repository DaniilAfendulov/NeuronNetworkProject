using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NeuronNetworkLibrary
{
    public class KohonenNeuron
    {
        public KohonenNeuron(int size)
        {
            var r = new Random();
            _omega = new double[size];
            for (int i = 0; i < _omega.Length; i++)
            {
                _omega[i] = r.NextDouble() - 0.5;
            }            
        }

        private double[] _omega;
        public double[] Omega
        {
            get { return _omega; }
        }

        public double CalcScalarProduct(double[] vector)
        {
            double scalarProduct = 0;
            for (int i = 0; i < _omega.Length; i++)
            {
                scalarProduct += vector[i] * _omega[i];
            }
            return scalarProduct;
        }

        public void Train(double[] vector, double eta)
        {
            for (int i = 0; i < _omega.Length; i++)
            {
                _omega[i] = eta * (vector[i] - _omega[i]);
            }
            var len = Math.Sqrt(_omega.Select(o => o * o).Sum());
            for (int i = 0; i < _omega.Length; i++)
            {
                _omega[i] /= len;
            }
        }
    }
}
