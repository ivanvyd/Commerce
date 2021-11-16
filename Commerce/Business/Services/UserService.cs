using Commerce.Exceptions;
using Interview.DataAccess;
using System.Linq;

namespace Commerce.Business.Services
{
    public class UserService : IUserService
    {
        public string GetUserInfo(int userId)
        {
            var user = DbContext.Users.FirstOrDefault(it => it.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return $"{user.Name} {user.Surname} (id {user.Id}).";
        }
    }
}