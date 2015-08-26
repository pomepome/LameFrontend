using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using CSCore;
using NVorbis;
using NVorbis.Ogg;
using TagLib;
using LameFrontend.Properties;
using CSCore.Codecs;
using CSCore.MediaFoundation;
using NVorbis.NAudioSupport;
using NAudio.Wave;

namespace OggencFrontend
{
    public partial class Form1 : Form
    {

        Setting setting = new Setting();

        bool isBitrate, useTag, useSampling, cbr, usePriority;
        int level, samplingRate;
        ushort priority;
        double bitrate, volume;

        string runDir; 

        public Form1()
        {
            InitializeComponent();
            AddOwnedForm(setting);
            setting.setMainForm(this);
            openAudio.Filter = "Supported Formats(*.wav *.ogg *.ogx *.oga *.flac *.aac *.m4a *.wma)|*.wav;*.ogg;*.ogx;*.oga;*.flac;*.aac;*.m4a;*.wma|Wave audio(*.wav)|*.wav|Ogg Volbis Audio(*.ogg,*.ogx,*.oga)|*.ogg;*.ogx;*.oga|Free Lossless Audio(*.flac)|*.flac|Advanced Audio Codec(*.aac,*.m4a)|*.aac;*.m4a|Windows Media Audio(*.wma)|*.wma";
        }

