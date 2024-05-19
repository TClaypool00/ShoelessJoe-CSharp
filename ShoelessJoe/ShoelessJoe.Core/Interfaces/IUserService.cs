using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.Core.Interfaces
{
    public interface IUserService
    {
        #region Public Properties
        public string NoUsersFoundMessage { get; }
        public string UserCreatedMessage { get; }
        public string IncorrectEmailMessage { get; }
        public string IncorrectPasswordMessage { get; }

        public string UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumb { get; }
        public string IsAdmin { get; }
        #endregion

        #region Public Methods
        public Task<List<CoreUser>> GetUsersAsync();

        public Task<CoreUser> GetUserByIdAsync(int id);

        public Task<CoreUser> GetUserByEmailAsync(string email);

        public Task AddUserAsync(CoreUser user);

        public Task UpdateUserAsync(CoreUser user, int id);

        public Task<bool> UserExistsByIdAsync(int id);

        public Task<bool> UserExistsByEmailAsync(string email, int? userId = null);

        public Task<bool> UserExistsByPhoneNumbAsync(string phoneNumb, int? userId = null);

        #region Message Methods
        public string UserNotFoundMessage(int id);
        public string EmailAlreadyExistsMessage(string email);
        public string PhoneNumbExistsMessage(string phoneNumb);
        #endregion
        #endregion
    }
}
