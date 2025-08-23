using System;

namespace FCG_Users.Domain.Interfaces.Services;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyHashedPassword(string hashedPassword, string providedPassword);
}
