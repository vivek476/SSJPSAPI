using SSJPSAPI.Data.Interface;
using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Repository
{
    public class UpdatePasswordRepository : IUpdatePassword
    {
        private readonly ApplicationDbContext _context;

        public UpdatePasswordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ChangePassword(ChangePasswordDto model, out string message)
        {
            message = string.Empty;

            if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.OldPassword) || string.IsNullOrWhiteSpace(model.NewPassword))
            {
                message = "All fields are required.";
                return false;
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            if (user == null)
            {
                message = "User not found.";
                return false;
            }

            if (user.Password != model.OldPassword)
            {
                message = "Incorrect old password.";
                return false;
            }

            user.Password = model.NewPassword;
            _context.SaveChanges();
            message = "Password updated successfully.";
            return true;
        }
    }
}
