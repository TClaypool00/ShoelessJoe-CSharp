using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class ModelAppSettings : AppSettingsHelper
    {
        #region Public Properties
        public string NoModelsFoundMessage { get; }
        #endregion

        #region Constructors
        public ModelAppSettings(IConfiguration configuration) : base(configuration, Tables.Model)
        {
            NoModelsFoundMessage = GetErrorConfiguration("ModelsNotFound");
        }
        #endregion
    }
}
