using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.Core.Interfaces
{
    public interface ICommentService
    {
        #region Public Properties
        public string NoCommentsFoundMessage { get; }
        public string CommentDeletedOKMessage { get; }
        #endregion

        #region Public Methods
        public Task<List<CoreComment>> GetCommentsAsync(int? potentialBuyId = null, int? shoeId = null, int? userId = null, int? index = null);

        public Task<CoreComment> GetCommentAsync(int id);

        public Task<CoreComment> AddCommentAsync(CoreComment comment);

        public Task<CoreComment> UpdateCommentAsync(CoreComment comment);

        public Task<bool> CommentExistsByIdAsync(int id);

        public Task<bool> CommentOwnedByUserAsync(int id, int userId);

        public Task DeleteCommentAsync(int id);

        #region Message Methods
        public string CommentNotfoundMessage(int id);
        #endregion
        #endregion
    }
}
