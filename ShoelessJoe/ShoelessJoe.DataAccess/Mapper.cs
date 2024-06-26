﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.DataAccess.DataModels;

namespace ShoelessJoe.DataAccess
{
    public class Mapper
    {
        public static ShoeImage MapShoeImage(CoreShoeImage shoeImage, int shoeId)
        {
            return new ShoeImage
            {
                FileName = shoeImage.FileName,
                ShoeArray = shoeImage.ShoeArray,
                ShoeId = shoeId,
                ShoeImageId = shoeImage.ShoeId
            };
        }

        public static User MapUser(CoreUser user)
        {
            return new User
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

        public static CoreUser MapUser(User user, bool showPassword = false)
        {
            return new CoreUser
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumb = user.PhoneNumb,
                IsAdmin = user.IsAdmin,
                Password = showPassword ? user.Password : ""
            };
        }

        public static Manufacter MapManufacter(CoreManufacter manufacter)
        {
            var dataManufacter = new Manufacter
            {
                ManufacterName = manufacter.ManufacterName,
                UserId = manufacter.UserId,                
            };

            if (manufacter.ManufacterId > 0)
            {
                dataManufacter.ManufacterId = manufacter.ManufacterId;
            }

            return dataManufacter;
        }

        public static CoreManufacter MapManufacter(Manufacter manufacter, CoreUser user = null)
        {
            var coreManufacter = new CoreManufacter
            {
                ManufacterId = manufacter.ManufacterId,
                ManufacterName = manufacter.ManufacterName                
            };

            if (user is not null)
            {
                coreManufacter.User = user;
            }
            else if (manufacter.User is not null)
            {
                coreManufacter.User = MapUser(manufacter.User);
            }

            return coreManufacter;
        }

        public static SelectListItem MapDropDown(Manufacter manufacter)
        {
            return new SelectListItem
            {
                Value = manufacter.ManufacterId.ToString(),
                Text = manufacter.ManufacterName,
                Selected = false
            };
        }

        public static Model MapModel(CoreModel model)
        {
            var dataModel = new Model
            {
                ModelName = model.ModelName,
                ManufacterId = model.Manufacter.ManufacterId
            };

            if (dataModel.ModelId != 0)
            {
                dataModel.ModelId = model.ModelId;
            }

            return dataModel;
        }

        public static CoreModel MapModel(Model model, out CoreManufacter manufacter, out CoreUser user)
        {
            manufacter = null;
            user = null;

            var coreModel = new CoreModel
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName               
            };

            if (manufacter is not null)
            {
                coreModel.Manufacter = manufacter;
            }
            else
            {
                coreModel.Manufacter = MapManufacter(model.Manufacter);

                if (user is null)
                {
                    user = coreModel.Manufacter.User;
                }
                else
                {
                    coreModel.Manufacter.User = user;
                }
            }

            return coreModel;
        }

        public static CoreModel MapModel(Model model, CoreManufacter manufacter = null)
        {
            var coreModel = new CoreModel
            {
                ModelId = model.ModelId,
                ModelName = model.ModelName
            };

            if (model.Manufacter is not null)
            {
                coreModel.Manufacter = MapManufacter(model.Manufacter);
            } else if (manufacter != null)
            {
                coreModel.Manufacter = manufacter;
            }

            return coreModel;
        }

        public static SelectListItem MapDropDown(Model model)
        {
            return new SelectListItem
            {
                Value = model.ModelId.ToString(),
                Text = model.ModelName,
                Selected = false
            };
        }

        public static Shoe MapShoe(CoreShoe shoe)
        {
            var dataShoe = new Shoe
            {
                LeftSize = shoe.LeftSize,
                RightSize = shoe.RightSize,
                DatePosted = shoe.DatePosted,
                ModelId = shoe.Model.ModelId,
            };

            if (shoe.ShoeId != 0)
            {
                dataShoe.ShoeId = shoe.ShoeId;
            }

            return dataShoe;
        }

