using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStoreSolution.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.Configuration
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.ImagePath).IsRequired();
            builder.Property(x=>x.Caption).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.HasOne(x=> x.Product).WithMany(x=> x.ProductImages).HasForeignKey(x=>x.ProductId);
        } 
    }
}
