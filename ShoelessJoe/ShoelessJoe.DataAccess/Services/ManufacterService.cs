using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;

namespace ShoelessJoe.DataAccess.Services
{
    public class ManufacterService : ServiceHelper, IManufacterService
    {
        #region Private Fields
        private readonly ManufacturerAppSettings _manufacturerAppSettings;
        #endregion

        #region Public Properties
        public string NoManufactersFoundMessage => _manufacturerAppSettings.NoManufacturersFoundMessage;

        public string ManufacterDeletedOKMessage => _manufacturerAppSettings.ManufacturerDeletedMessage;
        #endregion

        #region Constructors
        public ManufacterService(ShoelessJoeContext context, IConfiguration configuration) : base(context, configuration)
        {
            _manufacturerAppSettings = new ManufacturerAppSettings(_configuration);
        }
        #endregion

        #region Public Methods
        public async Task<CoreManufacter> AddManufacterAsync(CoreManufacter manufacter)
        {
            var dataManufacter = Mapper.MapManufacter(manufacter);

            await _context.Manufacters.AddAsync(dataManufacter);

            await SaveAsync();

            manufacter.ManufacterId = dataManufacter.ManufacterId;

            return manufacter;
        }

        public async Task DeleteManufacterAsync(int id)
        {
            _context.Manufacters.Remove(await _context.Manufacters.FindAsync(id));

            await SaveAsync();
        }

        public async Task<CoreManufacter> GetGetManufacterAsync(int id)
        {
            return Mapper.MapManufacter(await GetDataManufacter(id));
        }

        public async Task<List<CoreManufacter>> GetManufactersAsync(int? userId = null, int? index = null)
        {
            index ??= 1;
            int skipNumber = (index > 1) ? (int)index : 0;

            List<Manufacter> manufacters;
            var coreManufacters = new List<CoreManufacter>();

            if (userId is null)
            {
                manufacters = await _context.Manufacters.Select(m => new Manufacter
                {
                    ManufacterId = m.ManufacterId,
                    ManufacterName = m.ManufacterName,
                    User = new User
                    {
                        UserId = m.User.UserId,
                        FirstName = m.User.FirstName,
                        LastName = m.User.LastName
                    }
                })
                .Take(10)
                .Skip(skipNumber)
                .ToListAsync();
            }
            else
            {
                manufacters = await _context.Manufacters.Select(m => new Manufacter
                {
                    ManufacterId = m.ManufacterId,
                    ManufacterName = m.ManufacterName,
                    User = new User
                    {
                        UserId = m.User.UserId,
                        FirstName = m.User.FirstName,
                        LastName = m.User.LastName
                    }
                })
                .Take(10)
                .Skip(skipNumber)
                .Where(m => m.ManufacterId == userId)
                .ToListAsync();
            }

            if (manufacters.Count > 0)
            {
                for (int i = 0; i < manufacters.Count; i++)
                {
                    coreManufacters.Add(Mapper.MapManufacter(manufacters[i]));
                }
            }

            return coreManufacters;
        }

        public Task<bool> ManufacterExistsById(int id)
        {
            return _context.Manufacters.AnyAsync(m => m.ManufacterId == id);
        }

        public async Task<CoreManufacter> UpdateManufacter(CoreManufacter manufacter, int id)
        {
            var dataManufacter = await GetDataManufacter(id);

            dataManufacter.ManufacterName = manufacter.ManufacterName;

            _context.Manufacters.Update(dataManufacter);

            await SaveAsync();

            manufacter.ManufacterId = id;
            manufacter.User = Mapper.MapUser(dataManufacter.User);

            return manufacter;
        }

        public Task<bool> ManufacterExistByName(string name, int userId)
        {
            return _context.Manufacters.AnyAsync(m => m.ManufacterName == name && m.UserId == userId);
        }

        public async Task<List<SelectListItem>> GetCoreManufacterDropDown(int userId, int? index = null)
        {
            index ??= 1;
            int skipNumber = (index > 1) ? (int)index : 0;

            var dropDowns = new List<SelectListItem>
            {
                AddDefaultValue()
            };

            var manufacters = await _context.Manufacters.Select(m => new Manufacter
            {
                ManufacterId = m.ManufacterId,
                ManufacterName = m.ManufacterName,
                UserId = m.UserId
            })
            .Where(u => u.UserId == userId)
            .Take(10)
            .Skip(skipNumber)
            .ToListAsync();

            if (manufacters.Count > 0)
            {
                for (int i = 0; i < manufacters.Count; i++)
                {
                    dropDowns.Add(Mapper.MapDropDown(manufacters[i]));
                }
            }

            return dropDowns;
        }

        public Task<bool> ManufacterExistsByUserId(int id, int userId)
        {
            return _context.Manufacters.AnyAsync(m => m.ManufacterId == id && m.UserId == userId);
        }
        #endregion

        #region Private Methods
        private Task<Manufacter> GetDataManufacter(int id)
        {
            return _context.Manufacters
                .Select(m => new Manufacter
                {
                    ManufacterId = m.ManufacterId,
                    ManufacterName = m.ManufacterName,
                    User = new User
                    {
                        UserId = m.User.UserId,
                        FirstName = m.User.FirstName,
                        LastName = m.User.LastName
                    }
                })
                .FirstOrDefaultAsync(m => m.ManufacterId == id);
        }

        #region Message Methods
        public string ManufacterNotFoundMessage(int id)
        {
            return $"{_manufacturerAppSettings.TableName} {_globalSettings.WithId} {id} {_globalSettings.DoesNotExists}";
        }

        public string ManufacterNameExists(string name)
        {
            return $"{_manufacturerAppSettings.TableName} {_globalSettings.WithName} {name} {_globalSettings.AlreadyExists}";
        }
        #endregion
        #endregion
    }
}
