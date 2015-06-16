using NancyCRUDSample.Models.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NancyCRUDSample.Models.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("ApplicationDbContext")
        { }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ProductMapping());
            modelBuilder.Configurations.Add(new CategoryMapping());
        }

        public override int SaveChanges()
        {
            #region -- Created at --
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                }
            }
            #endregion

            #region -- Modified at --
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("ModifiedAt") != null))
            {
                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                {
                    entry.Property("ModifiedAt").CurrentValue = DateTime.Now;
                }
            }
            #endregion

            return base.SaveChanges();
        }
    }
}