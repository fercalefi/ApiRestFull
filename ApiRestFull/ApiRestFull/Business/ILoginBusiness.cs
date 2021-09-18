using ApiRestFull.Data.VO;

namespace ApiRestFull.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
        
        // refresh token
        TokenVO ValidateCredentials(TokenVO token);

        bool RevokeToken(string userName);
    }
}
