using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class Neuron
    {
        List<double> Weights = new List<double>();
        List<double> Inputs;
        List<double> Old_Weights = new List<double>();
        double Out;
        double b;

        public Neuron(int weights_nr)
        {
            for (int i = 0; i < weights_nr; i++)
            {
                Weights.Add(RND.NextRandomRange(0, 1)); // randome
            }

        }

        public void getInputs(List<double> list)
        {
            Inputs = new List<double>(list);
            Out = sigmoid(Net());
        }

        public void getWeights(List<double> list)
        {
            Weights = new List<double>(list);
        }
     

        public List<double> Return_Weights()
        {
            return Weights;
        }

        public double Return_Weight(int index)
        {
            return Weights[index];
        }

        public double Return_OldWeight(int index)
        {
            return Old_Weights[index];
        }

        public double Net()
        {
            double sum = 0;
            for (int i = 0; i < Weights.Count; i++)
            {
                sum += Inputs[i] * Weights[i];
            }
            return sum;
        }

        private double sigmoid(double x)
        {
            double e = Math.E;
            double po = Math.Pow(e, -x);
            double aux = 1 + po;
            
            double aux1 = Math.Round(1 / aux, 8);
            if (aux1 < 0.00001) {
                return 0.00001;
            }
            else
            {
                return aux1;
            } 
        }

        public double Output()
        {
            return Out;
        }

        public void Modificare_Weights(List<double> nou)
        {
            Old_Weights = new List<double>(Weights);
            Weights = new List<double>(nou);
        }


    }
}
