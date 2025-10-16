using System;
using System.Collections.Generic;
using store_management.Entity;

namespace store_management.Repository
{
    public interface IUserRepository : IDisposable
    {
    IEnumerable<Users> GetUsers();
    Users GetUserByID(int user_id);
    void InsertUser(Users user);
    void DeleteUser(int user_id);
    void UpdateUser(Users user);
    void Save();
}
}
