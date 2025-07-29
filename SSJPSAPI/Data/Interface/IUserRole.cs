using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IUserRole
    {
        List<UserRoleDto> GetAllUserRoles();
        UserRole? GetUserRoleById(int id);
        void CreateUserRole(UserRole userRole);
        void UpdateUserRole(UserRole userRole);
        void DeleteUserRole(int id);
    }
}
