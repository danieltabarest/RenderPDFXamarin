using WorkingWithWebview.UWP;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage;
using Windows.Storage.AccessCache;


[assembly: Dependency(typeof(BaseUrl))]
namespace WorkingWithWebview.UWP
{
    public class BaseUrl //: IBaseUrl
    {
        public async Task<string> Gets()
        {
            // return "ms-appx-web:///";
            await GetMyPDFFilesFolder();
            return FilePath;
        }

        public static string NewPDFFilePath = "";
        public static string FilePath = "";


        async static public Task GetMyPDFFilesFolder()
        {

            StorageFolder newPDFFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("PDFFiles",
                            CreationCollisionOption.OpenIfExists);

            StorageFolder assertMarks = await Package.Current.InstalledLocation.GetFolderAsync(@"Assets");

            IReadOnlyList<StorageFile> files = await assertMarks.GetFilesAsync();

            foreach (StorageFile file in files)
            {
                string ext = System.IO.Path.GetExtension(file.Name).ToLower();
                if (ext == ".pdf")
                {
                    FilePath = file.Path;
                    await file.CopyAsync(newPDFFolder, file.Name, NameCollisionOption.ReplaceExisting);
                }
            }

            NewPDFFilePath = newPDFFolder.Path;
        }


    }
}