        public void setSettings(bool is_Bitrate,bool use_tag,int lev,int sampling_rate,double bit_rate,bool useSamplingRate,double volume,string lame,bool useCbr,ushort cpuPriority,bool use_priority)
        {
            isBitrate = is_Bitrate;
            useTag = use_tag;
            level = lev;
            samplingRate = sampling_rate;
            bitrate = bit_rate;
            useSampling = useSamplingRate;
            this.volume = volume;
            runDir = lame;
            cbr = useCbr;
            priority = cpuPriority;
            usePriority = use_priority;
            saveSettings();
        }
        public void resetSettings()
        {
            isBitrate = false;
            useTag = true;
            level = 2;
            samplingRate = 44100;
            bitrate = 160;
            useSampling = false;
            volume = 1;
            runDir = "";
            cbr = false;
            priority = 2;
            usePriority = false;
            saveSettings();
        }
        private bool Decode(string filePath)
        {
            if (!Directory.Exists("temp"))
            {
                Directory.CreateDirectory("temp");
            }
            {
                string toOut = @"temp\" + Path.GetFileNameWithoutExtension(filePath) + ".wav";
                try
                {
                    using (var decoder = new MediaFoundationDecoder(filePath))
                    {
                        decoder.WriteToFile(toOut);
                    }
                }
                catch (MediaFoundationException e)
                {
                    e.ToString();
                    try
                    {
                        using (var reader = new VorbisWaveReader(filePath))
                        {
                            using (var converter = new Wave32To16Stream(reader))
                            {
                                WaveFileWriter.CreateWaveFile(toOut, converter);
                            }
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void showSetting_Click(object sender, EventArgs e)
        {
            setting.setSettings(isBitrate, level, bitrate, samplingRate, useTag, useSampling, volume, runDir, cbr, priority, usePriority);
            setting.ShowDialog();
        }

        private void saveSettings()
        {
            Settings s = getSettings();
            s.isBitrate = isBitrate;
            s.Level = level;
            s.Bitrate = bitrate;
            s.SamplingRate = samplingRate;
            s.useTag = useTag;
            s.LamePath = runDir;
            s.useSampling = useSampling;
            s.Volume = volume;
            s.Priority = priority;
            s.usePriority = usePriority;
            s.Save();
        }

        private void openAudio_FileOk(object sender, CancelEventArgs e)
        {
            string[] pathes = openAudio.FileNames;
            foreach(string path in pathes)
            {
                if (!listBox1.Items.Contains(path))
                {
                    listBox1.Items.Add(path);
                }
            }
        }

        private void deleteFromList_Click(object sender, EventArgs e)
        {
            while(listBox1.SelectedIndex > -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void addFile_Click(object sender, EventArgs e)
        {
            openAudio.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("out"))
            {
                Directory.CreateDirectory("out");
            }
            process();
        }
        private bool IsFileLocked(string path)
        {
            FileStream stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch
            {
                return true;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }

            return false;
        }
        private string addQuotation(object src)
        {
            char q = '\"';
            return String.Format("{0}{1}{2}", q, src, q);
        }
        private void process()
        {
            if (listBox1.Items.Count > 0)
            {
                string path = listBox1.Items[0].ToString();
                string baseName = Path.GetFileNameWithoutExtension(path);
                string option = "";
                bool doDecode = true;
                string toEncode = addQuotation(path);
                string toOutput = String.Format(@"out\{0}.mp3", baseName);

                if (System.IO.File.Exists(path))
                {
                    if (System.IO.File.Exists(toOutput) && IsFileLocked(toOutput))
                    {
                        MessageBox.Show(this, "出力先のファイルにロックが掛かっています。\nファイルを開いているタスクを閉じてください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Path.GetExtension(path) == ".wav")
                    {
                        doDecode = false;
                    }
                    else
                    {
                        if(!Decode(path))
                        {
                            return;
                        }
                        toEncode = addQuotation(String.Format("temp/{0}.wav",baseName));
                    }
                    option = getOption(path);
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = runDir;
                    psi.UseShellExecute = true;
                    psi.Arguments = String.Format("{0}{1} -o {2}", toEncode, option, addQuotation(toOutput));

                    Process p = Process.Start(psi);// Execution lame.exe
                    p.WaitForExit();

                    if (doDecode)
                    {
                        System.IO.File.Delete(@"temp\" + baseName + ".wav");
                        Directory.Delete(@"temp\");
                    }
                    listBox1.Items.RemoveAt(0);
                    if (listBox1.Items.Count > 0)
                    {
                        process();
                    }
                }
                else
                {
                    MessageBox.Show(this, "ファイルが見つかりませんでした:" + Path.GetFileName(path), "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listBox1.Items.RemoveAt(0);
                }
            }
        }

        private string getOption(string path)
        {
            string option = "";
            if (Path.GetExtension(path) != ".wav")
            {
                if (useTag)
                {
                    TagLib.File tagFile = TagLib.File.Create(path);
                    Tag tag = tagFile.Tag;
                    if (tag != null)
                    {
                        if (tag.Comment != null)
                        {
                            option += String.Format(" --tc {0}", addQuotation(tag.Comment));
                        }
                        if (tag.Performers != null && tag.Performers.Length > 0)
                        {
                            option += String.Format(" --ta {0}", addQuotation(tag.Performers[0]));
                        }
                        if (tag.FirstGenre != null)
                        {
                            option += String.Format(" --tg {0}", addQuotation(tag.FirstGenre));
                        }
                        if (tag.Year != 0)
                        {
                            option += String.Format(" --ty {0}", tag.Year);
                        }
                        if (tag.Track != 0)
                        {
                            option += String.Format(" --tn {0}", tag.Track);
                            if (tag.TrackCount != 0)
                            {
                                option += String.Format("/{0}", tag.TrackCount);
                            }
                        }
                        if (tag.Title != null)
                        {
                            option += String.Format(" --tt {0}", addQuotation(tag.Title));
                        }
                        if (tag.Album != null)
                        {
                            option += String.Format(" --tl {0}", addQuotation(tag.Album));
                        }
                    }
                }
            }

            {
                if (isBitrate)
                {
                    if (!cbr)
                    {
                        option += String.Format(" -abr {0}", bitrate);
                    }
                    else
                    {
                        option += String.Format(" --cbr -b {0}", bitrate);
                    }
                }
                else
                {
                    option += String.Format(" -q {0}", level);
                }
                if (useSampling)
                {
                    option += String.Format(" -s {0}", samplingRate);
                }
                if (volume < 1.0)
                {
                    option += String.Format(" --scale {0}", volume);
                }
                if (usePriority)
                {
                    option += String.Format(" --priority {0}",priority);
                }
            }
            return option;
        }

        private void openOggEnc_FileOk(object sender, CancelEventArgs e)
        {
            runDir = openLame.FileName;
            saveSettings();
        }

        private Settings getSettings()
        {
            return Settings.Default;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Settings s = getSettings();
            isBitrate = s.isBitrate;
            useTag = s.useTag;
            level = s.Level;
            samplingRate = s.SamplingRate;
            bitrate = s.Bitrate;
            runDir = s.LamePath;
            useSampling = s.useSampling;
            volume = s.Volume;
            cbr = s.UseCBR;
            priority = s.Priority;
            usePriority = s.usePriority;
            if(runDir == "")
            {
                DialogResult result = openLame.ShowDialog();
                if(result == DialogResult.Cancel)
                {
                    Close();
                }
            }
        }
    }
}
