using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OggencFrontend
{
    public partial class Setting : Form
    {
        Dictionary<string, int> dicSampling = new Dictionary<string, int>();
        Dictionary<string, int> dicBitrate = new Dictionary<string, int>();

        Form1 mainForm;

        public Setting()
        {
            InitializeComponent();
            for (int i = 0; i < comboSampling.Items.Count; i++)
            {
                dicSampling[comboSampling.Items[i].ToString()] = i;
            }
            for (int i = 0; i < comboBitrate.Items.Count; i++)
            {
                dicBitrate[comboBitrate.Items[i].ToString()] = i;
            }

        }

        public void setMainForm(Form1 main)
        {
            mainForm = main;
        }

        public void setSettings(bool isBitrate,int level,double bitrate,int samplingRate,bool useTag,bool useSampling,double volume,string oggenc,bool cbr,ushort priority,bool usePriority)
        {
            levelBox.Checked = !isBitrate;
            bitrateBox.Checked = isBitrate;
            numericLevel.Enabled = !isBitrate;
            comboBitrate.Enabled = isBitrate;
            comboSampling.Enabled = useSampling;
            comboSampling.SelectedIndex = dicSampling[samplingRate.ToString()];
            checkSampling.Checked = useSampling;
            checkTag.Checked = useTag;
            if (!dicBitrate.ContainsKey(bitrate.ToString()))
            {
                bitrate = 160;
            }
            comboBitrate.SelectedIndex = dicBitrate[bitrate.ToString()];
            numericLevel.Value = level;
            if((decimal)(volume * 100) > numericVolume.Maximum)
            {
                volume = (double)numericVolume.Maximum / 100;
            }
            numericVolume.Value = (decimal)volume * 100;
            textBox1.Text = oggenc;
            checkCBR.Checked = cbr;
            comboPriority.SelectedIndex = priority;
            checkPriority.Checked = usePriority;
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            comboSampling.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBitrate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPriority.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void levelBox_CheckedChanged(object sender, EventArgs e)
        {
            numericLevel.Enabled = levelBox.Checked;
            comboBitrate.Enabled = !levelBox.Checked;
        }

        public int getSamplingRate()
        {
            string s = comboSampling.SelectedItem.ToString();
            return Convert.ToInt32(s);
        }
        public int getBitrate()
        {
            string s = comboBitrate.SelectedItem.ToString();
            return Convert.ToInt32(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mainForm.setSettings(bitrateBox.Checked, checkTag.Checked,(int)numericLevel.Value, getSamplingRate(), getBitrate(), checkSampling.Checked, ((double)numericVolume.Value) / 100, textBox1.Text,checkCBR.Checked,(ushort)comboPriority.SelectedIndex,checkPriority.Checked);
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkSampling_CheckedChanged(object sender, EventArgs e)
        {
            comboSampling.Enabled = checkSampling.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openOggenc.ShowDialog();
        }

        private void openOggenc_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openOggenc.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show(this,"Are you sure you want to reset settings and quit?","Question",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                mainForm.resetSettings();
                mainForm.Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkCBR.Text = checkCBR.Checked ? "固定ビットレート" : "平均ビットレート";
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            comboPriority.Enabled = checkPriority.Checked;
        }
    }
}
