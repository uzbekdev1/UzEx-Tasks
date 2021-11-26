using e_Shopping.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace e_Shopping.Data.Contexts
{
    public class AppDbContext : IdentityDbContext<ApiUser, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
    }
}
