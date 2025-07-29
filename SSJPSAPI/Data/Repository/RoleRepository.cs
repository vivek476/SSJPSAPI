using SSJPSAPI.Data.Interface;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Repository
{
    public class RoleRepository : IRole
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public Role GetRoleById(int id)
        {
            return _context.Roles.Find(id);
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }

        public void UpdateRole(int id, Role role)
        {
            var existing = _context.Roles.Find(id);
            if (existing != null)
            {
                existing.Name = role.Name;
                _context.SaveChanges();
            }
        }

        public void DeleteRole(int id)
        {
            var role = _context.Roles.Find(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

    }
}
