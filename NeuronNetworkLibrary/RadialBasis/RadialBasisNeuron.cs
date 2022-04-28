﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworkLibrary
{
    public class RadialBasisNeuron
    {
        private double[] _omegas;
        public RadialBasisNeuron(double[] omegas)
        {
            if (omegas == null) throw new ArgumentNullException("omegas");
            _omegas = omegas;
        }

        public double Calc(double x, double S)
        {
            var sum = Math.Sqrt(Math.Pow(x - _omegas[0], 2));
            var g = 0.8326 / S;
            return Math.Exp(-1 * Math.Pow(sum / g, 2));
        }
    }
}