
using TrialApp.Common;
using TrialApp.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(iOSDevice))]
namespace TrialApp.iOS
{
    public class iOSDevice : IDevice
    {
        public string GetIdentifier()
        {
            return "iOSDevice";
        }
    }
}
