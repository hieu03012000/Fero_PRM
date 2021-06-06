using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FeroPRMData.Models
{
    public partial class FeroContext : DbContext
    {
        public FeroContext()
        {
        }

        public FeroContext(DbContextOptions<FeroContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplyCasting> ApplyCasting { get; set; }
        public virtual DbSet<Casting> Casting { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<ModelCasting> ModelCasting { get; set; }
        public virtual DbSet<ModelStyle> ModelStyle { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Style> Style { get; set; }
        public virtual DbSet<SubscribeCasting> SubscribeCasting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=Fero_PRM; User Id=sa; Password=123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplyCasting>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.CastingId });

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Casting)
                    .WithMany(p => p.ApplyCasting)
                    .HasForeignKey(d => d.CastingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyCasting_Casting");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ApplyCasting)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyCasting_Model");
            });

            modelBuilder.Entity<Casting>(entity =>
            {
                entity.Property(e => e.CloseTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OpenTime).HasColumnType("datetime");

                entity.Property(e => e.Salary).HasColumnType("decimal(38, 6)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Casting)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Casting_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Fanpage)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Gmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Web)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Link)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Image_Model");
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Bust).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.Gmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Height).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.Hip).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SocialNetworkLink)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Waist).HasColumnType("decimal(38, 6)");

                entity.Property(e => e.Weight).HasColumnType("decimal(38, 6)");
            });

            modelBuilder.Entity<ModelCasting>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.CastingId });

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Casting)
                    .WithMany(p => p.ModelCasting)
                    .HasForeignKey(d => d.CastingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelCasting_Casting");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelCasting)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelCasting_Model");
            });

            modelBuilder.Entity<ModelStyle>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.StyleId });

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelStyle)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelStyle_Model");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.ModelStyle)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelStyle_Style");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Notification_Customer");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.ModelId)
                    .HasConstraintName("FK_Notification_Model");
            });

            modelBuilder.Entity<Style>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubscribeCasting>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.CastingId });

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Casting)
                    .WithMany(p => p.SubscribeCasting)
                    .HasForeignKey(d => d.CastingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscribeCasting_Casting");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.SubscribeCasting)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubscribeCasting_Model");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
