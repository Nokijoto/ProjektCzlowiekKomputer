using Project.CrossCutting.Common;
using Project.CrossCutting.Dtos;
using Project.Data.Models;

namespace ProjektCzlowiekKomputer.Interfaces
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(bool Succeeded, string NewPassword)> ResetPasswordAsync(ResetPasswordModel model);
        Task <CrudOperationResult<UpdateProfileModel>> UpdateProfile(UpdateProfileModel model,Guid guid);
        Task<(int, string)> Login(LoginModel model);
    }
}
