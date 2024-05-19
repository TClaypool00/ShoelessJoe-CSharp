using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class UserAppSettings : AppSettingsHelper
    {
        #region Users Message Proprties
        public string NoUsersFoundMessage { get; }
        public string WithPhoneNum { get; }
        public string WithEmail { get; }
        public string UserCreatedMessage { get; }
        public string IncorrectEmail { get; }
        public string IncorrectPassword { get; }
        #endregion

        #region Users Proprties
        public string UserId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumb { get; }
        public string IsAdmin { get; }
        #endregion

        #region Constructors
        public UserAppSettings(IConfiguration configuration): base(configuration, Tables.User)
        {

            NoUsersFoundMessage = GetErrorConfiguration("NoUsersFound");
            WithPhoneNum = GetErrorConfiguration("PhoneNum");
            WithEmail = GetErrorConfiguration("Email");
            IncorrectEmail = GetErrorConfiguration("IncorrectdEmail");
            IncorrectPassword = GetErrorConfiguration("IncorrectPassword");

            UserCreatedMessage = GetOKMessagesConfiguration("Created");

            UserId = GetConfiguration("UserId");
            FirstName = GetConfiguration("FirstName");
            LastName = GetConfiguration("LastName");
            Email = GetConfiguration("Email");
            PhoneNumb = GetConfiguration("PhoneNum");
            IsAdmin = GetConfiguration("IsAdmin");
        }
        #endregion
    }
}
