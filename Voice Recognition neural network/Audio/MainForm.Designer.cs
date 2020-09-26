namespace Audio
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.OPEN = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Worker1 = new System.ComponentModel.BackgroundWorker();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.Identificare = new System.Windows.Forms.Button();
            this.Testare_label = new System.Windows.Forms.Label();
            this.Testare = new System.Windows.Forms.Button();
            this.NumericUpDown_Eroare = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.StergeUtilizator = new System.Windows.Forms.Button();
            this.Verificare_Label = new System.Windows.Forms.Label();
            this.Verificare = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.GeneratieValue_label = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.EroareValue_Label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Stop = new System.Windows.Forms.Button();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SelectedUser_Label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Learning = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.AddUser = new System.Windows.Forms.Button();
            this.textBox_User = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.Worker = new System.ComponentModel.BackgroundWorker();
            this.Settings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Eroare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // OPEN
            // 
            this.OPEN.Location = new System.Drawing.Point(6, 343);
            this.OPEN.Name = "OPEN";
            this.OPEN.Size = new System.Drawing.Size(133, 22);
            this.OPEN.TabIndex = 1;
            this.OPEN.Text = "Adauga Fisier .Wav";
            this.OPEN.UseVisualStyleBackColor = true;
            this.OPEN.Click += new System.EventHandler(this.OPEN_Click);
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            this.chart1.Location = new System.Drawing.Point(6, 6);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(764, 416);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // chart2
            // 
            chartArea5.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea5);
            this.chart2.Location = new System.Drawing.Point(3, 3);
            this.chart2.Name = "chart2";
            this.chart2.Size = new System.Drawing.Size(767, 421);
            this.chart2.TabIndex = 6;
            this.chart2.Text = "chart2";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(781, 454);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.Settings);
            this.tabPage4.Controls.Add(this.listBox2);
            this.tabPage4.Controls.Add(this.Identificare);
            this.tabPage4.Controls.Add(this.Testare_label);
            this.tabPage4.Controls.Add(this.Testare);
            this.tabPage4.Controls.Add(this.NumericUpDown_Eroare);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.StergeUtilizator);
            this.tabPage4.Controls.Add(this.Verificare_Label);
            this.tabPage4.Controls.Add(this.Verificare);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.NumericUpDown);
            this.tabPage4.Controls.Add(this.GeneratieValue_label);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.EroareValue_Label);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.Stop);
            this.tabPage4.Controls.Add(this.chart3);
            this.tabPage4.Controls.Add(this.SelectedUser_Label);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.Learning);
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.OPEN);
            this.tabPage4.Controls.Add(this.AddUser);
            this.tabPage4.Controls.Add(this.textBox_User);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(773, 428);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Learning";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(647, 49);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(120, 316);
            this.listBox2.TabIndex = 33;
            // 
            // Identificare
            // 
            this.Identificare.Location = new System.Drawing.Point(673, 372);
            this.Identificare.Name = "Identificare";
            this.Identificare.Size = new System.Drawing.Size(75, 38);
            this.Identificare.TabIndex = 32;
            this.Identificare.Text = "Identificare";
            this.Identificare.UseVisualStyleBackColor = true;
            this.Identificare.Click += new System.EventHandler(this.Identificare_Click);
            // 
            // Testare_label
            // 
            this.Testare_label.AutoSize = true;
            this.Testare_label.Location = new System.Drawing.Point(597, 392);
            this.Testare_label.Name = "Testare_label";
            this.Testare_label.Size = new System.Drawing.Size(15, 13);
            this.Testare_label.TabIndex = 31;
            this.Testare_label.Text = "%";
            // 
            // Testare
            // 
            this.Testare.Location = new System.Drawing.Point(497, 383);
            this.Testare.Name = "Testare";
            this.Testare.Size = new System.Drawing.Size(80, 36);
            this.Testare.TabIndex = 30;
            this.Testare.Text = "Testare";
            this.Testare.UseVisualStyleBackColor = true;
            this.Testare.Click += new System.EventHandler(this.Testare_Click);
            // 
            // NumericUpDown_Eroare
            // 
            this.NumericUpDown_Eroare.DecimalPlaces = 4;
            this.NumericUpDown_Eroare.Location = new System.Drawing.Point(243, 346);
            this.NumericUpDown_Eroare.Name = "NumericUpDown_Eroare";
            this.NumericUpDown_Eroare.Size = new System.Drawing.Size(81, 20);
            this.NumericUpDown_Eroare.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(162, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "EroareDorita";
            // 
            // StergeUtilizator
            // 
            this.StergeUtilizator.Location = new System.Drawing.Point(6, 316);
            this.StergeUtilizator.Name = "StergeUtilizator";
            this.StergeUtilizator.Size = new System.Drawing.Size(133, 23);
            this.StergeUtilizator.TabIndex = 25;
            this.StergeUtilizator.Text = "Sterge Utilizator Selectat";
            this.StergeUtilizator.UseVisualStyleBackColor = true;
            this.StergeUtilizator.Click += new System.EventHandler(this.StergeUtilizator_Click);
            // 
            // Verificare_Label
            // 
            this.Verificare_Label.AutoSize = true;
            this.Verificare_Label.Location = new System.Drawing.Point(444, 395);
            this.Verificare_Label.Name = "Verificare_Label";
            this.Verificare_Label.Size = new System.Drawing.Size(15, 13);
            this.Verificare_Label.TabIndex = 24;
            this.Verificare_Label.Text = "%";
            // 
            // Verificare
            // 
            this.Verificare.Location = new System.Drawing.Point(341, 386);
            this.Verificare.Name = "Verificare";
            this.Verificare.Size = new System.Drawing.Size(80, 35);
            this.Verificare.TabIndex = 23;
            this.Verificare.Text = "Verificare";
            this.Verificare.UseVisualStyleBackColor = true;
            this.Verificare.Click += new System.EventHandler(this.Verificare_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 397);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "NrMaximEpoci";
            // 
            // NumericUpDown
            // 
            this.NumericUpDown.Location = new System.Drawing.Point(243, 396);
            this.NumericUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.NumericUpDown.Name = "NumericUpDown";
            this.NumericUpDown.Size = new System.Drawing.Size(81, 20);
            this.NumericUpDown.TabIndex = 21;
            // 
            // GeneratieValue_label
            // 
            this.GeneratieValue_label.AutoSize = true;
            this.GeneratieValue_label.Location = new System.Drawing.Point(351, 310);
            this.GeneratieValue_label.Name = "GeneratieValue_label";
            this.GeneratieValue_label.Size = new System.Drawing.Size(13, 13);
            this.GeneratieValue_label.TabIndex = 20;
            this.GeneratieValue_label.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(301, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Epoca :";
            // 
            // EroareValue_Label
            // 
            this.EroareValue_Label.AutoSize = true;
            this.EroareValue_Label.Location = new System.Drawing.Point(450, 310);
            this.EroareValue_Label.Name = "EroareValue_Label";
            this.EroareValue_Label.Size = new System.Drawing.Size(9, 13);
            this.EroareValue_Label.TabIndex = 18;
            this.EroareValue_Label.Text = "l";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(397, 310);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Eroare : ";
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(497, 330);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(80, 36);
            this.Stop.TabIndex = 15;
            this.Stop.Text = "Opreste";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // chart3
            // 
            chartArea6.Name = "wave";
            this.chart3.ChartAreas.Add(chartArea6);
            legend2.Name = "Legend1";
            this.chart3.Legends.Add(legend2);
            this.chart3.Location = new System.Drawing.Point(151, 7);
            this.chart3.Name = "chart3";
            series2.ChartArea = "wave";
            series2.Legend = "Legend1";
            series2.Name = "Eroare";
            this.chart3.Series.Add(series2);
            this.chart3.Size = new System.Drawing.Size(432, 300);
            this.chart3.TabIndex = 14;
            this.chart3.Text = "chart3";
            // 
            // SelectedUser_Label
            // 
            this.SelectedUser_Label.AutoSize = true;
            this.SelectedUser_Label.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.SelectedUser_Label.Location = new System.Drawing.Point(249, 310);
            this.SelectedUser_Label.Name = "SelectedUser_Label";
            this.SelectedUser_Label.Size = new System.Drawing.Size(39, 13);
            this.SelectedUser_Label.TabIndex = 13;
            this.SelectedUser_Label.Text = "Nimeni";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(148, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Utilizator Selectat :";
            // 
            // Learning
            // 
            this.Learning.Location = new System.Drawing.Point(341, 336);
            this.Learning.Name = "Learning";
            this.Learning.Size = new System.Drawing.Size(80, 34);
            this.Learning.TabIndex = 11;
            this.Learning.Text = "Invatare";
            this.Learning.UseVisualStyleBackColor = true;
            this.Learning.Click += new System.EventHandler(this.Learning_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(3, 7);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(133, 303);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // AddUser
            // 
            this.AddUser.Location = new System.Drawing.Point(6, 397);
            this.AddUser.Name = "AddUser";
            this.AddUser.Size = new System.Drawing.Size(133, 25);
            this.AddUser.TabIndex = 9;
            this.AddUser.Text = "Adauga Utilizator";
            this.AddUser.UseVisualStyleBackColor = true;
            this.AddUser.Click += new System.EventHandler(this.AddUser_Click);
            // 
            // textBox_User
            // 
            this.textBox_User.Location = new System.Drawing.Point(6, 371);
            this.textBox_User.Name = "textBox_User";
            this.textBox_User.Size = new System.Drawing.Size(133, 20);
            this.textBox_User.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chart1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(773, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "WaveGraph";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.chart2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(773, 428);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "FFtGraph";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Worker
            // 
            this.Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Work);
            // 
            // Settings
            // 
            this.Settings.Image = global::RecunoastereaVorbitorului.Properties.Resources.Settings;
            this.Settings.Location = new System.Drawing.Point(732, 0);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(35, 33);
            this.Settings.TabIndex = 9;
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 469);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "RecunoastereaVorbitorului";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown_Eroare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OPEN;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.ComponentModel.BackgroundWorker Worker1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TextBox textBox_User;
        private System.Windows.Forms.Button AddUser;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Learning;
        private System.Windows.Forms.Label SelectedUser_Label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label GeneratieValue_label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label EroareValue_Label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.ComponentModel.BackgroundWorker Worker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown NumericUpDown;
        private System.Windows.Forms.Button Verificare;
        private System.Windows.Forms.Label Verificare_Label;
        private System.Windows.Forms.Button StergeUtilizator;
        private System.Windows.Forms.NumericUpDown NumericUpDown_Eroare;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Testare_label;
        private System.Windows.Forms.Button Testare;
        private System.Windows.Forms.Button Identificare;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button Settings;
    }
}

