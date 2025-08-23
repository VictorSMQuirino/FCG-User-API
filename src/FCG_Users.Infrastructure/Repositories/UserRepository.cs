using FCG_Common.Infrastructure.Data.Repository;
using FCG_Users.Domain.Entities;
using FCG_Users.Domain.Interfaces.Repositories;
using FCG_Users.Infrastructure.Data;

namespace FCG_Users.Infrastructure.Repositories;

public class UserRepository(FCGUsersDbContext context) : BaseRepository<User>(context), IUserRepository;
