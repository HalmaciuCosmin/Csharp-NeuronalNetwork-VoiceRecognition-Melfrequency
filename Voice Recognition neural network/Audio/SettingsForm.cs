using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using NAudio;
using NAudio.Wave;

namespace RecunoastereaVorbitorului
{
    public partial class SettingsForm : Form
    {
        int deviceNumber;
        bool b = false;
        public SettingsForm()
        {
            InitializeComponent();
            listView1.Dock = DockStyle.Fill;
            
            List<NAudio.Wave.WaveInCapabilities> sourse = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sourse.Add(NAudio.Wave.WaveIn.GetCapabilities(i));               
            }

            if (sourse.Count == 0)
            {
                MessageBox.Show("nu ai nici un microfon conectat");
            }

            foreach (WaveInCapabilities s in sourse)
            {
                ListViewItem item = new ListViewItem(s.ProductName);                
                listView1.Items.Add(item);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nu ai selectat nici un microfon");
                this.Close();
                return;
            }

            deviceNumber = listView1.SelectedItems[0].Index;
            b = true;
            this.Close();
        }


        public int ReturnMic()
        {
            return deviceNumber;
        }
        
        public bool ShouldReturn()
        {
            return b;
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            List<NAudio.Wave.WaveInCapabilities> sourse = new List<NAudio.Wave.WaveInCapabilities>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sourse.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            if (sourse.Count == 0)
            {
                MessageBox.Show("nu ai nici un microfon conectat");
            }

            foreach (WaveInCapabilities s in sourse)
            {
                ListViewItem item = new ListViewItem(s.ProductName);
                listView1.Items.Add(item);
            }
        }
    }
}
