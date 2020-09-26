using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpeechProcessingNative;
using NAudio.Wave;

namespace Audio
{
    class MFCC
    {

        WaveFormat waveFormat = new WaveFormat(16000, 16, 1);
        int BufferMilliseconds = 128;
        int frame;

        

        public Int16[] Int16Convertor(byte[] array)
        {
            int size = array.Length / 2;
            Int16[] aux = new Int16[size];

            for (int i = 0; i < array.Length; i = i + 2)
            {
                aux[i / 2] = BitConverter.ToInt16(array, i);
            }

            return aux;
        }


        public double[,] Dsp(short[] array)
        {
            double[,] aux = null;
            try
            {
                object Dsp = (new DSP()).specsub(array, waveFormat.SampleRate);
                aux = (double[,])Dsp;
            }
            catch (Exception ex)
            {

            }
            return aux;
        }


        public double MedieFrame(double[,] array)
        {
            double sum = 0;
            for (int i = 0; i < array.Length; i++)
            {
                sum += Math.Abs(array[i, 0]);
            }
            return sum / array.Length;
        }

        private List<double> MFcc(byte[] aray)  // meel fr
        {


            object obj = null;
            short[] sdata = Int16Convertor(aray);
            object filteredData = (new DSP()).specsub(sdata, waveFormat.SampleRate);
            obj = (new SpeechFeature()).melcepst(filteredData, 160000, 'M', 12, 29, frame);
            double[,] nc = (double[,])obj;
            List<double> dcoeff = new List<double>();

            for (int i = 0; i <= nc.GetUpperBound(1); i++)
            {
                dcoeff.Add(nc[0, i]);
            }

            return dcoeff;
        }

        public void Ini()
        {
            byte[] b = new byte[frame * 2];
            object obj = (new SpeechFeature()).MFCC(b);
        }

        public List<List<double>> MF(string location)
        {
  
            frame = (int)(waveFormat.SampleRate * ((float)BufferMilliseconds / 1000));
            Ini();
            List<List<double>> mfcc = new List<List<double>>();
            WaveFileReader waveReader = new WaveFileReader(location);
            byte[] buffer = new byte[frame * 2];

            while ((waveReader.Length - waveReader.Position) > (frame * 2))
            {
                waveReader.Read(buffer, 0, buffer.Length);

                Int16[] aux = Int16Convertor(buffer);
                double[,] dsp = Dsp(aux);
                double medie = MedieFrame(dsp);


                if (medie > 120)
                {
                    mfcc.Add(MFcc(buffer));  
                }

            }

            waveReader.Close();
            return mfcc;
        }
    }
}
