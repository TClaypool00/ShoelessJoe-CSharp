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
