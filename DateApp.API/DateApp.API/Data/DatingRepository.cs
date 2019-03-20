﻿using System;
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
            var users =  _context.Users.Include(u => u.Photos);

            return await PagedList<UserModel>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
