using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;

namespace TeamFinder.Server.Data.Repository;

public class UserRepository : RepositoryBase<ApplicationUser>
{
    public UserRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    protected override DbSet<ApplicationUser> DbSet => DbContext.Users;
}