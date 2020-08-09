using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PangyaDebugLauncher
{
    class AppDownloader
    {

        private string Action;

        public ProgressChangedEventHandler ProgresslHandler;


        public AppDownloader()
        {
        }

        public void DownloadProgressChangeEventHadler(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine("Percentage {0}", e.ProgressPercentage);
            ProgresslHandler(this, new ProgressChangedEventArgs(e.ProgressPercentage, sender));
        }

        public string GetStatus()
        {
            return Action;
        }


        public async Task<bool> Download(FileObject f, ProgressChangedEventHandler progress = null, AsyncCompletedEventHandler complete = null)
        {
            if (File.Exists(f.FullPath) && !f.overwrite)
                return true;

            ProgresslHandler = progress;
            Console.WriteLine("Progress:: {0}", progress);
            WebClient webClient = new WebClient();

            if (progress != null)
            {
                webClient.DownloadProgressChanged += DownloadProgressChangeEventHadler;
                Console.WriteLine("Not NULL 1");
            }
            if (complete != null)
            {
                webClient.DownloadFileCompleted += complete;
                Console.WriteLine("Not NULL 2");
            }
            await webClient.DownloadFileTaskAsync(new Uri(f.downloadLink), f.FullPath);
            return true;
        }


        public async Task<bool> ExtractFileAsync(object sender, FileObject file, FileObject destination, ProgressChangedEventHandler progress, AsyncCompletedEventHandler complete)
        {
            bool res = false;
            await Task.Run(() => res = ExtractFile(sender, file, destination, progress, complete));
            return res;
        }


        public bool ExtractFile(object sender, FileObject file, FileObject destination, ProgressChangedEventHandler progress, AsyncCompletedEventHandler complete)
        {

            if (!File.Exists(file.FullPath))
            {
                Console.WriteLine("Cannot Find File {0} at ", file.FileName, file.FileLocation);
                return false;
            }
            if (!Directory.Exists(destination.FullPathNoExtension))
                Directory.CreateDirectory(destination.FullPathNoExtension);
            Console.WriteLine("Extracting Game file at {0}", destination.FullPathNoExtension);
            Action = "Extracting Game file at" + destination.FullPathNoExtension;
            //ZipArchive za = ZipFile.OpenRead(f.FullPath);
            var Compressed = ArchiveFactory.Open(file.FullPath);

            var entries = Compressed.Entries;
            float count = entries.Count();
            float i = 0;
            Console.WriteLine("Found {0} file to Extract", count);
            foreach (var entry in entries)
            {
                //Console.WriteLine("Extracting file {0} file at {1} {2}%", entry.ToString(), destination.FullPathNoExtension, (i / count) * 100);
                Action = String.Format("Extracting file {0} file at {1} {2}%", entry.ToString(), destination.FullPathNoExtension, (i / count) * 100);
                try
                {
                    entry.WriteToDirectory(destination.FullPathNoExtension, new ExtractionOptions() { ExtractFullPath = true, Overwrite = destination.overwrite });
                }
                catch
                {
                    Console.WriteLine("Already Present");
                }

                //progerssVal = ((int)((++i / count) * 100));
                progress(this, new ProgressChangedEventArgs((int)((++i / count) * 100), sender));
            }
            Console.WriteLine("Done Extracting");
            Compressed.Dispose();
            complete(this, new AsyncCompletedEventArgs(null, false, sender));

            Action = "Done";

            return true;

        }


    }
}
