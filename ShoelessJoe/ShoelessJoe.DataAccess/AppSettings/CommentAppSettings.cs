using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public class CommentAppSettings : AppSettingsHelper
    {
        #region Public Properties
        public string NoCommentFoundMessage { get; }
        public string CommentDeletedOKMessage { get; }
        #endregion

        #region Constructors
        public CommentAppSettings(IConfiguration configuration) : base(configuration, Tables.Comment)
        {
            NoCommentFoundMessage = GetErrorConfiguration("NoCommentsFound");
            CommentDeletedOKMessage = GetOKMessagesConfiguration("Deleted");
        }
        #endregion
    }
}
