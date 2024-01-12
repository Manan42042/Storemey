using System;
using Xamarin.Forms;

namespace Storemey.Mobile
{
    public class WebPage : ContentPage
    {
        public WebPage()
        {
            var browser = new WebView();
            browser.Source = "http://xamarin.com";
            Content = browser;
        }
    }
}

