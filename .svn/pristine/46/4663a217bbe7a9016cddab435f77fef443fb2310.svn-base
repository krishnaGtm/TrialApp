using TrialApp.Common;
using TrialApp.WinPhone;
using Windows.Security.Cryptography;
using Windows.System.Profile;

[assembly: Xamarin.Forms.Dependency(typeof(WinPhoneDevice))]
namespace TrialApp.WinPhone
{
    public class WinPhoneDevice : IDevice
    {
        public string GetIdentifier()
        {
            var token = HardwareIdentification.GetPackageSpecificToken(null);
            return CryptographicBuffer.EncodeToBase64String(token.Id);
        }
    }
}
