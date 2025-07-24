﻿using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.DbContexts;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        dbContextBuilder.UseSqlServer(x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

        return new ApplicationDbContext(dbContextBuilder.Options);
    }
}
