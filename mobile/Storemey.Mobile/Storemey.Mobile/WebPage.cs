using System;
using Xamarin.Forms;

namespace Storemey.Mobile
{
    public class WebPage : ContentPage
    {
        public WebPage()
        {
            var browser = new WebView();
            browser.Source = "https://www.storemey.com/home/mobile";
            Content = browser;
        }
    }
}

