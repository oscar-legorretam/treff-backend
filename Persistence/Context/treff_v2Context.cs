using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Context
{
    public partial class treff_v2Context : DbContext
    {
        public treff_v2Context()
        {
        }

        public treff_v2Context(DbContextOptions<treff_v2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<SubCategories> SubCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("Server=127.0.0.1;Database=treff_v2;Uid=root;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.HasMany(e => e.SubCategories)
                .WithOne(g => g.Category)
                .HasPrincipalKey(category => category.Id)
                .HasForeignKey(f => f.IdCategory)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<SubCategories>(entity =>
            {
                entity.ToTable("sub_categories");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdCategory)
                    .HasColumnName("id_category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdSubCategory)
                    .HasColumnName("id_sub_category")
                    .HasColumnType("int(11)");

                entity
                    .HasOne(bc => bc.Category)
                    .WithMany(c => c.SubCategories)
                    .HasForeignKey(bc => bc.IdCategory)
                    .IsRequired(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
