using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

using SpeechProcessingNative;
using NAudio.Wave;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using Accord.Math.Distances;
using System.Runtime.ExceptionServices;

namespace Audio
{
    class ReteaNeuronala
    {

        List<double> Inputs = new List<double>();
        List<List<Neuron>> HidenLayers = new List<List<Neuron>>();
        List<Neuron> OutputLayer = new List<Neuron>();
        List<double> LastOutput = new List<double>();
        double eroare;
        double learning_rate;
        XmlSerializer xs;


        int nr_intrare;
        int nr_neuroni_pe_strat_ascuns;

        public ReteaNeuronala(int nr_intrare, int nr_straturi_ascunse, int nr_neuroni_pe_strat_ascuns, int nr_iesire, double learnigRate = 0.25)
        {

            if (learnigRate < 0) learnigRate = 0.45;

            this.nr_intrare = nr_intrare;
            this.nr_neuroni_pe_strat_ascuns = nr_neuroni_pe_strat_ascuns;

            xs = new XmlSerializer(typeof(List<double>));
            learning_rate = learnigRate;
            // first hiden layer
            List<Neuron> FirstHiden = new List<Neuron>();
            for (int i = 0; i < nr_neuroni_pe_strat_ascuns; i++)
            {
                FirstHiden.Add(new Neuron(nr_intrare));
            }
            HidenLayers.Add(FirstHiden);

            // next hiden layers

            for (int i = 1; i < nr_straturi_ascunse; i++)
            {
                List<Neuron> NextHiden = new List<Neuron>();
                for (int j = 0; j < nr_neuroni_pe_strat_ascuns; j++)
                {
                    NextHiden.Add(new Neuron(nr_neuroni_pe_strat_ascuns));
                }
                HidenLayers.Add(NextHiden);
            }

            // outputlayer

            for (int i = 0; i < nr_iesire; i++)
            {
                Neuron iesire = new Neuron(nr_neuroni_pe_strat_ascuns);
                OutputLayer.Add(iesire);
            }

        }

        public List<double> ForWordPropagation(List<double> Inputs, List<double> target)
        {
            this.Inputs = Inputs;
            // first layer
            List<double> outputs_FirstHidenLayer = new List<double>();
            foreach (Neuron Neu in HidenLayers[0])
            {
                Neu.getInputs(Inputs);
                outputs_FirstHidenLayer.Add(Neu.Output());
            }

            // next layers

            for (int i = 1; i < HidenLayers.Count; i++)
            {
                List<double> Outputs_NextHidenLayers = new List<double>();
                foreach (Neuron neu in HidenLayers[i])
                {
                    if (i == 1)
                    {
                        neu.getInputs(outputs_FirstHidenLayer);
                    }
                    else
                    {
                        List<double> aux_Inputs = new List<double>();
                        foreach (Neuron n in HidenLayers[i - 1])
                        {
                            aux_Inputs.Add(n.Output());
                        }
                        neu.getInputs(aux_Inputs);
                    }
                }
            }

            // outputs layer
            LastOutput.Clear();
            List<double> aux_inputs = new List<double>();
            foreach (Neuron neu in HidenLayers[HidenLayers.Count - 1])
            {
                aux_inputs.Add(neu.Output());
            }

            LastOutput.Clear();
            for (int i = 0; i < OutputLayer.Count; i++)
            {
                OutputLayer[i].getInputs(aux_inputs);
                LastOutput.Add(OutputLayer[i].Output());
            }

            eroare = Eroare(target);
            return new List<double>(LastOutput);
        }

        public double Return_Eroare()
        {
            return eroare;
        }

        public double Eroare(List<double> target)
        {
            double sum = 0;
            for (int i = 0; i < target.Count; i++)
            {
                double pow = Math.Pow(target[i] - LastOutput[i], 2);
                sum = sum + (0.5 * pow);
            }
            return sum;
        }

      
        public List<double> Return_Result()
        {
            List<double> aux = new List<double>();
            foreach (Neuron neu in OutputLayer)
            {
                aux.Add(neu.Output());
            }
            return aux;
        }

