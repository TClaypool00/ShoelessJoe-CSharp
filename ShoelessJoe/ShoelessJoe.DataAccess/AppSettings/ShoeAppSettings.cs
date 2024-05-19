using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class ShoeAppSettings : AppSettingsHelper
    {
        #region Public Proprties
        public string NoShoesFoundMessage { get; }
        public string NoPicturesMessage { get; }
        public string CannotBuyOwnShoeMessage { get; }
        public string ShoeAlreadySoldMessage { get; }
        public string BothSizesAreNullMessage { get; }

        public string ShoeAddedMessage { get; }
        public string ShoeUpdatedMessage { get; }
        public string ShoeSoldMessage { get; }

        public string RightSize { get; }
        public string LeftSize { get; }
        #endregion

        #region Constructors
        public ShoeAppSettings(IConfiguration configuration) : base(configuration, Tables.Shoe)
        {
            NoShoesFoundMessage = GetErrorConfiguration("NoShoesFound");
            NoPicturesMessage = GetErrorConfiguration("NoPictures");
            CannotBuyOwnShoeMessage = GetErrorConfiguration("CannotBuyOwnShoe");
            ShoeAlreadySoldMessage = GetErrorConfiguration("AlreadySold");
            BothSizesAreNullMessage = GetErrorConfiguration("BothSizesAreNull");

            ShoeAddedMessage = GetOKMessagesConfiguration("Added");
            ShoeUpdatedMessage = GetOKMessagesConfiguration("Updated");
            ShoeSoldMessage = GetOKMessagesConfiguration("Sold");

            RightSize = GetConfiguration("RightSize");
            LeftSize = GetConfiguration("LeftSize");
        }
        #endregion
    }
}
