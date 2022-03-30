using Microsoft.EntityFrameworkCore;
using TeamFinder.Server.Models;
using TeamFinder.Shared.Models;

namespace TeamFinder.Server.Data.Repository;

public class UserRepository : RepositoryBase<ApplicationUser>
{
    public UserRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    protected override DbSet<ApplicationUser> DbSet => DbContext.Users;

    public async Task<ApplicationUser> GetUser(string id)
    {
        return await DbSet
            .Include(u => u.Events)
            .SingleAsync(u => u.Id == id);
    }
}