using store_management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using store_management.Infrastructure.Data;

namespace store_management.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private AppDbContext context;
        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteUser(int user_id)
        {
            Users user = context.Users.Find(user_id);  
            if (user != null)
            {
                context.Users.Remove(user);  
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Users GetUserByID(int user_id)
        {
            return context.Users.Find(user_id);
        }

        public IEnumerable<Users> GetUsers()
        {
            return context.Users.ToList();
        }

        public void InsertUser(Users user)
        {
            context.Users.Add(user);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateUser(Users user)
        {
            context.Entry(user).State = EntityState.Modified;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();  // Dispose DbContext
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
