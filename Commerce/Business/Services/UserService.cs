using Commerce.Exceptions;
using Interview.DataAccess;
using System.Linq;

namespace Commerce.Business.Services
{
    public class UserService : IUserService
    {
        private readonly DbContext dbContext;

        public UserService(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public string GetUserInfo(int userId)
        {
            var user = dbContext.Users.FirstOrDefault(it => it.Id == userId);
            if (user == null)
            {
                throw new NotFoundException();
            }

            return $"{user.Name} {user.Surname} (id {user.Id}).";
        }
    }
}