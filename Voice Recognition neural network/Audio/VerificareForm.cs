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

namespace Audio
{
    public partial class VerificareForm : Form
    {

        public VerificareForm(string n, int DvNr)
        {
            InitializeComponent();
            path = n;
            pictureBox1.ImageLocation = pictureLocation_Red;
            deviceNumber = DvNr;
        }

        #region Inregistreaza  

        string path;
        int deviceNumber;
        WaveIn microfon = null;
        WaveFileWriter fileWriter = null;
        bool should_record = true;
        public bool ok = false;
        string pictureLocation_Red = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\img\\Red.Png";
        string pictureLocation_Green = $"{Path.GetDirectoryName(Application.ExecutablePath)}\\img\\Green.Png";

        private void Data(object sender, WaveInEventArgs e)
        {
            if (fileWriter == null)
            {

                return;
            }
            fileWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            fileWriter.Flush();
        }

        private void Microphone_Click(object sender, EventArgs e)
        {

            if(deviceNumber == -1)
            {
                MessageBox.Show("Nu ai microfon");
                return;
            }

            if (should_record)
            {
                should_record = false;
                pictureBox1.ImageLocation = pictureLocation_Green;

                int deviceNumber = 0;
                microfon = new NAudio.Wave.WaveIn();
                microfon.DeviceNumber = deviceNumber;
                microfon.WaveFormat = new NAudio.Wave.WaveFormat(16384, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

                microfon.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(Data);
           
                fileWriter = new NAudio.Wave.WaveFileWriter(path, microfon.WaveFormat);             
               
                microfon.StartRecording();
            }
            else
            {
                should_record = true;

                if (microfon != null)
                {
                    microfon.StopRecording();
                    microfon.Dispose();
                    microfon = null;
                }
                if (fileWriter != null)
                {
                    fileWriter.Dispose();
                    fileWriter = null;
                }
                ok = true;
                this.Close();
            }       

        }

        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (microfon != null)
            {
                microfon.StopRecording();
                microfon.Dispose();
                microfon = null;
            }
            if (fileWriter != null)
            {
                fileWriter.Dispose();
                fileWriter = null;
            }
               
            if(should_record == false)
            {
                if (File.Exists(path)) File.Delete(path);
            }

        }


        private void AddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() != DialogResult.OK) return;

            string[] pa = open.FileName.Split('.');
            string p = $"{pa[0]}.wav";

            try
            {
                File.Copy(p, path);
            }
            catch
            {
                File.Delete(path);
                File.Copy(p, path);
            }

            this.Close();
        }
    }
}

#endregion 