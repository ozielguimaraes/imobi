using Imobi.Validations.Interfaces;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Imobi.Validations
{
    public class PermissionValidation : IPermissionValidation
    {
        public async Task<bool> ValidateAsync(Permission permission)
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    await Application.Current.MainPage.DisplayAlert($"Permitir acesso a {permission.ToString()}", $"App precisa da {permission.ToString()}", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                status = results[permission];
            }

            return status == PermissionStatus.Granted;
        }

        public async Task<bool> ValidateCameraAccessAsync()
        {
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                {
                    await Application.Current.MainPage.DisplayAlert($"Permitir acesso a {Permission.Camera.ToString()}", $"App precisa da {Permission.Camera.ToString()}", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Camera);
                status = results[Permission.Camera];
            }

            return status == PermissionStatus.Granted;
        }
    }
}