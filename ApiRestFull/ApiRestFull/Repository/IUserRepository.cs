using ApiRestFull.Data.VO;
using ApiRestFull.Model;

namespace ApiRestFull.Repository
{
    public interface IUserRepository
    {
        User ValidadeCredentials(UserVO user);

        User RefreshUserInfo(User user);
    }
}
