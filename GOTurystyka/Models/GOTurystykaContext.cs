﻿using Microsoft.EntityFrameworkCore;

#nullable disable

namespace GOTurystyka.Models
{
    public class GOTurystykaContext : DbContext
    {
        public GOTurystykaContext()
        {
        }

        public GOTurystykaContext(DbContextOptions<GOTurystykaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Commission> Commissions { get; set; }
        public virtual DbSet<Foreman> Foremen { get; set; }
        public virtual DbSet<Got3> Got3s { get; set; }
        public virtual DbSet<Got4> Got4s { get; set; }
        public virtual DbSet<Gottier> Gottiers { get; set; }
        public virtual DbSet<LicensesFor> LicensesFors { get; set; }
        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<PointsInSegment> PointsInSegments { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Segment> Segments { get; set; }
        public virtual DbSet<SegmentsInRoute> SegmentsInRoutes { get; set; }
        public virtual DbSet<TemporaryPoint> TemporaryPoints { get; set; }
        public virtual DbSet<Tourist> Tourists { get; set; }
        public virtual DbSet<TouristGot> TouristGots { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersInTrip> UsersInTrips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(13)
                    .HasColumnName("PESEL");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<Commission>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(12);

                entity.Property(e => e.QuitOfficeDate).HasColumnType("date");

                entity.Property(e => e.TakenTheOfficeDate).HasColumnType("date");
            });

            modelBuilder.Entity<Foreman>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Got3>(entity =>
            {
                entity.ToTable("GOT3s");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Tier)
                    .WithMany(p => p.Got3s)
                    .HasForeignKey(d => d.TierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GOT3s_GOTTiers");
            });

            modelBuilder.Entity<Got4>(entity =>
            {
                entity.ToTable("GOT4s");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Tier)
                    .WithMany(p => p.Got4s)
                    .HasForeignKey(d => d.TierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GOT4s_GOTTiers");
            });

            modelBuilder.Entity<Gottier>(entity =>
            {
                entity.ToTable("GOTTiers");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<LicensesFor>(entity =>
            {
                entity.HasKey(e => new {e.ForemanId, e.SegmentId});

                entity.ToTable("LicensesFor");

                entity.Property(e => e.AreaName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.DateOfLicensing).HasColumnType("date");

                entity.HasOne(d => d.Foreman)
                    .WithMany(p => p.LicensesFors)
                    .HasForeignKey(d => d.ForemanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LicensesFor_Foremen");

                entity.HasOne(d => d.Segment)
                    .WithMany(p => p.LicensesFors)
                    .HasForeignKey(d => d.SegmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LicensesFor_Segments");
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Points_Admins");
            });

            modelBuilder.Entity<PointsInSegment>(entity =>
            {
                entity.HasKey(e => new {e.SegmentId, e.PointId, e.OrderingNumber});

                entity.HasOne(d => d.Point)
                    .WithMany(p => p.PointsInSegments)
                    .HasForeignKey(d => d.PointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PointsInSegments_Points");

                entity.HasOne(d => d.Segment)
                    .WithMany(p => p.PointsInSegments)
                    .HasForeignKey(d => d.SegmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PointsInSegments_Segments");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.DateOfCreation).HasColumnType("date");

                entity.Property(e => e.LastUpdate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Routes_Tourists");
            });

            modelBuilder.Entity<Segment>(entity =>
            {
                entity.HasOne(d => d.Foreman)
                    .WithMany(p => p.Segments)
                    .HasForeignKey(d => d.ForemanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Segments_Foremen");

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Segments)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Segments_Tourists");
            });

            modelBuilder.Entity<SegmentsInRoute>(entity =>
            {
                entity.HasKey(e => new {e.OrderingNumber, e.RouteId, e.SegmentId});

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.SegmentsInRoutes)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SegmentsInRoutes_Routes");

                entity.HasOne(d => d.Segment)
                    .WithMany(p => p.SegmentsInRoutes)
                    .HasForeignKey(d => d.SegmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SegmentsInRoutes_Segments");
            });

            modelBuilder.Entity<TemporaryPoint>(entity =>
            {
                entity.Property(e => e.CreationDate).HasColumnType("date");

                entity.Property(e => e.GccheckTime)
                    .HasColumnType("date")
                    .HasColumnName("GCCheckTime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.Used)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.TemporaryPoints)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TemporaryPoints_Tourists");
            });

            modelBuilder.Entity<Tourist>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<TouristGot>(entity =>
            {
                entity.ToTable("TouristGOTs");

                entity.Property(e => e.AwardedOn).HasColumnType("date");

                entity.Property(e => e.Got3id).HasColumnName("GOT3Id");

                entity.Property(e => e.Got4id).HasColumnName("GOT4Id");

                entity.HasOne(d => d.Commision)
                    .WithMany(p => p.TouristGots)
                    .HasForeignKey(d => d.CommisionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TouristGOTs_Commissions");

                entity.HasOne(d => d.Got3)
                    .WithMany(p => p.TouristGots)
                    .HasForeignKey(d => d.Got3id)
                    .HasConstraintName("FK_TouristGOTs_GOT3s");

                entity.HasOne(d => d.Got4)
                    .WithMany(p => p.TouristGots)
                    .HasForeignKey(d => d.Got4id)
                    .HasConstraintName("FK_TouristGOTs_GOT4s");

                entity.HasOne(d => d.Tourist)
                    .WithMany(p => p.TouristGots)
                    .HasForeignKey(d => d.TouristId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TouristGOTs_Tourists");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trips_Routes");

                entity.HasOne(d => d.Tourist)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.TouristId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trips_Tourists");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UsersInTrip>(entity =>
            {
                entity.HasKey(e => new {e.UserId, e.TripId});

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.UsersInTrips)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersInTrips_Trips");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersInTrips)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersInTrips_Users");
            });
        }
    }
}