using Storemey.Mobile.UWP;
using Xamarin.Forms;

[assembly: Dependency(typeof(BaseUrl))]
namespace Storemey.Mobile.UWP
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///";
        }
    }
}
