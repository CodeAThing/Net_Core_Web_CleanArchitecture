using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Repositories;
    public class ProductConfiguration:IEntityTypeConfiguration<Product> 
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id); //Id'yi, primary key olarak belirttik.
            builder.Property(x => x.Name).IsRequired().HasMaxLength(150); //zorunlu ve max 150 karakter
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(x => x.Stock).IsRequired();
        }
    }
