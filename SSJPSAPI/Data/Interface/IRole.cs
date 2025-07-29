using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IRole
    {
        IEnumerable<Role> GetRoles();
        Role GetRoleById(int id);
        void AddRole(Role role);
        void UpdateRole(int id, Role role);
        void DeleteRole(int id);
    }
}
