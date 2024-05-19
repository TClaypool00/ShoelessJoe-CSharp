using Microsoft.Extensions.Configuration;

namespace ShoelessJoe.DataAccess.AppSettings
{
    public abstract class AppSettingsHelper
    {
        #region Private Fields
        private readonly IConfiguration _configuration;
        private readonly Tables _tables;
        private readonly string _tableName;
        #endregion

        #region Constructors
        protected AppSettingsHelper(IConfiguration configuration, Tables tables)
        {
            _configuration = configuration;
            _tables = tables;

            _tableName = _tables.ToString();
            TableName = _tableName;
        }
        #endregion

        #region Public Properties
        public string TableName { get; }
        #endregion

        #region Protected Methods
        protected string GetConfiguration(string key)
        {
            return _configuration[$"{_tableName}:{key}"];
        }

        protected string GetErrorConfiguration(string key)
        {
            return GetConfiguration($"Errors:{key}");
        }

        protected string GetOKMessagesConfiguration(string key)
        {
            return GetConfiguration($"OKMessages:{key}");
        }
        #endregion

        #region Enums
        public enum Tables
        {
            User,
            Global,
            JWT,
            Comment,
            Manufacturer,
            Model,
            PotentialBuy,
            Shoe
        }
        #endregion
    }
}
