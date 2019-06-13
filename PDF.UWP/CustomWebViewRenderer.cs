using GeoWorks.Mobile.Controls;
using GeoWorks.Mobile.UWP;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomWebView), typeof(CustomWebViewRenderer))]
namespace GeoWorks.Mobile.UWP
{
    public class CustomWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.Source = new Uri("ms-appx-web:///Assets/pdfjs/web/viewer.html");
                Control.LoadCompleted += Control_LoadCompleted;
            }
        }
        private async void Control_LoadCompleted(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            CustomWebView pdfView = Element as CustomWebView;
            if (string.IsNullOrEmpty(pdfView?.Filename)) return;
            try
            {
                var Base64Data = await OpenAndConvert(pdfView?.Filename);

                var obj = await Control.InvokeScriptAsync("openPdfAsBase64", new[] { Base64Data });
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private async Task<string> OpenAndConvert(string FileName)
        {
            var folder = ApplicationData.Current.LocalFolder;
            var file = await folder.GetFileAsync(FileName);
            var filebuffer = await file.OpenAsync(FileAccessMode.Read);
            var reader = new DataReader(filebuffer.GetInputStreamAt(0));
            var bytes = new byte[filebuffer.Size];
            await reader.LoadAsync((uint)filebuffer.Size);
            reader.ReadBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
   
}
