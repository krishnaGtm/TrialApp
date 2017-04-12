using TrialApp.Common;
using TrialApp.Windows;
using Windows.Security.Cryptography;
using Windows.System.Profile;

[assembly: Xamarin.Forms.Dependency(typeof(WinRTDevice))]
namespace TrialApp.Windows
{
    public class WinRTDevice : IDevice
    {
        public string GetIdentifier()
        {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            return CryptographicBuffer.EncodeToBase64String(token.Id);
        }
    }
}
