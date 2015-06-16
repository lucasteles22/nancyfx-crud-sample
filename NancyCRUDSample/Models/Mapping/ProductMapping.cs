using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace NancyCRUDSample.Models.Mapping
{
    public class ProductMapping : EntityTypeConfiguration<Product>
    {
        public ProductMapping()
        {
            ToTable("Products");

            HasKey(x => x.Id)
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            Property(x => x.CreatedAt)
                .IsRequired();

            Property(x => x.ModifiedAt)
                .IsRequired();

            HasRequired(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);
        }
    }
}