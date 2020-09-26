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
using RecunoastereaVorbitorului;

namespace Audio

{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            Ini();
        }

        public XmlSerializer xs;
        public XmlSerializer xss;
        List<double> waveData = new List<double>();
        List<double> fftData = new List<double>();
        List<List<double>> mfccData = new List<List<double>>();
        int count_test = 0;
        int nrNeuroni = 20;
        int DeviceNumber;

        private void Ini()
        {

            List<NAudio.Wave.WaveInCapabilities> sourse = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sourse.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            if (sourse.Count == 0)
            {
                MessageBox.Show("nu ai nici un microfon conectat");
                DeviceNumber = -1;
            }
            else
            {
                DeviceNumber = 0;
            }
            

            Settings.Image = (Image)(new Bitmap(Settings.Image, new Size(32, 32)));
            RND.ini();
            chart1.Series.Add("Eroare");
            chart2.Series.Add("Eroare");
            chart2.Series["Eroare"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart2.Series["Eroare"].ChartArea = "ChartArea1";

            for (int n = -1; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
            }
           
            View();

            xs = new XmlSerializer(typeof(List<Template_MFCC>));
            xss = new XmlSerializer(typeof(List<List<double>>));
            Worker.WorkerSupportsCancellation = true;
            chart3.Series["Eroare"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            ClearTrainingTest();
            foreach (string item in Directory.GetFiles("Data"))
            {
                FileInfo ffi = new FileInfo(item);
                Create_TrainSet_AND_TestSet(ffi.Name);

            }

        }

        #region Deschide Fisier .wave  // copiere date in waveData

        private void OPEN_Click(object sender, EventArgs e)
        {
            if (textBox_User.Text == "") { MessageBox.Show("Username invalid"); return; }
            if (Check(textBox_User.Text)) { MessageBox.Show("Username invalid, Exista utilizator cu acest nume"); return; }

            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() != DialogResult.OK) return;

            string[] path = open.FileName.Split('.');
            string p;


            p = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Users\\{textBox_User.Text}";
            using (StreamWriter sw = File.CreateText(p))


            p = $"{path[0]}.wav";
            List<List<double>> List = new List<List<double>>(DoMFCC(p));
            List<Template_MFCC> c = new List<Template_MFCC>();
            for (int i = 0; i < List.Count; i++)
            {
                List<double> aux = new List<double>(List[i]);
                c.Add(new Template_MFCC(aux));
            }

            p = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Data\\{textBox_User.Text}.txt";
            FileStream f = new FileStream(p, FileMode.Create, FileAccess.ReadWrite);
            xs.Serialize(f, c);
            f.Close();

            p = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Waves\\{textBox_User.Text}";

            File.Copy(open.FileName, $"{p}.wav");
            View();

            ClearTrainingTest();
            foreach (string item in Directory.GetFiles("Data"))
            {
                FileInfo ffi = new FileInfo(item);
                Create_TrainSet_AND_TestSet(ffi.Name);

            }
            SelectedUser_Label.Text = textBox_User.Text;

        }
        #endregion

       

        #region  MFCC

        private List<List<double>> DoMFCC(string location)
         {
            MFCC ob = new MFCC();
            return  new List<List<double>>(ob.MF(location));
        }
        #endregion


        #region FFT

        private void FFt(string name)
        {
            name = $"{name}.wav";
            chart1.Series.RemoveAt(0);
            chart1.Series.Add("Eroare");
            chart1.Series["Eroare"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series["Eroare"].ChartArea = "ChartArea1";
            WaveChannel32 Wave32 = new WaveChannel32(new WaveFileReader(name));

            byte[] buffer = new byte[2048];
            int read = 0;

            while (Wave32.Position < Wave32.Length)
            {
                
                read = Wave32.Read(buffer, 0, 2048);
                for (int i = 0; i < read / 4; i += 30)
                {

                    chart1.Series["Eroare"].Points.Add(BitConverter.ToSingle(buffer, i * 4));
                    waveData.Add(BitConverter.ToSingle(buffer, i * 4));
                }       
            }

            Wave32.Dispose();
            fftData = FFT.GetData(waveData.ToArray());
            waveData = new List<double>();

            chart2.Series.RemoveAt(0);
            chart2.Series.Add("wave");
            chart2.Series["wave"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            for (int i = 0; i < fftData.Count - 1; i = i+=30)
            {
                chart2.Series["wave"].Points.Add(Math.Round(fftData[i], 8)); 
            }
          
        }
        #endregion
    

        #region View
     
        public void View()
        {
            listBox1.Items.Clear();
            foreach (string item in Directory.GetFiles("Users"))
            {
                FileInfo f = new FileInfo(item);
                listBox1.Items.Add(f.Name);
            }
        }

        public bool Check(string a)
        {
            foreach (string item in Directory.GetFiles("Users"))
            {        
                if (listBox1.Items.Contains(a))
                {
                    return true;
                }
            }
            return false;
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                SelectedUser_Label.Text = (string)listBox1.SelectedItems[0];
                string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Waves\\{listBox1.SelectedItems[0]}"; ;
                Verificare_Label.Text = "%";
                Testare_label.Text = "%";
                FFt(path);
            }
        }

        #endregion

        private void AddUser_Click(object sender, EventArgs e)
        {


            if (textBox_User.Text == "") { MessageBox.Show("Username invalid"); return; }
            if(Check(textBox_User.Text)) { MessageBox.Show("Username invalid, Exista utilizator cu acest nume"); return; }

            AddUserForm form = new AddUserForm(textBox_User.Text,DeviceNumber);
            form.ShowDialog();
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Waves\\{textBox_User.Text}.wav";
            if(!File.Exists(path))
            {
                MessageBox.Show("ERRORE: Ai inchis inainte de a finaliza inregistrarea");
                return;
            }

            List<NAudio.Wave.WaveInCapabilities> sourse = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sourse.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            if (!(sourse.Count >= DeviceNumber - 1))
            {
                DeviceNumber = sourse.Count() - 1;
            }

            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Users\\{textBox_User.Text}";
            using (StreamWriter sw = File.CreateText(path))

       
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Waves\\{textBox_User.Text}.wav";
            List<List<double>> List = new List<List<double>>(DoMFCC(path));
            List<Template_MFCC> c = new List<Template_MFCC>();
            for (int i = 0; i < List.Count; i++)
            {
                List<double> aux = new List<double>(List[i]);
                c.Add(new Template_MFCC(aux));
            }

            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Data\\{textBox_User.Text}.txt";
            FileStream f = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            xs.Serialize(f, c);
            f.Close();

            ClearTrainingTest();
            foreach (string item in Directory.GetFiles("Data"))
            {            
                FileInfo fi = new FileInfo(item);
                Create_TrainSet_AND_TestSet(fi.Name);
               
            }

            View();
        }

        //  Pentru utilizator selectat 80% - Train , 20% Test din Data, pentru neselectat 33% din Data,  1/4 - Test, Restul - Train.
        public void Create_TrainSet_AND_TestSet(string name)
        {

            string pathTrainSet = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TrainSet\\{name}";
            List<List<double>> TrainSet = new List<List<double>>();
            List<List<double>> TestSet = new List<List<double>>();
            int count = 0;
            int ElementeTest = 0;

            string p = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Data\\{name}";
            FileStream fff = new FileStream(p, FileMode.Open, FileAccess.Read);
            List<Template_MFCC> obb = (List<Template_MFCC>)xs.Deserialize(fff);
            for (int i = 0; i < obb.Count; i++)
            {
                List<double> obList = new List<double>(obb[i].ReturnList());
                obList.Add(1);
                if (count == 3)
                {
                    TestSet.Add(new List<double>(obList));
                    count = 0;
                    ElementeTest++;
                }
                else
                {
                    TrainSet.Add(new List<double>(obList));
                    count++;
                }
            }

            foreach (string item in Directory.GetFiles("Data")) 
            { 
           
                count = 0;

                FileInfo fi = new FileInfo(item);
                string pp = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Data\\{fi.Name}";
                FileStream f = new FileStream(pp, FileMode.Open, FileAccess.Read);
                List<Template_MFCC> ob = (List<Template_MFCC>)xs.Deserialize(f);

                if(fi.Name == $"{name}")
                {

                }
                else
                {
                    for (int i = 0; i < (int)ob.Count/5;i++)
                    {
                        List<double> obList = new List<double>(ob[i].ReturnList());
                        obList.Add(0);
                        if (count == 3)
                        {
                            if(ElementeTest  > TestSet.Count / 2) {
                                TestSet.Add(new List<double>(obList));
                            }
                            count = 0;
                        }
                        else
                        {
                            TrainSet.Add(new List<double>(obList));
                            count++;
                        }
                    }
                }
            }

            string TestPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSet\\{name}";
            string TrainPath = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TrainSet\\{name}";

            FileStream ff = new FileStream(TestPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            xss.Serialize(ff, TestSet);
            ff.Close();
            AmestecaTestSet(name);

            ff = new FileStream(TrainPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            xss.Serialize(ff, TrainSet);
            ff.Close();
            AmestecaTrainSet(name);
        }


        #region AI
        List<List<double>> Inputs;
        List<List<double>> Target;
        ReteaNeuronala rs;
        private void Learning_Click(object sender, EventArgs e)
        {
            if (Worker.IsBusy) return;

            string user = SelectedUser_Label.Text;
            if(user == "Nimeni")
            {
                return;
            }
            rs = new ReteaNeuronala(12, 1, nrNeuroni, 1);
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Weights\\{user}.txt";
            bool a = File.Exists(path);
            if (File.Exists(path))
            {
                rs.ImportXML(user);
            }
            else
            {
                rs.ExportXML(user);
            }

             Inputs = new List<List<double>>();
             Target = new List<List<double>>();


            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TrainSet\\{user}.txt";
            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<List<double>> ob = (List<List<double>>)xss.Deserialize(f);

            for(int i = 0; i < ob.Count; i++)
            {
                List<double> aux = new List<double>(ob[i]);
                aux.RemoveAt(aux.Count - 1);
                Inputs.Add(new List<double>(aux));

                aux = new List<double>(ob[i]);
                aux.RemoveRange(0, 12);
                Target.Add(new List<double>(aux));
            }


            if (!Worker.IsBusy)
            {
                chart3.Series.RemoveAt(0);
                chart3.Series.Add("Eroare");
                chart3.Series["Eroare"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                Worker.RunWorkerAsync();
            }
            
        }


        public void AmestecaInputsTargets()
        {
            for (int i = 0; i < Inputs.Count; i++)
            {
                List<double> temp = Inputs[i];
                int randomIndex = RND.Next(Inputs.Count);
                Inputs[i] = Inputs[randomIndex];
                Inputs[randomIndex] = temp;

                temp = Target[i];
                Target[i] = Target[randomIndex];
                Target[randomIndex] = temp;
            }
        }

        private void Work(object sender, DoWorkEventArgs e)
        {
  
            int nr_generatii = (int)NumericUpDown.Value;
            double eroare_dorita = (double)NumericUpDown_Eroare.Value;
            for (int i = 0; i < nr_generatii; i++)
            {
                Thread.Sleep(2);

                if (Worker.CancellationPending)
                {
                    e.Cancel = true;

                    return;
                }
                List<double> Rez = new List<double>();
                List<double> Targ = new List<double>();
                for (int j = 0; j < Inputs.Count(); j++)
                {
                    rs.ForWordPropagation(Inputs[j], Target[j]);
                    rs.BackPropagation(Target[j]);
                    List<double> tg_aux = new List<double>(Target[j]);
                    List<double> rz_aux = new List<double>(rs.Return_Result());
                    Rez.Add(rz_aux[0]);
                    Targ.Add(tg_aux[0]);
                }
                if(i%25 == 0)
                {
                     AmestecaInputsTargets();
                }
                BeginInvoke((MethodInvoker)delegate
                {
                    GeneratieValue_label.Text = i.ToString();
                    EroareValue_Label.Text = $"{Eroare(Rez, Targ).ToString()}";
                    chart3.Series["Eroare"].Points.AddXY(i, Eroare(Rez,Targ));
                    if(Eroare(Rez,Targ) < eroare_dorita)
                    {
                        Stop.PerformClick();
                    }
                });  
               
            }
            BeginInvoke((MethodInvoker)delegate
            {
                rs.ExportXML(SelectedUser_Label.Text);
            });

        }


        public double Eroare(List<double> rez, List<double> tg)
        {
            double sum = 0;
            for(int i =0; i< rez.Count;i++)
            {
                sum += ((Math.Pow(tg[i] - rez[i], 2)) * 1/2); 
            }

            return sum;
        }

    

        private void Stop_Click(object sender, EventArgs e)
        {
            if (Worker.IsBusy)
            {
                Worker.CancelAsync();
                rs.ExportXML(SelectedUser_Label.Text);
            }
        }


        public void AmestecaTrainSet(string user)
        {
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TrainSet\\{user}";

            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<List<double>> ob = (List<List<double>>)xss.Deserialize(f);
            f.Close();

            for (int i = 0; i < ob.Count; i++)
            {
                List<double> temp = ob[i];
                int randomIndex = RND.Next(ob.Count);
                ob[i] = ob[randomIndex];
                ob[randomIndex] = temp;
            }


            f = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            xss.Serialize(f, ob);
            f.Close();
        }

        public void AmestecaTestSet(string user)
        {

            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSet\\{user}";

            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<List<double>> ob = (List<List<double>>)xss.Deserialize(f);
            f.Close();

            for (int i = 0; i < ob.Count; i++)
            {
                List<double> temp = ob[i];
                int randomIndex = RND.Next(ob.Count);
                ob[i] = ob[randomIndex];
                ob[randomIndex] = temp;
            }


            f = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
            xss.Serialize(f, ob);
            f.Close();
        }


        private double Medie(List<double> list)
        {
            double sum = 0;
            for(int i = 0; i < list.Count; i++)
            {
                sum = sum + list[i];
            }
            return sum / list.Count;
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

       #endregion
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string item in Directory.GetFiles("TestSample"))
            {
                File.Delete(item);
            }
        }

        private void StergeUtilizator_Click(object sender, EventArgs e)
        {
            if (SelectedUser_Label.Text == "Nimeni")
            {
                MessageBox.Show("Selecteaza un utilizator");
                return;
            }

            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Weights\\{SelectedUser_Label.Text}.txt";
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Data\\{SelectedUser_Label.Text}.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Waves\\{SelectedUser_Label.Text}.wav";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Users\\{SelectedUser_Label.Text}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TrainSet\\{SelectedUser_Label.Text}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSet\\{SelectedUser_Label.Text}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }



            View();
        }

        public void ClearTrainingTest()
        {
            foreach (string item in Directory.GetFiles("TrainSet"))
            {
                FileInfo fi = new FileInfo(item);
                string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TrainSet\\{fi.Name}";
                File.Delete(path);
            }
            foreach (string item in Directory.GetFiles("TestSet"))
            {
                FileInfo fi = new FileInfo(item);
                string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSet\\{fi.Name}";
                File.Delete(path);
            }
        }

        double Eroarre(List<double> rez, List<double> targets)
        {
            List<double> aux = new List<double>();
            for(int i = 0; i < rez.Count; i++)
            {
                aux.Add(Math.Abs(targets[i] - rez[i]));
            }
            return Math.Round(100 - Medie(aux) * 100,2);
        }

        private void Verificare_Click(object sender, EventArgs e)
        {
            if (SelectedUser_Label.Text == "Nimeni") {
                MessageBox.Show("Selecteaza un utilizator");
                return;
            }


            List<NAudio.Wave.WaveInCapabilities> sourse = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sourse.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            if (!(sourse.Count >= DeviceNumber -1))
            {
                DeviceNumber = sourse.Count() - 1;
            }



            string p = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSample\\ {SelectedUser_Label.Text}.wav";
            VerificareForm form = new VerificareForm(p,DeviceNumber);
          
            form.ShowDialog();
            if (!File.Exists(p))
            {
                MessageBox.Show("Eroare, inregistrarea nu a fost efectuata cu succes");
                return;
            }
            List<List<double>> aux = new List<List<double>>(DoMFCC(p));

            rs = new ReteaNeuronala(12, 1, nrNeuroni, 1);
            rs.ImportXML(SelectedUser_Label.Text);
            List<List<double>> Rezultate = new List<List<double>>();
            for (int i = 0; i < aux.Count; i++)
            {
                List<double> target = new List<double>();
                target.Add(1);
                List<double> Inputs = new List<double>(aux[i]);
                Rezultate.Add(rs.ForWordPropagation(Inputs, target));
            }

            List<double> rez = new List<double>();
            List<double> targ = new List<double>();
            for(int i  = 0; i < Rezultate.Count;i++)
            {
                List<double> au = new List<double>(Rezultate[i]);
                double value = au[0];
                rez.Add(value);
                targ.Add(1);
            }

            double eroare = Eroarre(rez, targ) ;
            Verificare_Label.Text = eroare.ToString() + "%";
            if(Verificare_Label.Text == "NaN%")
            {
                MessageBox.Show("Inregistrare esuata");
                Verificare_Label.Text = "%";
            }
        }

        private void Testare_Click(object sender, EventArgs e)
        {
            string user = SelectedUser_Label.Text;
            if (user == "Nimeni")
            {
                return;
            }
            rs = new ReteaNeuronala(12, 1, nrNeuroni, 1);
            string path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\Weights\\{user}.txt";
            bool a = File.Exists(path);
            if (File.Exists(path))
            {
                rs.ImportXML(user);
            }
            else
            {
                rs.ExportXML(user);
            }

            Inputs = new List<List<double>>();
            Target = new List<List<double>>();

              
            path = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSet\\{SelectedUser_Label.Text}.txt";
            FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);
            List<List<double>> ob = (List<List<double>>)xss.Deserialize(f);
            for (int i = 0; i < ob.Count; i++)
            {
                List<double> aux = new List<double>(ob[i]);
                aux.RemoveAt(aux.Count - 1);
                Inputs.Add(new List<double>(aux));

                aux = new List<double>(ob[i]);
                aux.RemoveRange(0, 12);
                Target.Add(new List<double>(aux));
            }


            List<double> Rez = new List<double>();
            List<double> Targ = new List<double>();
            for (int j = 0; j < Inputs.Count(); j++)
            {
                rs.ForWordPropagation(Inputs[j], Target[j]);
                List<double> tg_aux = new List<double>(Target[j]);
                List<double> rz_aux = new List<double>(rs.Return_Result());
                Rez.Add(rz_aux[0]);
                Targ.Add(tg_aux[0]);
            }

            Testare_label.Text =  Eroarre(Rez, Targ).ToString() + "%";
            f.Close();
            f.Dispose();          
        }


        List<double> re;
        List<string> st;


        private void Identificare_Click(object sender, EventArgs e)
        {

            listBox2.Items.Clear();



            string p = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\TestSample\\ Identificare.wav";
            VerificareForm form = new VerificareForm(p,DeviceNumber);

            List<NAudio.Wave.WaveInCapabilities> sourse = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sourse.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            if (!(sourse.Count >= DeviceNumber - 1))
            {
                DeviceNumber = sourse.Count() - 1;
            }

            form.ShowDialog();
            if (!File.Exists(p))
            {
                MessageBox.Show("Eroare, inregistrarea nu a fost efectuata cu succes");
                return;
            }
            List<List<double>> aux = new List<List<double>>(DoMFCC(p));

            re = new List<double>();
            st = new List<string>();

            foreach (string item in Directory.GetFiles("Users"))
            {

                FileInfo f = new FileInfo(item);
                rs = new ReteaNeuronala(12, 1, nrNeuroni, 1);
                if(!File.Exists($"{Path.GetDirectoryName(Application.ExecutablePath)}\\Weights\\{f.Name}.txt"))
                {
                    rs.ExportXML(f.Name);
                }
                rs.ImportXML(f.Name);
                List<List<double>> Rezultate = new List<List<double>>();
                for (int i = 0; i < aux.Count; i++)
                {
                    List<double> target = new List<double>();
                    target.Add(1);
                    List<double> Inputs = new List<double>(aux[i]);
                    Rezultate.Add(rs.ForWordPropagation(Inputs, target));
                }

                List<double> rez = new List<double>();
                List<double> targ = new List<double>();
                for (int i = 0; i < Rezultate.Count; i++)
                {
                    List<double> au = new List<double>(Rezultate[i]);
                    double value = au[0];
                    rez.Add(value);
                    targ.Add(1);
                }

                double eroare = Eroarre(rez, targ);
                if (Verificare_Label.Text == "NaN%")
                {
                    MessageBox.Show("Inregistrare esuata");
                    return;
                   
                }
                else
                {
                    re.Add(eroare);
                    st.Add(f.Name);
                }
            }

            Sort();

            for(int i = 0; i < st.Count;i++)
            {
                listBox2.Items.Add(st[i] + $"-> {re[i]}%");
            }
            
        }


        private void Sort()
        {
            int n = re.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (re[j] > re[j + 1])
                    {
                        // swap temp and arr[i] 
                        double temp = re[j];
                        re[j] = re[j + 1];
                        re[j + 1] = temp;

                        string s = st[j];
                        st[j] = st[j + 1];
                        st[j + 1] = s;
                    }
                }                   
            }
            st.Reverse();
            re.Reverse();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
            
            if(form.ShouldReturn())
            {
                DeviceNumber = form.ReturnMic();
            }
        }
    }
}


