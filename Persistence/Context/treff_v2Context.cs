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

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<SubCategories> SubCategories { get; set; }
        public virtual DbSet<Freelancer> Freelancers { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<ServiceImage> ServiceImages { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FreelancerComment> FreelancerComments { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<Language> Languages { get; set; }

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
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("int(11)")
                    .HasDefaultValue(0);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);

                entity.HasOne(s => s.Parent)
                    .WithMany(m => m.SubCategories)
                    .HasForeignKey(e => e.ParentId);
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

                //entity
                //    .HasOne(bc => bc.Category)
                //    .WithMany(c => c.SubCategories)
                //    .HasForeignKey(bc => bc.IdCategory)
                //    .IsRequired(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
