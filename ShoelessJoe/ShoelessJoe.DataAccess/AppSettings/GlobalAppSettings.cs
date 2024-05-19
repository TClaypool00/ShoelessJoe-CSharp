using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class GlobalAppSettings : AppSettingsHelper
    {
        #region Public Properties
        public string DoesNotExists { get; }
        public string AlreadyExists { get; }
        public string WithId { get; }
        public string WithName { get; }
        public string DefaultSelectText { get; }
        #endregion

        #region Constructors
        public GlobalAppSettings(IConfiguration configuration): base(configuration, Tables.Global)
        {
            DoesNotExists = GetErrorConfiguration("DoesNotExists");
            AlreadyExists = GetErrorConfiguration("AlreadyExists");
            WithId = GetErrorConfiguration("Id");
            DefaultSelectText = GetConfiguration("DefaultSelectText");
            WithName = GetErrorConfiguration("WithName");
        }
        #endregion
    }
}
