using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Audio
{
   public class Template_MFCC : ISerializable
    {
        public double number1 { get; set; }
        public double number2 { get; set; }
        public double number3 { get; set; }
        public double number4 { get; set; }
        public double number5 { get; set; }
        public double number6 { get; set; }
        public double number7 { get; set; }
        public double number8 { get; set; }
        public double number9 { get; set; }
        public double number10 { get; set; }
        public double number11 { get; set; }
        public double number12 { get; set; }

        public Template_MFCC(List<double> a)
        {
            number1 = a[0];
            number2 = a[1];
            number3 = a[2];
            number4 = a[3];
            number5 = a[4];
            number6 = a[5]; 
            number7 = a[6];
            number8 = a[7]; 
            number9 = a[8];
            number10 = a[9]; 
            number11 = a[10];
            number12 = a[11];
        }

        public Template_MFCC()
        {

        }

        public List<double> ReturnList()
        {
            List<double> aux = new List<double>();
            aux.Add(number1);
            aux.Add(number2);
            aux.Add(number3);
            aux.Add(number4);
            aux.Add(number5);
            aux.Add(number6);
            aux.Add(number7);
            aux.Add(number8);
            aux.Add(number9);
            aux.Add(number10);
            aux.Add(number11);
            aux.Add(number12);
            return aux;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
