using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);

        // Value Object: Money (Owned Entity)
        builder.OwnsOne(p => p.AcquisitionCost, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("AcquisitionCostAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("AcquisitionCostCurrency")
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.OwnsOne(p => p.AcquisitionCostUsd, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("AcquisitionCostUsdAmount")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("AcquisitionCostUsdCurrency")
                .HasMaxLength(3)
                .IsRequired()
                .HasDefaultValue("USD");
        });

        builder.Property(p => p.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(p => p.AcquisitionDate)
            .IsRequired();

        // Relationships
        builder.HasOne(p => p.Supplier)
            .WithMany()
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(p => p.Status);
        builder.HasIndex(p => p.AcquisitionDate);

        // Ignore domain events
        builder.Ignore(p => p.DomainEvents);
    }
}
