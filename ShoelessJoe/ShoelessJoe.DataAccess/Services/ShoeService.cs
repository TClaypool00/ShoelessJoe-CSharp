using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;
using System.Linq.Expressions;

namespace ShoelessJoe.DataAccess.Services
{
    public class ShoeService : ServiceHelper, IShoeService
    {
        #region Private Fields
        private readonly ShoeAppSettings _shoeAppSettings;
        #endregion

        #region Public Properties
        public string NoShoesFoundMessage => _shoeAppSettings.NoShoesFoundMessage;

        public string NoPicturesMessage => _shoeAppSettings.NoPicturesMessage;

        public string CannotBuyOwnShoeMessage => _shoeAppSettings.CannotBuyOwnShoeMessage;

        public string ShoeAlreadySoldMessage => _shoeAppSettings.ShoeAlreadySoldMessage;

        public string BothSizesAreNullMessage => _shoeAppSettings.BothSizesAreNullMessage;

        public string ShoeAddedMessage => _shoeAppSettings.ShoeAddedMessage;

        public string ShoeUpdatedMessage => _shoeAppSettings.ShoeUpdatedMessage;

        public string ShoeSoldMessage => _shoeAppSettings.ShoeSoldMessage;

        public string RigthSize => _shoeAppSettings.RightSize;

        public string LeftSize => _shoeAppSettings.LeftSize;
        #endregion

        #region Constructors
        public ShoeService(ShoelessJoeContext context, IConfiguration configuration) : base(context, configuration)
        {
            _shoeAppSettings = new ShoeAppSettings(_configuration);
        }
        #endregion

        #region Public Methods
        public async Task<CoreShoe> AddShoeAsync(CoreShoe shoe)
        {
            var dataShoe = Mapper.MapShoe(shoe);

            await _context.Shoes.AddAsync(dataShoe);

            await SaveAsync();

            shoe.ShoeId = dataShoe.ShoeId;

            if (shoe.ShoeImages is not null && shoe.ShoeImages.Count > 0)
            {
                dataShoe.ShoeImages = new List<ShoeImage>();

                try
                {
                    for (int i = 0; i < shoe.ShoeImages.Count; i++)
                    {
                        dataShoe.ShoeImages.Add(Mapper.MapShoeImage(shoe.ShoeImages[i], shoe.ShoeId));
                    }

                    await _context.ShoeImages.AddRangeAsync(dataShoe.ShoeImages);
                    await SaveAsync();
                }
                catch (Exception)
                {
                    _context.Shoes.Remove(dataShoe);
                    await SaveAsync();

                    throw;
                }
            }

            return shoe;
        }

        public async Task<List<CoreShoe>> GetShoesAsync(int? ownerId = null, int? soldToId = null, DateTime? datePosted = null, bool? isSold = null, int? index = null)
        {
            ConfigureIndex(index);

            var shoes = new List<Shoe>();
            var coreShoes = new List<CoreShoe>();

            if (ownerId is null && soldToId is null && datePosted is null && isSold is null)
            {
                shoes = await _context.Shoes.Select(s => new Shoe
                {
                    ShoeId = s.ShoeId,
                    LeftSize = s.LeftSize,
                    RightSize = s.RightSize,
                    DatePosted = s.DatePosted,
                    Model = new Model
                    {
                        ModelName = s.Model.ModelName,
                        Manufacter = new Manufacter
                        {
                            ManufacterName = s.Model.Manufacter.ManufacterName,
                            User = new User
                            {
                                FirstName = s.Model.Manufacter.User.FirstName,
                                LastName = s.Model.Manufacter.User.LastName
                            }
                        }
                    },
                    ShoeImage = s.ShoeImages.FirstOrDefault()
                })
                .Take(15)
                .OrderBy(x => x.DatePosted)
                .ToListAsync();
            }
            else
            {
                if (ownerId is not null)
                {
                    shoes = await _context.Shoes
                    .Select(FindDataShoe())
                    .Take(10)
                    .Skip(_index)
                    .Where(a => a.Model.Manufacter.User.UserId == ownerId)
                    .ToListAsync();
                }

                if (soldToId is not null)
                {
                    shoes = shoes.Where(a => a.Model.Manufacter.UserId == soldToId).ToList();
                }

                if (datePosted is not null)
                {
                    shoes = shoes.Where(b => b.DatePosted == datePosted).ToList();
                }
            }

            if (shoes.Count > 0)
            {
                for (int i = 0; i < shoes.Count; i++)
                {
                    coreShoes.Add(Mapper.MapShoe(shoes[i]));
                }
            }

            return coreShoes;
        }

