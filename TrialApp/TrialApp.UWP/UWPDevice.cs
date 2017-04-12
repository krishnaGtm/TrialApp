using TrialApp.Common;
using TrialApp.UWP;

[assembly: Xamarin.Forms.Dependency(typeof(UWPDevice))]
namespace TrialApp.UWP
{
    public class UWPDevice : IDevice
    {
        public string GetIdentifier()
        {
            var token = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);
            return Windows.Security.Cryptography.CryptographicBuffer.EncodeToBase64String(token.Id);
        }
    }
}
