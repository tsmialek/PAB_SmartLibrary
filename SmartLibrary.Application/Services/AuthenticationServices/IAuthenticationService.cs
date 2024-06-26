﻿using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string Email, string password);
        AuthenticationResult Register(string firstName, string lastName, string email, string password);
        User GetById(Guid id);
        User GetByEmail(string email);
        User Delete(Guid id);
        User Delete(string email);
        IEnumerable<User> GetAll();
    }
}
