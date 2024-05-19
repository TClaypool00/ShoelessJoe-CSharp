using ShoelessJoe.App.Models;
using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.App
{
    public static class Mapper
    {
        public static CoreUser MapUser(UserViewModel user)
        {
            return new CoreUser
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumb = user.PhoneNumb,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
