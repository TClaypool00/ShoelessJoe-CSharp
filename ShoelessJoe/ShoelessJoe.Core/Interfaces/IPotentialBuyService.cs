using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.Core.Interfaces
{
    public interface IPotentialBuyService
    {
        #region Public Properites
        public string NoPotentialBuysFoundMessage { get; }
        public string PotentialBuyAlreadyExistsMessage { get; }
        public string PotentialBuyDeletedMessage { get; }
        #endregion

        #region Public Methods
        public Task<List<CorePotentialBuy>> GetPotentialBuysAsync(int? userId = null, int? shoeId = null, bool? isSold = null, DateTime? dateSold = null, int? index = null);

        public Task<CorePotentialBuy> GetPotentialBuyByIdAsync(int id);

        public Task<CorePotentialBuy> AddPotentialBuyAsync(CorePotentialBuy potentialBuy);

        public Task<bool> PotentialBuyExistsByIdAsync(int id);

        public Task<bool> PotentialBuyExistsByUserIdAsync(int userId, int shoeId);

        public Task<bool> UserHasAccessToPotentialBuy(int userId, int id);

        public Task SellShoeAsync(int id, int userId);

        public bool IsShoeSoldAsync(int shoeId, int userId);

        public Task<bool> IsShoeSoldByCommentId(int commentId, int userId);

        public Task<bool> IsShoeSoldByPotentialBuyId(int id);

        public Task DeletePotentialBuyById(int id);

        #region Message Methods
        public string PotentialBuyNotFoundMessage(int id);
        #endregion
        #endregion
    }
}
