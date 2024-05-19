using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class PotentialBuyAppSettings : AppSettingsHelper
    {
        #region Public Properties
        public string NoPotentialBuysFoundMessage { get; }
        public string PotentialBuyAlreadyExistsMessage { get; }
        public string PotentialBuyDeletedMessage { get; }
        #endregion

        #region Constructors
        public PotentialBuyAppSettings(IConfiguration configuration) : base(configuration, Tables.PotentialBuy)
        {
            NoPotentialBuysFoundMessage = GetErrorConfiguration("PotentialBuysNotFound");
            PotentialBuyAlreadyExistsMessage = GetErrorConfiguration("AlreadyExists");
            PotentialBuyDeletedMessage = GetOKMessagesConfiguration("Deleted");
        }
        #endregion
    }
}
