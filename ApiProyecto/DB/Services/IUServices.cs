using DB.Request;
using DB.Response;

namespace DB.Services
{
    public interface IUServices
    {
        UserResponse Auth(AuthRequest model);
    }
}
