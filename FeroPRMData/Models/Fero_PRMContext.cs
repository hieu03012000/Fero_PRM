using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FeroPRMData.Models
{
    public partial class Fero_PRMContext : DbContext
    {
        public Fero_PRMContext()
        {
        }

        public Fero_PRMContext(DbContextOptions<Fero_PRMContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplyCasting> ApplyCasting { get; set; }
        public virtual DbSet<Casting> Casting { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<FavoriteModel> FavoriteModel { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<ModelOffer> ModelOffer { get; set; }
        public virtual DbSet<ModelStyle> ModelStyle { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Offer> Offer { get; set; }
        public virtual DbSet<Style> Style { get; set; }
        public virtual DbSet<SubscribeCasting> SubscribeCasting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost,1433; Database=ModelBooking; User Id=admin; Password=teen;");
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

                entity.Property(e => e.Time).HasColumnType("datetime");

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

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

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

                entity.Property(e => e.DeviceToken)
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

            modelBuilder.Entity<FavoriteModel>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.CustomerId });

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.FavoriteModel)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteModel_Customer");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.FavoriteModel)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoriteModel_Model");
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
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Gmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SocialNetworkLink)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DeviceToken)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModelOffer>(entity =>
            {
                entity.HasKey(e => new { e.ModelId, e.OfferId });

                entity.Property(e => e.ModelId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Model)
                    .WithMany(p => p.ModelOffer)
                    .HasForeignKey(d => d.ModelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelOffer_Model");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.ModelOffer)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModelOffer_Offer");
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
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Offer)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Offer_Customer");
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
