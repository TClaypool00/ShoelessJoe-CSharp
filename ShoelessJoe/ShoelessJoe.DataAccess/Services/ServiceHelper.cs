using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;

namespace ShoelessJoe.DataAccess.Services
{
    public class ServiceHelper
    {
        #region Private Fields
        protected readonly IConfiguration _configuration;
        #endregion

        #region Protected Fields
        protected int _index;
        protected string _idErrorMessage = "Id cannot be 0";
        protected readonly ShoelessJoeContext _context;
        protected readonly GlobalAppSettings _globalSettings;
        #endregion

        #region Constructors
        public ServiceHelper(ShoelessJoeContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
            _globalSettings = new GlobalAppSettings(_configuration);
        }
        #endregion

        #region Protected Methods
        protected void ConfigureIndex(int? index)
        {
            if (index is null || (int)index == 1)
            {
                _index = 0;
            }
            else
            {
                _index = (int)index * 10;
            }
        }

        protected async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected SelectListItem AddDefaultValue()
        {
            return new SelectListItem
            {
                Value = "",
                Text = "Please select a value",
                Selected = true
            };
        }
        #endregion
    }
}
