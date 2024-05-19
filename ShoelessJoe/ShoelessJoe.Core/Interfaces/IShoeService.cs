using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.Core.Interfaces
{
    public interface IShoeService
    {
        #region Public Properites
        public string NoShoesFoundMessage { get; }
        public string NoPicturesMessage { get; }
        public string CannotBuyOwnShoeMessage { get; }
        public string ShoeAlreadySoldMessage { get; }
        public string BothSizesAreNullMessage { get; }

        public string ShoeAddedMessage { get; }
        public string ShoeUpdatedMessage { get; }
        public string ShoeSoldMessage { get; }

        public string RigthSize { get; }
        public string LeftSize { get; }
        #endregion

        #region Public Methods
        public Task<List<CoreShoe>> GetShoesAsync(int? ownerId = null, int? soldToId = null, DateTime? datePosted = null, bool? isSold = null, int? index = null);

        public Task<CoreShoe> GetShoesAsync(int id);

        public Task AddShoeAsync(CoreShoe shoe);

        public Task<CoreShoe> UpdateShoeAsync(CoreShoe shoe, int id);

        public Task<bool> ShoeExistsById(int id);

        public Task<bool> ShoeIsOwnedByUserAsync(int id, int owner);

        #region Message Methods
        public string ShoeNotFoundMessage(int id);
        #endregion
        #endregion
    }
}
