using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateApp.API.Helpers;
using DateApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DateApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<LikeModel> GetLike(int userId, int recipientId)
        {
            return await _context.Likes.FirstOrDefaultAsync(u => u.LikerID == userId
            && u.LikeeID == recipientId);
        }

        public async Task<PhotoModel> GetMainPhotoForUser(int userId)
        {
            var photo = await _context.Photos.Where(p => p.UserID == userId).FirstOrDefaultAsync(p => p.IsMain);
            return photo;
        }

        public async Task<PhotoModel> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(d => d.ID == id);
            return photo;
        }

        public async Task<UserModel> GetUser(int id)
        {
            var user = await _context.Users.Include(u => u.Photos).FirstOrDefaultAsync(u => u.ID == id);
            return user;
        }

        public async Task<PagedList<UserModel>> GetUsers(UserParams userParams)
        {
            var users =  _context.Users.Include(u => u.Photos).AsQueryable();

            if (userParams.Likeer)
            {
                var userLikers =await GetUsersLikes(userParams.UserID, userParams.Likeer);
                users = users.Where(u => userLikers.Contains(u.ID));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUsersLikes(userParams.UserID, userParams.Likees);
                users = users.Where(u => userLikees.Contains(u.ID));
            }

            return await PagedList<UserModel>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUsersLikes(int id, bool likers)
        {
            var user = await _context.Users
                .Include(u => u.Likees)
                .Include(u => u.Likers)
                .FirstOrDefaultAsync(u => u.ID == id);

            if (likers)
                return user.Likers.Where(u => u.LikeeID == id).Select(u => u.LikerID);
            else
                return user.Likees.Where(u => u.LikeeID == id).Select(u => u.LikeeID);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
