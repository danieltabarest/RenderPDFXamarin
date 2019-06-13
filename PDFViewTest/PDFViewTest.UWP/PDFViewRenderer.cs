using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.UWP;
using Xamarin.Forms;
using PDFViewTest;
using Windows.Storage;
using Windows.Storage.Streams;
using PDFViewTest.UWP;

[assembly: ExportRenderer(typeof(PDFView), typeof(PDFViewRenderer))]

namespace PDFViewTest.UWP
{
    public class PDFViewRenderer :WebViewRenderer
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
            PDFView pdfView = Element as PDFView;
            if (string.IsNullOrEmpty(pdfView?.FileName)) return;
            try
            {
                var Base64Data = await OpenAndConvert(pdfView?.FileName);

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
