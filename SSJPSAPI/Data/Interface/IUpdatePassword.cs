using SSJPSAPI.DTO;

namespace SSJPSAPI.Data.Interface
{
    public interface IUpdatePassword
    {
        bool ChangePassword(ChangePasswordDto model, out string message);
    }
}
