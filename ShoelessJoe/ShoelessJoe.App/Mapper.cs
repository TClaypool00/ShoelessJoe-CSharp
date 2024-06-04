using ShoelessJoe.App.Models;
using ShoelessJoe.App.Models.PostModels;
using ShoelessJoe.Core.CoreModels;

namespace ShoelessJoe.App
{
    public static class Mapper
    {
        public static CoreManufacter MapManufacter(ManufacterViewModel manufacter, int userId)
        {
            return new CoreManufacter
            {
                ManufacterId = manufacter.ManufacturerId,
                ManufacterName = manufacter.ManufacturerName,
                UserId = userId,
            };
        }

        public static CoreModel MapModel(PostModelViewModel model)
        {
            return new CoreModel
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName,
                Manufacter = new CoreManufacter
                {
                    ManufacterId = model.ManufacterId
                }
            };
        }

        public static PotentialBuyViewModel MapPotentialBuy(CorePotentialBuy potentialBuy)
        {
            return new PotentialBuyViewModel
            {
                PotentialBuyId = potentialBuy.PotentialBuyId,
                UserDisplayName = potentialBuy.PotentialBuyer.DisplayName,
                UserId = potentialBuy.PotentialBuyer.UserId
            };
        }

        public static CoreShoe MapShoe(PostShoeViewModel shoe)
        {
            return new CoreShoe(shoe.Files)
            {
                ShoeId = shoe.ShoeId,
                LeftSize = shoe.LeftSize,
                RightSize = shoe.RightSize,
                Model = new CoreModel
                {
                    ModelId = shoe.ModelId.Value,
                    Manufacter = new CoreManufacter
                    {
                        ManufacterId = shoe.ManufacterId.Value
                    }
                }
            };
        }

        public static ShoeViewModel MapShoe(CoreShoe shoe)
        {
            var shoeModel = new ShoeViewModel
            {
                ShoeId = shoe.ShoeId,
                ManufacterId = shoe.Model.Manufacter.ManufacterId,
                ManufacterName = shoe.Model.Manufacter.ManufacterName,
                ModelId = shoe.Model.ModelId,
                ModelName = shoe.Model.ModelName,
                LeftSize = shoe.LeftSize,
                RightSize = shoe.RightSize,
                OwnerId = shoe.Model.Manufacter.User.UserId,
                OwnerDisplayName = shoe.Model.Manufacter.User.DisplayName,
            };

            if (shoe.ShoeImages is not null && shoe.ShoeImages.Count > 0)
            {
                for (int i = 0; i < shoe.ShoeImages.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            shoeModel.LeftShoeFile1 = shoe.ShoeImages[i].File;
                            shoeModel.LeftShoeFilePath1 = shoe.ShoeImages[i].FilePath;
                            break;
                        case 1:
                            shoeModel.LeftShoeFile2 = shoe.ShoeImages[i].File;
                            break;
                        case 2:
                            shoeModel.RightShoeFile1 = shoe.ShoeImages[i].File;
                            break;
                        case 3:
                            shoeModel.RightShoeFile2 = shoe.ShoeImages[i].File;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (shoe.PotentialBuys is not null && shoe.PotentialBuys.Count > 0)
            {
                for (int i = 0; i < shoe.PotentialBuys.Count; i++)
                {
                    shoeModel.PotentialBuys.Add(Mapper.MapPotentialBuy(shoe.PotentialBuys[i]));
                }
            }

            return shoeModel;
        }

        public static CoreUser MapUser(UserViewModel user)
        {
            return new CoreUser
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumb = user.PhoneNumb,
                IsAdmin = user.IsAdmin
            };
        }
    }
}
