﻿using Microsoft.EntityFrameworkCore;
using Models.Data;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Service.Interface;
using Models.ViewModels.Menu;
using Service.Helpers;
using Service.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Service.Implementation
{

    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IHubContext<HenryHub> _hubContext;
        public AuthService(DataContext context, IHubContext<HenryHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        public void abc()
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("shc@1234", out passwordHash, out passwordSalt);

           
            var list = _context.Users.ToList();
            list.ForEach(x =>
            {
                x.PasswordHash = passwordHash;
                x.PasswordSalt = passwordSalt;
               
            });

             _context.SaveChangesAsync();
        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            user.IsOnlines = true;
            await _context.SaveChangesAsync();
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public async Task<Role> GetRolesAsync(int role)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.ID == role);
        }
        public async Task<User> FindByNameAsync(string username)
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (item != null)
                return item;

            return null;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.Password = user.Password.SHA256Hash();
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> Edit(string username)
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash("1", out passwordHash, out passwordSalt);
            item.PasswordHash = passwordHash;
            item.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return item;
        }
        public async Task<User> GetById(int Id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.ID == Id);
        }
        public async Task<List<MenuViewModel>> GetMenusAsync(int role)
        {
            var model =await _context.MenuRoles.Where(x => x.RoleID == role).Select(x => new MenuViewModel
            {
                Code = x.Menu.Code,
                BackgroudColor = x.Menu.BackgroudColor,
                Link = x.Menu.Link,
                FontAwesome = x.Menu.FontAwesome,
                Index = x.Index
            }).ToListAsync();
            return model;
        }

        public async Task<bool> UpdateIsOnline(int userid)
        {
            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.ID == userid);
            findUser.IsOnlines = false;
            try
            {
                await _context.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("updateCount");
                return true;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return false;
            }
        }

    }
}
