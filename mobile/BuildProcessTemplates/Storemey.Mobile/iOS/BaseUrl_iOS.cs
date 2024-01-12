using Xamarin.Forms;
using Storemey.Mobile.iOS;
using Foundation;

[assembly: Dependency (typeof (BaseUrl_iOS))]

namespace Storemey.Mobile.iOS 
{
	public class BaseUrl_iOS : IBaseUrl 
	{
		public string Get () 
		{
			return NSBundle.MainBundle.BundlePath;
		}
	}
}