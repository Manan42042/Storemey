using System;
using Xamarin.Forms;
using Storemey.Mobile.Android;

[assembly: Dependency (typeof (BaseUrl_Android))]
namespace Storemey.Mobile.Android 
{
	public class BaseUrl_Android : IBaseUrl 
	{
		public string Get () 
		{
			return "file:///android_asset/";
		}
	}
}