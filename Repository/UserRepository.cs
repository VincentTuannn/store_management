using store_management.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace store_management.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        public void DeleteUser(int user_id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void InsertUser(Users user)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(Users user)
        {
            throw new NotImplementedException();
        }
    }
}
