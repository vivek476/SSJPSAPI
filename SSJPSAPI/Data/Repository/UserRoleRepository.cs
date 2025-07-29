using Microsoft.EntityFrameworkCore;
using SSJPSAPI.Data.Interface;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Repository
{
    public class UserRoleRepository : IUserRole
    {
        private readonly ApplicationDbContext _context;

        public UserRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UserRoleDto> GetAllUserRoles()
        {
            return (from ur in _context.UserRoles
                    join u in _context.Users on ur.UserId equals u.Id
                    join r in _context.Roles on ur.RoleId equals r.Id
                    select new UserRoleDto
                    {
                        Id = ur.Id,
                        UserId = ur.UserId,
                        FullName = u.FullName,
                        RoleId = ur.RoleId,
                        RoleName = r.Name
                    }).ToList();
        }

        public UserRole? GetUserRoleById(int id)
        {
            return _context.UserRoles.FirstOrDefault(ur => ur.Id == id);
        }

        public void CreateUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }

        public void UpdateUserRole(UserRole userRole)
        {
            var trackedEntity = _context.UserRoles.Local.FirstOrDefault(e => e.Id == userRole.Id);
            if (trackedEntity != null)
            {
                _context.Entry(trackedEntity).State = EntityState.Detached;
            }

            _context.UserRoles.Update(userRole);
            _context.SaveChanges();
        }

        public void DeleteUserRole(int id)
        {
            var userRole = _context.UserRoles.Find(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                _context.SaveChanges();
            }
        }

    }
}
