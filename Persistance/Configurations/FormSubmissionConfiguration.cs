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
    public class FormSubmissionConfiguration : IEntityTypeConfiguration<FormSubmission>
    {
        public void Configure(EntityTypeBuilder<FormSubmission> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.FirstName)
                .IsRequired()
                .HasMaxLength(100);



            builder.Property(f => f.LastName)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(f => f.Email)
                .IsRequired()
                .HasMaxLength(255);


            builder.Property(f => f.Gender)
                .HasMaxLength(20);

            builder.Property(f => f.Country)
                .HasMaxLength(100);

            builder.Property(f => f.Street)
                .HasMaxLength(200);

            builder.Property(f => f.City)
                .HasMaxLength(100);

            builder.Property(f => f.State)
                .HasMaxLength(100);

            builder.Property(f => f.PostalCode)
                .HasMaxLength(20);

            builder.Property(f => f.CountryAddress)
                .HasMaxLength(100);

            builder.HasMany(f => f.AdditionalProperties)
                   .WithOne(a => a.FormSubmission)
                   .HasForeignKey(a => a.FormId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(f => f.Id);
        }
    }
}
