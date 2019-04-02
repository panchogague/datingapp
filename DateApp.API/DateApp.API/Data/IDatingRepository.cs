using DateApp.API.Helpers;
using DateApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<UserModel>> GetUsers(UserParams userParams);
        Task<UserModel> GetUser(int id);
        Task<PhotoModel> GetPhoto(int id);
        Task<PhotoModel> GetMainPhotoForUser(int userId);
        Task<LikeModel> GetLike(int userId, int recipientId);
    }
}
