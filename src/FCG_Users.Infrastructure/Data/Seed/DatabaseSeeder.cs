using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Enums;
using FCG_Users.Domain.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FCG_Users.Infrastructure.Data.Seed;

public class DatabaseSeeder
{
    private readonly FCGUsersDbContext _context;
    private readonly IPasswordService _passwordService;

    public DatabaseSeeder(FCGUsersDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {

        var adminId = Guid.Parse(configuration["AdminUser:Id"]!);

        if (!await _context.Users.AsNoTracking().AnyAsync(u => u.Id == adminId && u.Role == UserRole.Admin))
        {
            var adminUser = new User
            {
                Id = adminId,
                UserName = configuration["AdminUser:UserName"]!,
                Email = configuration["AdminUser:Email"]!,
                Password = _passwordService.HashPassword(configuration["AdminUser:Password"]!),
                Role = UserRole.Admin
            };

            await _context.Users.AddAsync(adminUser);
            await _context.SaveChangesAsync();
        }
    }
}
