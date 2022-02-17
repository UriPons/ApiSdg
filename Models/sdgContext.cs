using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class sdgContext : DbContext
    {
        public sdgContext()
        {
        }

        public sdgContext(DbContextOptions<sdgContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiTable> ApiTable { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiTable>(entity =>
            {
                entity.HasKey(e => e.Region)
                    .HasName("PRIMARY");

                entity.ToTable("api_table");

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.F).HasColumnName("f");

                entity.Property(e => e.M).HasColumnName("m");

                entity.Property(e => e.T).HasColumnName("t");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
