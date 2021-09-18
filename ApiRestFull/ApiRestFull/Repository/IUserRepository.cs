using ApiRestFull.Data.VO;
using ApiRestFull.Model;

namespace ApiRestFull.Repository
{
    public interface IUserRepository
    {
        User ValidadeCredentials(UserVO user);
        User ValidadeCredentials(string username);
        User RefreshUserInfo(User user);

        bool RevokeToken(string userName);
    }

}
