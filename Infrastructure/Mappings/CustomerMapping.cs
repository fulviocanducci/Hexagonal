using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Mappings
{
   internal class CustomerMapping : IEntityTypeConfiguration<Customer>
   {
      public void Configure(EntityTypeBuilder<Customer> builder)
      {
         builder.ToTable("customers");
         builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
         builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(100);
         builder.Property(x => x.DateOfBirth)
            .HasColumnName("date_of_birth")
            .HasColumnType("date")
            .IsRequired();
      }

      public static CustomerMapping Instance { get; } = new CustomerMapping();   
   }
}
