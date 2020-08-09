using System;
using System.Windows.Forms;


namespace PangyaDebugLauncher
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>



        [STAThread]
        static void Main()
        {
            /*var myClient = new WebClient();
            string url = "https://drive.google.com/uc?id=1LtnW0qyDtNZVme-xIZ804M31pGBj-NXi&export=download";
            Stream response = myClient.OpenRead(url);
            // The stream data is used here.
            byte[] buffer = new byte[1048576 * 5];
            response.ReadAsync(buffer, 0, buffer.Length);*/

            /*
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
            client.DownloadFileAsync(new Uri("https://drive.google.com/u/0" + downloadURL), @"c:\myfile.txt");
            */


            //response.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

        }

    }
}