        public static CoreShoe MapShoe(Shoe shoe)
        {
            var coreShoe = new CoreShoe
            {
                ShoeId = shoe.ShoeId,
                LeftSize = shoe.LeftSize,
                RightSize = shoe.RightSize,
                DatePosted = shoe.DatePosted
            };

            if (shoe.Model is not null)
            {
                coreShoe.Model = MapModel(shoe.Model);
            }

            if (shoe.PotentialBuys is not null && shoe.PotentialBuys.Count > 0)
            {
                coreShoe.PotentialBuys = new List<CorePotentialBuy>();

                for (int i = 0; i < shoe.PotentialBuys.Count; i++)
                {
                    coreShoe.PotentialBuys.Add(MapPotentialBuy(shoe.PotentialBuys[i]));
                }
            }

            if (shoe.ShoeImage is not null)
            {
                coreShoe.ShoeImages = new List<CoreShoeImage>
                {
                    MapShoeImage(shoe.ShoeImage)
                };
            }

            return coreShoe;
        }

        public static PotentialBuy MapPotentialBuy(CorePotentialBuy potentialBuy)
        {
            var dataPotentialBuy = new PotentialBuy
            {
                ShoeId = potentialBuy.ShoeId,
                PotentialBuyerUserId = potentialBuy.PotentialBuyerUserId,
                IsSold = potentialBuy.IsSold,
                DateSold = potentialBuy.DateSoldDate
            };

            if (potentialBuy.PotentialBuyId != 0)
            {
                dataPotentialBuy.PotentialBuyId = potentialBuy.PotentialBuyId;
            }

            return dataPotentialBuy;
        }

        public static CorePotentialBuy MapPotentialBuy(PotentialBuy potentialBuy)
        {
            var corePotentialBuy = new CorePotentialBuy
            {
                PotentialBuyId = potentialBuy.PotentialBuyId,
                DateSoldDate = potentialBuy.DateSold,
                IsSold = potentialBuy.IsSold,
            };

            if (potentialBuy.PotentialBuyer is not null)
            {
                corePotentialBuy.PotentialBuyer = MapUser(potentialBuy.PotentialBuyer);
            }

            if (potentialBuy.Shoe is not null)
            {
                corePotentialBuy.Shoe = MapShoe(potentialBuy.Shoe);
            }

            return corePotentialBuy;
        }

        public static CorePotentialBuy MapPotentialBuy(PotentialBuy potentialBuy, out CoreUser potentialBuyer, out Shoe shoe)
        {
            potentialBuyer = null;
            shoe = null;

            var corePotentialBuy = new CorePotentialBuy
            {
                PotentialBuyId = potentialBuy.PotentialBuyId,
                DateSoldDate = potentialBuy.DateSold,
                IsSold = potentialBuy.IsSold,
            };

            if (potentialBuy.PotentialBuyer is not null)
            {
                corePotentialBuy.PotentialBuyer = MapUser(potentialBuy.PotentialBuyer);
            }

            if (potentialBuy.Shoe is not null)
            {
                corePotentialBuy.Shoe = MapShoe(potentialBuy.Shoe);
            }

            return corePotentialBuy;
        }

        public static CoreShoeImage MapShoeImage(ShoeImage shoeImage)
        {
            return new CoreShoeImage(shoeImage.ShoeArray, shoeImage.FileName)
            {
                ShoeImageId = shoeImage.ShoeImageId,
                //Shoe = MapShoe(shoeImage.Shoe)
            };
        }

        public static ShoeImage MapShoeImage(CoreShoeImage shoeImage)
        {
            var dataShoeImage = new ShoeImage
            {
                ShoeImageId = shoeImage.ShoeImageId,
                ShoeId = shoeImage.Shoe.ShoeId,
                ShoeArray = shoeImage.ShoeArray,
                FileName = shoeImage.FileName,
            };

            if (shoeImage.ShoeImageId != 0)
            {
                dataShoeImage.ShoeImageId = shoeImage.ShoeImageId;
            }
            return dataShoeImage;
        }

        public static CoreComment MapComment(Comment comment)
        {
            var coreComment = new CoreComment
            {
                CommentId = comment.CommentId,
                CommentText = comment.CommentText,
                DatePosted = comment.DatePosted,
            };

            if (comment.User != null)
            {
                coreComment.User = MapUser(comment.User);
            }

            return coreComment;
        }

        public static Comment MapComment(CoreComment comment)
        {
            var dataComment = new Comment
            {
                CommentText = comment.CommentText,
                DatePosted = comment.DatePosted,
                PotentialBuyId = comment.PotentialBuyId,
                UserId = comment.UserId
            };

            if (comment.CommentId != 0)
            {
                dataComment.CommentId = comment.CommentId;
            }

            return dataComment;
        }
    }
}
