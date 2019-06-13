using Xamarin.Forms;

namespace DisplayPDF
{
	public class WebViewPageCS : ContentPage
	{
		public WebViewPageCS ()
		{
			Padding = new Thickness (0, 20, 0, 0);
			Content = new StackLayout { 
				Children = {
					new CustomWebView {
						Uri = "pdffile.pdf",
						HorizontalOptions = LayoutOptions.FillAndExpand,
						VerticalOptions = LayoutOptions.FillAndExpand
					}
				}
			};
		}
	}
}
