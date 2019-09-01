using Desafio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio.Repositories.EntityConfig
{
    public class PhoneConfig : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Number)
                .IsRequired()
                .HasMaxLength(9);

            builder.Property(p => p.AreaCode)
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(p => p.CountryCode)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasOne(u => u.User)
                .WithMany(p => p.Phones);
        }
    }
}
