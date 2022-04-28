using NeuronNetworkLibrary;
using NeuronNetworkProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AIShower
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RadialBasisTest();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Draw(string name, List<Tuple<double, double>> points, int width, SeriesChartType chartType = SeriesChartType.Line)
        {
            if (chart1.Series.FindByName(name) != null)
                chart1.Series[name].Points.Clear();
            else
                chart1.Series.Add(name);
            var series = chart1.Series[name];
            series.ToolTip = "X = #VALX, Y = #VALY, #SERIESNAME";
            series.ChartType = chartType;
            series.BorderWidth = width;

            foreach (Tuple<double, double> point in points)
                series.Points.AddXY(point.Item1, point.Item2);
        }

        private void KohonenTest()
        {
            Func<int, KohonenNeuron[]> genlayer = (int length) =>
            {
                List<KohonenNeuron> tlayer = new List<KohonenNeuron>();
                for (int i = 0; i < length; i++)
                {
                    tlayer.Add(new KohonenNeuron(2));
                }
                return tlayer.ToArray();
            };

            KohonenNeuron[] layer = genlayer(4);

            Tuple<double, double>[] c = new Tuple<double, double>[]
            {
                new Tuple<double, double>(17, 12),
                new Tuple<double, double>(-13, -15),
                new Tuple<double, double>(15, -10),
                new Tuple<double, double>(-10, 10)
            };

            var r = new Random();
            Func<double, double, double> generateRandomNumber = 
                (center, k) => { return center + (r.NextDouble() - 0.5) * k; };


            var datalen = 5000;
            var data = new double[datalen][];
            for (int i = 0; i < datalen; i++)
            {
                int index = i % c.Length;
                data[i] = new double[] 
                { 
                    generateRandomNumber(c[index].Item1, 10),
                    generateRandomNumber(c[index].Item2, 10)
                };
            }

            layer.KohonenLayerTraining(data, 1000);

            var clasters = new List<List<Tuple<double, double>>>();
            foreach (var item in c)
            {
                clasters.Add(new List<Tuple<double, double>>());
            }

            foreach (var point in data)
            {
                int index = NeuronTraining.FindClosestKohonenNeuronIndex(layer, point);
                clasters[index].Add(new Tuple<double, double>(point[0], point[1]));
            }
            foreach (var point in layer.Select(n => n.Omega))
            {
                int index = NeuronTraining.FindClosestKohonenNeuronIndex(layer, point);
                clasters[index].Add(new Tuple<double, double>(point[0], point[1]));
            }

            for (int i = 0; i < clasters.Count; i++)
            {
                Draw("Кластер " + i, clasters[i], 50, SeriesChartType.Point);
            }
        }

        private void RadialBasisTest()
        {
            List<RadialBasisNeuron> neurons = new List<RadialBasisNeuron>();
            List<Tuple<double, double>> data = new List<Tuple<double, double>>();
            for (double i = 0; i < 10; i++)
            {
                data.Add(new Tuple<double, double>(i, i * 2));
                neurons.Add(new RadialBasisNeuron(new double[] { i }));
            }
            RadialBasisNeuronLayer neuronLayer = new RadialBasisNeuronLayer(neurons.ToArray());
            neuronLayer.Training(data.ToArray(), 100000, 0.05);

            List<Tuple<double, double>> points = new List<Tuple<double, double>>();
            for (double i = 0; i < 20; i += 0.05)
            {
                points.Add(new Tuple<double, double>(i, neuronLayer.Calc(i)));
            }
            Draw("RadialBasis", points, 10);
        }
    }
}
