using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace Imobi.Validations.Interfaces
{
    public interface IPermissionValidation
    {
        Task<bool> ValidateAsync(Permission permission);

        Task<bool> ValidateCameraAccessAsync();
    }
}