using Microsoft.EntityFrameworkCore;
using ucondo_challenge.business.Entities;

namespace ucondo_challenge.infrastructure.Mappings
{
    public class ChartOfAccountsMapping : IEntityTypeConfiguration<ChartOfAccountsEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ChartOfAccountsEntity> builder)
        {
            builder.ToTable("chart_of_accounts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(x => x.TenantId)
                .HasColumnName("tenant_id")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("type")
                .IsRequired();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.AllowEntries)
                .HasColumnName("allow_entries")
                .IsRequired();

            builder.Property(x => x.Code)
                .HasColumnName("code")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ParentId)
                .HasColumnName("parent_id")
                .IsRequired(false);

            builder.HasOne(x => x.Parent)                
                .WithMany()
                .HasForeignKey(x => x.ParentId);

            builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("timestamp")
            .HasColumnName("created_at");

            builder.Property(c => c.DeletedAt)
                .IsRequired(false)
                .HasColumnType("timestamp")
                .HasColumnName("deleted_at");

            builder.Property(c => c.UpdatedAt)
                .IsRequired(false)
                .HasColumnType("timestamp")
                .HasColumnName("updated_at");

            builder.HasIndex(x => x.Id);
            builder.HasIndex(x => x.TenantId);
            builder.HasIndex(x => x.Code).IsUnique();
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }    
}
