using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class JWTAppSettings: AppSettingsHelper
    {
        #region Public Properties
        public string RefreshTokenNotFoundMessage { get; }
        public string RefreshTokenExpiredMessage { get; }
        public string JWTSubject { get; }
        public string JWTKey { get; }
        #endregion

        #region Constructors
        public JWTAppSettings(IConfiguration configuration): base(configuration, Tables.JWT)
        {
            RefreshTokenNotFoundMessage = GetErrorConfiguration("NotFound");
            RefreshTokenExpiredMessage = GetErrorConfiguration("Expired");

            JWTKey = GetConfiguration("Key");
            JWTSubject = GetConfiguration("Subject");
        }
        #endregion
    }
}
