using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;

namespace ShoelessJoe.DataAccess.Services
{
    public class UserService : ServiceHelper, IUserService
    {
        #region Private Fields
        private readonly UserAppSettings _userAppSettings;
        #endregion

        #region Public Properties
        public string NoUsersFoundMessage => _userAppSettings.NoUsersFoundMessage;

        public string UserCreatedMessage => _userAppSettings.UserCreatedMessage;

        public string IncorrectEmailMessage => _userAppSettings.IncorrectEmail;

        public string IncorrectPasswordMessage => _userAppSettings.IncorrectPassword;

        public string UserId => _userAppSettings.UserId;

        public string FirstName => _userAppSettings.FirstName;

        public string LastName => _userAppSettings.LastName;

        public string Email => _userAppSettings.Email;

        public string PhoneNumb => _userAppSettings.PhoneNumb;

        public string IsAdmin => _userAppSettings.IsAdmin;
        #endregion

        #region Constructors
        public UserService(ShoelessJoeContext context, IConfiguration configuration) : base(context, configuration)
        {
            _userAppSettings = new UserAppSettings(_configuration);
        }
        #endregion

        #region Public Methods
        public async Task AddUserAsync(CoreUser user)
        {
            await _context.Users.AddAsync(Mapper.MapUser(user));
            await SaveAsync();
        }

        public async Task<CoreUser> GetUserByEmailAsync(string email)
        {
            var dataUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return Mapper.MapUser(dataUser, true);
        }

        public async Task<CoreUser> GetUserByIdAsync(int id)
        {
            var dataUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            return Mapper.MapUser(dataUser);
        }

        public async Task<List<CoreUser>> GetUsersAsync()
        {
            var dataUsers = await _context.Users.ToListAsync();
            var coreUsers = new List<CoreUser>();

            for (int i = 0; i < dataUsers.Count; i++)
            {
                coreUsers.Add(Mapper.MapUser(dataUsers[i]));
            }

            return coreUsers;

        }

        public async Task UpdateUserAsync(CoreUser user, int id)
        {
            var dataUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            var userToUpdate = Mapper.MapUser(user);

            userToUpdate.Password = dataUser.Password;
            userToUpdate.IsAdmin = dataUser.IsAdmin;

            _context.Entry(dataUser).CurrentValues.SetValues(userToUpdate);

            await SaveAsync();
        }

        public Task<bool> UserExistsByEmailAsync(string email, int? userId = null)
        {
            if (userId is null)
            {
                return _context.Users.AnyAsync(u => u.Email == email);
            }

            return _context.Users.AnyAsync(u => u.Email == email && u.UserId != userId);
        }

        public Task<bool> UserExistsByIdAsync(int id)
        {
            return _context.Users.AnyAsync(u => u.UserId == id);
        }

        public Task<bool> UserExistsByPhoneNumbAsync(string phoneNumb, int? userId = null)
        {
            if (userId is null)
            {
                return _context.Users.AnyAsync(u => u.PhoneNumb == phoneNumb);
            }

            return _context.Users.AnyAsync(u => u.PhoneNumb == phoneNumb && u.UserId != userId);
        }
        #endregion

        #region Message Methods
        public string UserNotFoundMessage(int id)
        {
            return $"{_userAppSettings.TableName} {_globalSettings.WithId}{id}{_globalSettings.DoesNotExists}";
        }

        public string EmailAlreadyExistsMessage(string email)
        {
            return $"{_userAppSettings.TableName} {_userAppSettings.WithEmail}{email}{_globalSettings.AlreadyExists}";
        }

        public string PhoneNumbExistsMessage(string phoneNumb)
        {
            return $"{_userAppSettings.TableName} {_userAppSettings.WithPhoneNum}{phoneNumb}{_globalSettings.AlreadyExists}";
        }
        #endregion
    }
}
