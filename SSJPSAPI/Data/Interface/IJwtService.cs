using SSJPSAPI.Model;

namespace SSJPSAPI.Data.Interface
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
