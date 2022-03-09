using Microsoft.EntityFrameworkCore;
using UserManaging.Infrastructure.Data;

namespace UserManaging.API.Tests.Helpers
{
    internal class InMemoryDbContextGenerator
    {
        public static ApplicationDbContext Context
        {
            get
            {
                if (Context is null) _ = new InMemoryDbContextGenerator();

                return Context;
            }
            private set { Context = value; }
        }

        private InMemoryDbContextGenerator()
        {
            Context = GenereteContext();
        }

        private static ApplicationDbContext GenereteContext()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            builder.UseInMemoryDatabase("userManaging");
            options = builder.Options;

            var dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}
