using ApiRestFull.Data.VO;

namespace ApiRestFull.Business
{
    public interface ILoginBusiness
    {
        TokenVO ValidateCredentials(UserVO user);
    }
}
