using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using WordCount.Core.Models;

namespace WordCount.Data.Configurations
{
    public class TextConfiguration : IEntityTypeConfiguration<Text>
    {

        public void Configure(EntityTypeBuilder<Text> builder)
        {
            builder
                .HasKey(a => a.TextId);

            builder
                .Property(m => m.TextId)
                .UseIdentityColumn();

            builder
                .Property(m => m.TextString)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .ToTable("Texts");
        }
    }
}
