using System;
using System.Windows.Forms;


namespace PangyaDebugLauncher
{
    public partial class OptionForm : Form
    {
        int backgroundMode;
        int imageIndex;
        MainForm parent;
        public OptionForm()
        {
            InitializeComponent();
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            parent = (MainForm)Owner;
            textBox1.Text = Properties.Settings.Default["InstallationPath"].ToString();

            imageIndex = (int)Properties.Settings.Default["imageIndex"];
            listBox1.SelectedIndex = imageIndex;
            switch ((int)Properties.Settings.Default["backgroundMode"])
            {
                default:
                case 0:
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    backgroundMode = 0;
                    break;
                case 1:
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    backgroundMode = 1;
                    break;
            }
            parent.Enabled = false;
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = Properties.Settings.Default["installationPath"].ToString();
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res.ToString() == "OK")
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["InstallationPath"] = textBox1.Text;
            Properties.Settings.Default["backgroundMode"] = backgroundMode;
            Properties.Settings.Default["ImageIndex"] = imageIndex;
            if (backgroundMode == 1)
            {

                parent.UpdateBackground(imageIndex);
            }
            Properties.Settings.Default.Save();
        }
        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = false;
            backgroundMode = 0;

        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Enabled = true;
            backgroundMode = 1;

        }

        private void ListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            imageIndex = listBox1.SelectedIndex;
        }
    }
}
