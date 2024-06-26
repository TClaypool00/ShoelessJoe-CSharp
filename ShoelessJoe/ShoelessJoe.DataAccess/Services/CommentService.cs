﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShoelessJoe.Core.CoreModels;
using ShoelessJoe.Core.Interfaces;
using ShoelessJoe.DataAccess.AppSettings;
using ShoelessJoe.DataAccess.DataModels;

namespace ShoelessJoe.DataAccess.Services
{
    public class CommentService : ServiceHelper, ICommentService
    {
        #region Private Fields
        private readonly CommentAppSettings _commentAppSettings;
        #endregion

        #region Constructors
        public CommentService(ShoelessJoeContext context, IConfiguration configuration) : base(context, configuration)
        {
            _commentAppSettings = new CommentAppSettings(_configuration);
        }
        #endregion

        #region Public Properties
        public string NoCommentsFoundMessage => _commentAppSettings.NoCommentFoundMessage;

        public string CommentDeletedOKMessage => _commentAppSettings.CommentDeletedOKMessage;
        #endregion

        #region Public Methods
        public async Task<CoreComment> AddCommentAsync(CoreComment comment)
        {
            var dataComment = Mapper.MapComment(comment);

            await _context.Comments.AddAsync(dataComment);

            await SaveAsync();

            if (dataComment.CommentId == 0)
            {
                throw new ArgumentException(_idErrorMessage);
            }

            dataComment = await FindCommentByIdAsync(dataComment.CommentId);

            return Mapper.MapComment(dataComment);
        }

        public Task<bool> CommentExistsByIdAsync(int id)
        {
            return _context.Comments.AnyAsync(c => c.CommentId == id);
        }

        public Task<bool> CommentOwnedByUserAsync(int id, int userId)
        {
            return _context.Comments.AnyAsync(c => c.UserId == userId && c.CommentId == id);
        }

        public async Task DeleteCommentAsync(int id)
        {
            _context.Comments.Remove(await _context.Comments.FindAsync(id));
            await SaveAsync();
        }

        public async Task<CoreComment> GetCommentAsync(int id)
        {
            var dataComment = await FindCommentByIdAsync(id);

            return Mapper.MapComment(dataComment);
        }

        public async Task<List<CoreComment>> GetCommentsAsync(int? potentialBuyId = null, int? shoeId = null, int? userId = null, int? index = null)
        {
            List<Comment> comments = null;
            var coreComments = new List<CoreComment>();

            ConfigureIndex(index);

            if (potentialBuyId is null && shoeId is null && userId is null)
            {
                comments = await _context.Comments.Select(c => new Comment
                {
                    CommentId = c.CommentId,
                    CommentText = c.CommentText,
                    DatePosted = c.DatePosted,
                    PotentialBuyId = c.PotentialBuyId,
                    User = new User
                    {
                        UserId = c.User.UserId,
                        FirstName = c.User.FirstName,
                        LastName = c.User.LastName,
                    }
                })
                .Take(10)
                .Skip(_index)
                .ToListAsync();
            }
            else
            {
                if (potentialBuyId is not null)
                {
                    comments = await _context.Comments.Select(c => new Comment
                    {
                        CommentId = c.CommentId,
                        CommentText = c.CommentText,
                        DatePosted = c.DatePosted,
                        PotentialBuyId = c.PotentialBuyId,
                        User = new User
                        {
                            UserId = c.User.UserId,
                            FirstName = c.User.FirstName,
                            LastName = c.User.LastName,
                        },
                        PotentialBuy = new PotentialBuy
                        {
                            ShoeId = c.PotentialBuy.ShoeId
                        }
                    })
                    .Where(a => a.PotentialBuyId == potentialBuyId)
                    .Take(10)
                    .Skip(_index)
                    .ToListAsync();
                }

                if (shoeId is not null)
                {
                    if (comments is null)
                    {
                        comments = await _context.Comments.Select(c => new Comment
                        {
                            CommentId = c.CommentId,
                            CommentText = c.CommentText,
                            DatePosted = c.DatePosted,
                            PotentialBuyId = c.PotentialBuyId,
                            User = new User
                            {
                                UserId = c.User.UserId,
                                FirstName = c.User.FirstName,
                                LastName = c.User.LastName,
                            },
                            PotentialBuy = new PotentialBuy
                            {
                                ShoeId = c.PotentialBuy.ShoeId
                            }
                        })
                    .Where(a => a.PotentialBuy.ShoeId == shoeId)
                    .Take(10)
                    .Skip(_index)
                    .ToListAsync();
                    }
                    else
                    {
                        comments = comments.Where(a => a.PotentialBuy.ShoeId == shoeId).ToList();
                    }
                }

                if (userId is not null)
                {
                    if (comments is null)
                    {
                        comments = await _context.Comments.Select(c => new Comment
                        {
                            CommentId = c.CommentId,
                            CommentText = c.CommentText,
                            DatePosted = c.DatePosted,
                            PotentialBuyId = c.PotentialBuyId,
                            User = new User
                            {
                                UserId = c.User.UserId,
                                FirstName = c.User.FirstName,
                                LastName = c.User.LastName,
                            }
                        })
                        .Where(a => a.User.UserId == userId)
                        .Take(10)
                        .Skip(_index)
                        .ToListAsync();
                    }
                    else
                    {
                        comments = comments.Where(a => a.User.UserId == userId).ToList();
                    }
                }
            }

            if (comments.Count > 0)
            {
                for (int i = 0; i < comments.Count; i++)
                {
                    coreComments.Add(Mapper.MapComment(comments[i]));
                }
            }

            return coreComments;
        }

        public async Task<CoreComment> UpdateCommentAsync(CoreComment comment)
        {
            var dataComment = await FindCommentByIdAsync(comment.CommentId);
            dataComment.UserId = dataComment.User.UserId;
            Comment newComment = dataComment;

            newComment.CommentText = comment.CommentText;

            _context.Entry(dataComment).CurrentValues.SetValues(newComment);

            await SaveAsync();

            return Mapper.MapComment(dataComment);
        }

        #region Message Methods
        public string CommentNotfoundMessage(int id)
        {
            return $"{_commentAppSettings.TableName} {_globalSettings.WithId}{id}{_globalSettings.DoesNotExists}";
        }
        #endregion
        #endregion

        #region Private Methods
        private Task<Comment> FindCommentByIdAsync(int id)
        {
            return _context.Comments.Select(c => new Comment
            {
                CommentId = c.CommentId,
                CommentText = c.CommentText,
                DatePosted = c.DatePosted,
                PotentialBuyId = c.PotentialBuyId,
                User = new User
                {
                    UserId = c.User.UserId,
                    FirstName = c.User.FirstName,
                    LastName = c.User.LastName,
                }
            }).FirstOrDefaultAsync(a => a.CommentId == id);
        }
        #endregion
    }
}