        public async Task<CoreShoe> GetShoeAsync(int id, int userId)
        {
            var dataShoe = await _context.Shoes
                .Select(FindDataShoe())
                .FirstOrDefaultAsync(s => s.ShoeId == id);

            dataShoe.ShoeImages = await _context.ShoeImages
                .Where(a => a.ShoeId == id)
                .Select(b => new ShoeImage
                {
                    ShoeImageId = b.ShoeImageId,
                    FileName = b.FileName,
                    ShoeArray = b.ShoeArray
                })
                .ToListAsync();

            if (await ShoeIsOwnedByUserAsync(id, userId))
            {
                dataShoe.PotentialBuys = await _context.PotentialBuys
                    .Where(b => b.ShoeId == id)
                    .Take(5)
                    .Select(a => new PotentialBuy
                    {
                        PotentialBuyId = a.PotentialBuyId,
                        IsSold = a.IsSold,
                        PotentialBuyer = new User
                        {
                            UserId = a.PotentialBuyer.UserId,
                            FirstName = a.PotentialBuyer.FirstName,
                            LastName = a.PotentialBuyer.LastName
                        }
                    })
                    .ToListAsync();
            }

            return Mapper.MapShoe(dataShoe);
        }

        public Task<bool> ShoeExistsById(int id)
        {
            return _context.Shoes.AnyAsync(s => s.ShoeId == id);
        }

        public Task<bool> ShoeIsOwnedByUserAsync(int id, int ownerId)
        {
            return _context.Shoes.AnyAsync(s => s.ShoeId == id && s.Model.Manufacter.UserId == ownerId);
        }

        public async Task<CoreShoe> UpdateShoeAsync(CoreShoe shoe, int id)
        {
            var dataShoe = Mapper.MapShoe(shoe);

            _context.Shoes.Update(dataShoe);

            await SaveAsync();

            return shoe;
        }

        public string ShoeNotFoundMessage(int id)
        {
            return $"{_shoeAppSettings.TableName} {_globalSettings.WithId} {id} {_globalSettings.DoesNotExists}";
        }
        #endregion

        #region Private Methods
        private static Expression<Func<Shoe, Shoe>> FindDataShoe()
        {
            return a => new Shoe
            {
                ShoeId = a.ShoeId,
                LeftSize = a.LeftSize,
                RightSize = a.RightSize,
                DatePosted = a.DatePosted,
                Model = new Model
                {
                    ModelId = a.ModelId,
                    ModelName = a.Model.ModelName,
                    Manufacter = new Manufacter
                    {
                        ManufacterId = a.Model.ManufacterId,
                        ManufacterName = a.Model.Manufacter.ManufacterName,
                        User = new User
                        {
                            UserId = a.Model.Manufacter.UserId,
                            FirstName = a.Model.Manufacter.User.FirstName,
                            LastName = a.Model.Manufacter.User.LastName
                        }
                    }
                },
                ShoeImage = a.ShoeImages.FirstOrDefault()
            };
        }

        private static string GenerateFileName(IFormFile file, ShoePicturesTypes type)
        {
            return Path.GetFileNameWithoutExtension(file.FileName) + "-" + Guid.NewGuid().ToString() + "-" + type.ToString() + Path.GetExtension(file.FileName);
        }
        #endregion

        #region Public Enums
        public enum ShoePicturesTypes
        {
            LeftShoe1,
            LeftShoe2,
            RightShoe1,
            RightShoe2
        }
        #endregion
    }
}
