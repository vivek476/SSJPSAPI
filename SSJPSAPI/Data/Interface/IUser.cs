using SSJPSAPI.DTO;
using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IUser
    {
        IEnumerable<User> GetUser();
        User GetUserById(int id);
        void UpdateUser(int id, User user);
        string Signup(SignupRequest request);
        (string Token, User User, Role Role)? Login(LoginRequest request);
        void DeleteUser(int id);
    }
}
