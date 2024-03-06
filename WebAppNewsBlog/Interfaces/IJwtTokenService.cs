using WebAppNewsBlog.Data.Entities.Identity;

namespace WebAppNewsBlog.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(UserEntity user, IList<string> roles);
    }
}
