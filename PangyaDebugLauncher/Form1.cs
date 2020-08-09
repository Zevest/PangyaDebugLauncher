using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PangyaDebugLauncher
{
    struct FileObject
    {
        public string downloadLink;
        private string fileName;
        private string fileLocation;
        private string fileExtension;
        private string fullPath;
        public bool overwrite;

        private bool changed;
        public string FullPath
        {
            get { if (changed) fullPath = FileLocation + "\\" + FileName + "." + FileExtension; return fullPath; }
        }
        public string FileName
        {
            get => fileName;
            set { changed = true; fileName = value; }
        }
        public string FileLocation
        {
            get => fileLocation;
            set { changed = true; fileLocation = value; }
        }
        public string FileExtension
        {
            get => fileExtension;
            set { changed = true; fileExtension = value; }
        }
        public string FileNameWithExtension { get => (FileName + "." + FileExtension); }
        public string FullPathNoExtension { get => (FileLocation + "\\" + FileName); }
    };

    public partial class MainForm : Form
    {
        OptionForm optionForm;
        readonly AppDownloader downloader;

        private int StepCount = 6;
        private int currentStep = 0;


        public readonly Image[] bg = {global::PangyaDebugLauncher.Properties.Resources.Pangya_full,
                      global::PangyaDebugLauncher.Properties.Resources.pangyaSunset,
                      global::PangyaDebugLauncher.Properties.Resources.pangyaUnited
        };
        public MainForm()
        {
            //Console.WriteLine("path : {0}::: {1}", ResourceLocation.Embedded, Ress);
            if ((string)Properties.Settings.Default["InstallationPath"] == "")
            {
                Properties.Settings.Default["InstallationPath"] = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            }
            //Properties.Settings.Default.IsRegInstalled = false;
            InitializeComponent();
            int imgIndex;
            if ((int)Properties.Settings.Default["backgroundMode"] == 0)
            {
                Random random = new Random();
                imgIndex = random.Next() % bg.Length;
            }
            else
            {
                imgIndex = (int)Properties.Settings.Default["imageIndex"];
            }

            this.BackgroundImage = bg[imgIndex];
            downloader = new AppDownloader();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            loadingBar.Hide();
            stepLoadingBar.Hide();
            if (!Directory.Exists(Properties.Settings.Default.installationPath + Properties.Settings.Default.AppFolderName))
            {

                updateButton.Enabled = false;
                playButton.Enabled = false;
            }
        }


        public void UpdateBackground(int index)
        {
            this.BackgroundImage = bg[index];
        }

        private void OptionButton_Click(object sender, EventArgs e)
        {
            if (optionForm == null || optionForm.IsDisposed)
            {
                optionForm = new OptionForm();
                optionForm.Show(this);
            }
            else
            {
                optionForm.Focus();
            }
        }

        void InstallRegFile()
        {

            if (!((bool)Properties.Settings.Default["IsRegInstalled"]))
            {
                string reg;
                string fdest = Properties.Settings.Default.AppFolderName;
                if (Environment.Is64BitOperatingSystem)
                    reg = Properties.Settings.Default.reg64Path;
                else
                    reg = Properties.Settings.Default.reg32Path;
                try
                {
                    string strRegPath = Properties.Settings.Default.installationPath + fdest + reg;
                    Console.WriteLine("Starting " + strRegPath);
                    Process.Start("regedit.exe", "/s \"" + strRegPath + "\"").WaitForExit();
                    Properties.Settings.Default["IsRegInstalled"] = true;
                    Properties.Settings.Default.Save();
                }
                catch (Exception exce)
                {
                    Console.WriteLine(exce.Message);
                    return;
                }
            }
            Console.WriteLine("Done Installing RegFile");
            loadingBar.Value += 20;
        }

        private async Task<bool> DownloadAndInstallPatch(object sender)
        {
            FileObject f = new FileObject
            {
                FileName = "debugPatch",
                FileExtension = "zip",
                FileLocation = Properties.Settings.Default.installationPath,
                downloadLink = Properties.Settings.Default.PatchFileAltLink,
                overwrite = true
            };
            FileObject dest = new FileObject
            {
                overwrite = true,
                FileLocation = f.FileLocation + Properties.Settings.Default.AppFolderName
            };
            Console.WriteLine("Donwloading file {0} and saving it at {1}", f.FileNameWithExtension, f.FullPath);

            bool result;

            result = await downloader.Download(f, UpdateStepLoading, DownloadFileComplete);
            if (!result)
            {
                Console.WriteLine("Error downloaing");
                return false; ;

            }
            Console.WriteLine("Done Donwloading");

            Console.WriteLine("Extracting Files {0}, {1}, {2}", f.FileName, f.FileLocation, f.FullPath);
            result = await downloader.ExtractFileAsync(sender, f, dest, UpdateStepLoading, DownloadFileComplete);
            Console.WriteLine("Done Extracting PatchFiles :{0}", result);
            return true;
        }

        private async Task<bool> DownloadAndInstallregFile(object sender)
        {
            FileObject f = new FileObject
            {
                FileName = "regFile",
                FileExtension = "zip",
                FileLocation = Properties.Settings.Default.installationPath
            };
            FileObject dest = new FileObject
            {
                FileLocation = f.FileLocation + Properties.Settings.Default.AppFolderName
            };

            string BaseURL = Properties.Settings.Default["BaseURL"].ToString();
            string RegisteryFileID = Properties.Settings.Default["RegisteryFileID"].ToString();
            Regex rx = new Regex("href=\"(/uc\\?export=download[^\"]+)",
                            RegexOptions.Compiled | RegexOptions.IgnoreCase);
            string URL = Regex.Replace(BaseURL, "<<<<FILEIDHERE>>>>", RegisteryFileID);
            f.downloadLink = URL;

            Console.WriteLine("Donwloading file {0} and saving it at {1}", f.FileNameWithExtension, f.FullPath);

            bool result;

            result = await downloader.Download(f, UpdateStepLoading, DownloadFileComplete);
            if (!result)
            {
                Console.WriteLine("Error downloaing");
                return false;
            }
            Console.WriteLine("Done Donwloading");

            Console.WriteLine("Extracting Files {0}, {1}, {2}", f.FileName, f.FileLocation, dest.FullPath);
            result = await downloader.ExtractFileAsync(sender, f, dest, UpdateStepLoading, DownloadFileComplete);

            if (!result)
            {
                Console.WriteLine("Cannot Install regFile");
                return false;
            }
            Console.WriteLine("Done Extracting PatchFiles :{0}", result);
            InstallRegFile();
            return true;
        }

        private async Task<bool> DownloadAndInstallClient(object sender)
        {
            StepCount = (Properties.Settings.Default.IsRegInstalled ? 4 : 6);
            FileObject f = new FileObject
            {
                FileName = "PangyaUS_851",
                FileExtension = "rar",
                FileLocation = Properties.Settings.Default.installationPath,
                downloadLink = Properties.Settings.Default.clientFileAltLink
            };
            FileObject dest = new FileObject
            {
                FileLocation = f.FileLocation
            };
            Console.WriteLine("Donwloading file {0} and saving it at {1}", f.FileNameWithExtension, f.FullPath);
            bool result = await downloader.Download(f, UpdateStepLoading, DownloadFileComplete);
            if (result)
            {
                result = await downloader.ExtractFileAsync(sender, f, dest, UpdateStepLoading, DownloadFileComplete);
            }
            return true;
        }

        private async Task<bool> InstallClientFile(object sender)
        {
            //string currentFileName = Properties.Settings.Default.installationPath + Properties.Settings.Default.AppFolderName;
            if (!(await DownloadAndInstallClient(sender)))
                return false;
            if (!(await DownloadAndInstallregFile(sender)))
                return false;
            return true;
        }

        private async Task<bool> UpdateGame(object sender)
        {
            StepCount = 2;
            if (!(await DownloadAndInstallPatch(sender)))
                return false;
            return true;
        }

        private void UpdateStepLoading(object sender, ProgressChangedEventArgs e)
        {
            stepLoadingBar.Value = e.ProgressPercentage;
            loadingBar.Value = (int)Math.Ceiling((float)(currentStep * 100 + e.ProgressPercentage) / (float)(StepCount * 100));
        }

        private async void DownloadButton_Click(object sender, EventArgs e)
        {
            //downloadGameFile();
            stepLoadingBar.Show();
            loadingBar.Show();
            updateButton.Enabled = false;
            playButton.Enabled = false;
            optionButton.Enabled = false;
            downloadButton.Enabled = false;
            await InstallClientFile(sender);
            await UpdateGame(sender);
            updateButton.Enabled = true;
            playButton.Enabled = true;
            optionButton.Enabled = true;
            downloadButton.Enabled = true;
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone. Utilisez l'opérateur 'await' pour attendre les appels d'API non bloquants ou 'await Task.Run(…)' pour effectuer un travail utilisant le processeur sur un thread d'arrière-plan.
        private async void PlayButton_Click(object sender, EventArgs e)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone. Utilisez l'opérateur 'await' pour attendre les appels d'API non bloquants ou 'await Task.Run(…)' pour effectuer un travail utilisant le processeur sur un thread d'arrière-plan.
        {
            string appFolder = Properties.Settings.Default.installationPath + Properties.Settings.Default.AppFolderName;
            string exe = @"\Debug center.exe";
            if (Directory.Exists(appFolder) && File.Exists(appFolder + exe))
            {
                Process.Start(appFolder + exe);
            }
            else
            {
                playButton.Enabled = false;
            }
        }

        private async void UpdateButton_Click(object sender, EventArgs e)
        {
            stepLoadingBar.Show();
            loadingBar.Show();
            updateButton.Enabled = false;
            playButton.Enabled = false;
            optionButton.Enabled = false;
            downloadButton.Enabled = false;
            await UpdateGame(sender);
            updateButton.Enabled = true;
            playButton.Enabled = true;
            optionButton.Enabled = true;
            downloadButton.Enabled = true;
        }

        private void DownloadFileComplete(object sender, AsyncCompletedEventArgs e)
        {
            Console.WriteLine("DownloadCompleted");
            currentStep += 1;
        }


    }
}
