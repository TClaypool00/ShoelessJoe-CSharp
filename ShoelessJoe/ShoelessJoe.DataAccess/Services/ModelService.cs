using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;

namespace ShoelessJoe.DataAccess.Services
{
    public class ModelService : ServiceHelper, IModelService
    {
        #region Private Fields
        private readonly ModelAppSettings _modelAppSettings;
        #endregion

        #region Constructors
        public ModelService(ShoelessJoeContext context, IConfiguration configuration) : base(context, configuration)
        {
            _modelAppSettings = new ModelAppSettings(_configuration);
        }
        #endregion

        #region Public Properties
        public string NoModelsFoundMessage => _modelAppSettings.NoModelsFoundMessage;
        #endregion

        #region Public Methods
        public async Task<CoreModel> AddModelAsync(CoreModel model)
        {
            var dataModel = Mapper.MapModel(model);

            await _context.Models.AddAsync(dataModel);

            await SaveAsync();

            model.ModelId = dataModel.ModelId;

            return model;
        }

        public async Task DeleteModelAsync(int id)
        {
            var model = await _context.Models.FindAsync(id);

            _context.Models.Remove(model);

            await SaveAsync();
        }

        public async Task<CoreModel> GetModelAsync(int id)
        {
            var dataModel = await _context.Models
                .Select(m => new Model
                {
                    ModelId = m.ModelId,
                    ModelName = m.ModelName,
                    Manufacter = new Manufacter
                    {
                        ManufacterId = m.Manufacter.ManufacterId,
                        ManufacterName = m.Manufacter.ManufacterName,
                        User = new User
                        {
                            UserId = m.Manufacter.UserId,
                            FirstName = m.Manufacter.User.FirstName,
                            LastName = m.Manufacter.User.LastName
                        }
                    }
                })
                .FirstOrDefaultAsync(a => a.ModelId == id);

            return Mapper.MapModel(dataModel);
        }

        public async Task<List<SelectListItem>> GetModelDropDown(int userId, int? index = null)
        {
            ConfigureIndex(index);
            var dropDowns = new List<SelectListItem>();

            var models = await _context.Models
                .Where(u => u.Manufacter.UserId == userId)
                .Select(m => new Model
                {
                    ModelId = m.ModelId,
                    ModelName = m.ModelName,
                    Manufacter = new Manufacter
                    {
                        UserId = m.Manufacter.UserId
                    }
                })
                .Take(10)
                .Skip(_index)
                .ToListAsync();

            dropDowns.Add(AddDefaultValue());

            if (models.Count > 0)
            {
                for (int i = 0; i < models.Count; i++)
                {
                    dropDowns.Add(Mapper.MapDropDown(models[i]));
                }
            }

            return dropDowns;
        }

        public async Task<List<CoreModel>> GetModelsAsync(int? userId = null, int? index = null)
        {
            ConfigureIndex(index);

            List<Model> models;
            var coreModels = new List<CoreModel>();

            if (userId != null)
            {
                models = await _context.Models
                    .Select(m => new Model
                    {
                        ModelId = m.ModelId,
                        ModelName = m.ModelName,
                        Manufacter = new Manufacter
                        {
                            ManufacterId = m.Manufacter.ManufacterId,
                            ManufacterName = m.Manufacter.ManufacterName,
                            User = new User
                            {
                                UserId = m.Manufacter.User.UserId,
                                FirstName = m.Manufacter.User.FirstName,
                                LastName = m.Manufacter.User.LastName
                            }
                        }
                    })
                    .Take(10)
                    .Skip(_index)
                    .Where(x => x.Manufacter.User.UserId == (int)userId)
                    .ToListAsync();
            }
            else
            {
                models = await _context.Models
                    .Select(m => new Model
                    {
                        ModelId = m.ModelId,
                        ModelName = m.ModelName,
                        Manufacter = new Manufacter
                        {
                            ManufacterId = m.Manufacter.ManufacterId,
                            ManufacterName = m.Manufacter.ManufacterName,
                            User = new User
                            {
                                UserId = m.Manufacter.User.UserId,
                                FirstName = m.Manufacter.User.FirstName,
                                LastName = m.Manufacter.User.LastName
                            }
                        }
                    })
                    .Take(10)
                    .Skip(_index)
                    .ToListAsync();
            }

            if (models.Count > 0)
            {
                CoreManufacter manufacter = null;
                CoreUser user = null;

                for (int i = 0; i < models.Count; i++)
                {
                    coreModels.Add(Mapper.MapModel(models[i], out manufacter, out user));
                }
            }

            return coreModels;
        }

        public Task<bool> ModelExistsAsync(int id, int? userId = null)
        {
            if (userId == null)
            {
                return _context.Models.AnyAsync(m => m.ModelId == id);
            }

            return _context.Models.AnyAsync(m => m.ModelId == id && m.Manufacter.UserId == userId);
        }

        public Task<bool> ModelNameExistsAsync(string name, int userId, int? id = null)
        {
            if (id is null)
            {
                return _context.Models.AnyAsync(m => m.ModelName == name && m.Manufacter.UserId == userId);
            }
            else
            {
                return _context.Models.AnyAsync(m => m.ModelName == name && m.Manufacter.UserId == userId && m.ModelId != id);
            }
        }

        public async Task<CoreModel> UpdateModelAsync(CoreModel model, int id)
        {
            var dataModel = Mapper.MapModel(model);

            _context.Models.Update(dataModel);

            await SaveAsync();

            return model;
        }

        #region Message Methods
        public string ModelNotFoundMessage(int id)
        {
            return $"{_modelAppSettings.TableName} {_globalSettings.WithId} {id} {_globalSettings.DoesNotExists}";
        }

        public string ModelNameExistMessage(string name)
        {
            return $"{_modelAppSettings.TableName} {_globalSettings.WithName} {name} {_globalSettings.AlreadyExists}";
        }
        #endregion
        #endregion
    }
}
