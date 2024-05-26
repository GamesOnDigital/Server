using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using BCrypt.Net;

namespace DAL.Functions;

public partial class UserDal : IUserDal
{
    CountdContext db;
    public UserDal(CountdContext db)
    {
        this.db = db;
    }

    public Task<bool> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAll()
    {
        try
        {
            return await db.Users.ToListAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("An error occurred while saving data to the database. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("error-", ex);
        }
    }

    public Task<User> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> Login(string email, string password)
    {
        try
        {
            User newU = await db.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            if (newU != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(password,newU.Password))
                    throw new Exception("סיסמא לא נכונה");
                else return newU;
            }
            else throw new Exception("מייל לא נכון");                                  
        }
        catch (Exception ex)
        {
            throw new Exception("error--",ex);
        }
        {
        }
    }

    public async Task<User> SignIn(User user)
    {

        try
        {
            if (db.Users.FirstOrDefault(u => u.Email.Equals(user.Email)) != null)
                throw new Exception("אמייל קיים במערכת", null);
            else if (db.Users.FirstOrDefault(u => u.Password.Equals(user.Password)) != null)
                throw new Exception("סיסמא לא תקינה", null);
            else
            {
                string encodingPassword= BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password= encodingPassword;
                await db.Users.AddAsync(user);
                db.SaveChangesAsync();
                return user;
            }
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("An error occurred while saving data to the database. Please try again later.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("error-", ex);
        }
    }

    public Task<User> Update(User gender)
    {
        throw new NotImplementedException();
    }








}
