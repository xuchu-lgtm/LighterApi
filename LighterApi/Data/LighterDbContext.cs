using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LighterApi.Data.Project;

namespace LighterApi.Data
{
    public class LighterDbContext : DbContext
    {
        public LighterDbContext(DbContextOptions<LighterDbContext> options) : base(options) { }

        public DbSet<LighterApi.Data.Project.Project> Projects { get; set; }
        public DbSet<Project.ProjectGroup> ProjectGroups { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project.Project>().ToTable("Project")
                .Property(p => p.Id).HasColumnName("id");

            modelBuilder.Entity<Entity>().HasQueryFilter(p => p.TenantId == "1");//全局的筛选器

            modelBuilder.Entity<AuditLog>(); //以隐式的方式添加进来
        }
    }
}
