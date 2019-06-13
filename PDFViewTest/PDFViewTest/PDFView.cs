using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PDFViewTest
{
    public class PDFView : WebView
    {
        public PDFView()
        {

        }
        public static readonly BindableProperty FileNameProperty = BindableProperty.Create(
        propertyName: "FileName",
        returnType: typeof(string),
        declaringType: typeof(PDFView),
        defaultValue: default(string));

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }
    }
}
