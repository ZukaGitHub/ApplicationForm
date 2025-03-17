using Domain.Enitites;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Configurations
{
    public class AdditionalPropertiesConfiguration : IEntityTypeConfiguration<AdditionalProperties>
    {
        public void Configure(EntityTypeBuilder<AdditionalProperties> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Key)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(a => a.Value)
                .IsRequired();

            builder.HasOne(a => a.FormSubmission)
                   .WithMany(f => f.AdditionalProperties)
                   .HasForeignKey(a => a.FormId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.Id);
        }
    }
}
