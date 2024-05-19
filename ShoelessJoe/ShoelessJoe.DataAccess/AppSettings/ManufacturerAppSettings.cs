using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class ManufacturerAppSettings : AppSettingsHelper
    {
        #region Constructors
        public ManufacturerAppSettings(IConfiguration configuration) : base(configuration, Tables.Manufacturer)
        {
            NoManufacturersFoundMessage = GetErrorConfiguration("NomanufacturerFound");
            ManufacturerDeletedMessage = GetOKMessagesConfiguration("Deleted");
        }
        #endregion

        #region Public Properties
        public string NoManufacturersFoundMessage { get; }
        public string ManufacturerDeletedMessage { get; }
        #endregion
    }
}
