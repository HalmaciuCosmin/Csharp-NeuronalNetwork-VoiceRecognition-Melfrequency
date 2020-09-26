using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
    class FFT
    {

        public static List<double> GetData(double[] array)
        {
           
            // imparte array in multimi de cate 32 si apoi le combina pe toate intr-o singura lista;

            int bits = 32;
            int size = array.Length;
            int pow = size / bits;
            List<List<double>> listadeliste = new List<List<double>>();
            List<double> final = new List<double>();
           
            

            for (int i = 0; i < pow; i++)
            {
                System.Numerics.Complex[] AuxFFT = new System.Numerics.Complex[bits];
                for (int j = 0; j < bits; j++)
                {
                    AuxFFT[j] = array[j + (bits * i)];
                }
                Accord.Math.FourierTransform.FFT(AuxFFT, Accord.Math.FourierTransform.Direction.Forward);
                double[] aux = new double[bits];
                
                for(int k = 0; k< bits; k++)
                {
                    aux[k] = AuxFFT[k].Magnitude; 
                } 

                List<double> l = aux.OfType<double>().ToList();
                listadeliste.Add(l);

            }

            for(int i = 0; i < listadeliste.Count; i++)
            {
                List<double> aux = listadeliste[i];
                for(int j = 0; j<aux.Count; j++)
                {
                    final.Add(aux[j]);
                }
                
            }
        
            return final;
            
        }
    }
}