        public void BackPropagation(List<double> target) 
        {
            // output layers         
            for (int j = 0; j < OutputLayer.Count; j++)
            {
                List<double> new_weights = new List<double>();
                double A = -(target[j] - OutputLayer[j].Output());
                double B = OutputLayer[j].Output() * (1 - OutputLayer[j].Output());

                List<Neuron> List = HidenLayers[HidenLayers.Count - 1];
                for (int i = 0; i < List.Count; i++)
                {
                    double C = List[i].Output();
                    double Er_T = A * B * C;

                    new_weights.Add(OutputLayer[j].Return_Weight(i) - (learning_rate * Er_T));
                }
                OutputLayer[j].Modificare_Weights(new_weights);
            }

            // hiden layers
            for (int i = 0; i < HidenLayers[0].Count; i++)
            {
                double A = 0;
                for (int j = 0; j < OutputLayer.Count; j++)
                {
                    double output = OutputLayer[j].Output();

                    double A1 = -(target[j] - output);
                    double A2 = output * (1 - output);
                    double A3 = OutputLayer[j].Return_OldWeight(i);
                    double A4 = A1 * A2 * A3;
                    A = A + A4;
                }

                List<Neuron> n = new List<Neuron>();
                n = HidenLayers[0];

                double B = n[i].Output() * (1 - n[i].Output());
                List<double> a = new List<double>(n[i].Return_Weights());
                List<double> new_weights = new List<double>();
                for (int j = 0; j < a.Count(); j++)
                {
                    double C = Inputs[j];
                    double ER_T = A * B * C;
                    double Lr_T = ER_T * learning_rate;
                    double w = n[i].Return_Weight(j) - Lr_T;
                    new_weights.Add(w);
                }
                n[i].Modificare_Weights(new_weights);
            }
        }

        public void ImportXML(string name)
        {

            List<double> Weights = new List<double>();
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Weights\\{name}.txt";
            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            Weights = (List<double>)xs.Deserialize(f);
            f.Close();

            int count = 0;

            // primu strat ascuns;

            for (int i = 0; i < HidenLayers[0].Count; i++)
            {
                List<Neuron> aux = HidenLayers[0];
                List<double> list = new List<double>();
                for (int j = 0; j < nr_intrare; j++)
                {
                    list.Add(Weights[count]);
                    count++;
                }
                aux[i].getWeights(list);
                HidenLayers[0] = aux;
            }

            // celelate straturi ascunse;

            for (int i = 1; i < HidenLayers.Count; i++)
            {
                List<Neuron> aux = HidenLayers[i];
                for (int k = 0; k < nr_neuroni_pe_strat_ascuns; k++)
                {
                    List<double> list = new List<double>();
                    for (int j = 0; j < nr_neuroni_pe_strat_ascuns; j++)
                    {
                        list.Add(Weights[count]);
                        count++;
                    }
                    aux[k].getWeights(list);
                }
                HidenLayers[i] = aux;
            }

            // stratul output

            for (int i = 0; i < OutputLayer.Count; i++)
            {
                List<double> list = new List<double>();
                for (int j = 0; j < nr_neuroni_pe_strat_ascuns; j++)
                {
                    list.Add(Weights[count]);
                    count++;
                    //}
                }
                OutputLayer[i].getWeights(list);
            }

           }

            public void ExportXML(string name)
            {
                List<double> Weights = new List<double>();

                for (int i = 0; i < HidenLayers.Count; i++)
                {
                    List<Neuron> aux = new List<Neuron>(HidenLayers[i]);
                    for (int j = 0; j < aux.Count; j++)
                    {
                        List<double> var = aux[j].Return_Weights();
                        for (int k = 0; k < var.Count; k++)
                        {
                            Weights.Add(var[k]);
                        }
                    }
                }

                for (int i = 0; i < OutputLayer.Count; i++)
                {
                    List<double> var = new List<double>(OutputLayer[i].Return_Weights());
                    for (int j = 0; j < var.Count; j++)
                    {
                        Weights.Add(var[j]);
                    }
                }

                string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Weights\\{name}.txt";
                using (FileStream f = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    xs.Serialize(f, Weights);
                }

            }

        
    }
}
