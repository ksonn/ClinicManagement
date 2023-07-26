using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace Clinic.Models
{
    public partial class Clinic11Context : DbContext
    {
        public Clinic11Context()
        {
        }

        public Clinic11Context(DbContextOptions<Clinic11Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<DonThuoc> DonThuocs { get; set; } = null!;
        public virtual DbSet<LoaiThuoc> LoaiThuocs { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Treatment> Treatments { get; set; } = null!;
        public virtual DbSet<TuThuoc> TuThuocs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                if (!optionsBuilder.IsConfigured)
                {
                    var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                    optionsBuilder.UseSqlServer(config.GetConnectionString("AppDBConStr"));
                }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_Accounts");

                entity.ToTable("Account");

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<DonThuoc>(entity =>
            {
                entity.ToTable("DonThuoc");

                entity.Property(e => e.DonThuocId)
                    .ValueGeneratedNever()
                    .HasColumnName("DonThuocID");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.ThuocId).HasColumnName("ThuocID");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.DonThuocs)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonThuoc_Patient");

                entity.HasOne(d => d.Thuoc)
                    .WithMany(p => p.DonThuocs)
                    .HasForeignKey(d => d.ThuocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonThuoc_TuThuoc");
            });

            modelBuilder.Entity<LoaiThuoc>(entity =>
            {
                entity.HasKey(e => e.LoaiId);

                entity.ToTable("LoaiThuoc");

                entity.Property(e => e.LoaiId)
                    .ValueGeneratedNever()
                    .HasColumnName("LoaiID");

                entity.Property(e => e.TenLoai).HasMaxLength(100);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(e => e.Pid);

                entity.ToTable("Patient");

                entity.Property(e => e.Pid)
                    .ValueGeneratedNever()
                    .HasColumnName("pid");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(50)
                    .HasColumnName("cmnd");

                entity.Property(e => e.Dayadd)
                    .HasColumnType("date")
                    .HasColumnName("dayadd");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Job)
                    .HasMaxLength(50)
                    .HasColumnName("job");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Photo)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.Pname)
                    .HasMaxLength(50)
                    .HasColumnName("pname");

                entity.Property(e => e.Reason)
                    .HasMaxLength(3000)
                    .HasColumnName("reason");

                entity.HasMany(d => d.Sids)
                    .WithMany(p => p.Pids)
                    .UsingEntity<Dictionary<string, object>>(
                        "PatientService",
                        l => l.HasOne<Service>().WithMany().HasForeignKey("Sid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Patient_Service_Service"),
                        r => r.HasOne<Patient>().WithMany().HasForeignKey("Pid").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Patient_Service_Patient"),
                        j =>
                        {
                            j.HasKey("Pid", "Sid");

                            j.ToTable("Patient_Service");

                            j.IndexerProperty<int>("Pid").HasColumnName("pid");

                            j.IndexerProperty<int>("Sid").HasColumnName("sid");
                        });
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Sid);

                entity.ToTable("Service");

                entity.Property(e => e.Sid)
                    .ValueGeneratedNever()
                    .HasColumnName("sid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Sname)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("sname");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasKey(e => e.Tid);

                entity.ToTable("Treatment");

                entity.Property(e => e.Tid)
                    .ValueGeneratedNever()
                    .HasColumnName("tid");

                entity.Property(e => e.Diagnose)
                    .HasMaxLength(2500)
                    .HasColumnName("diagnose");

                entity.Property(e => e.Solution)
                    .HasMaxLength(2500)
                    .HasColumnName("solution");

                entity.Property(e => e.Username)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.TidNavigation)
                    .WithOne(p => p.Treatment)
                    .HasForeignKey<Treatment>(d => d.Tid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Treatment_Patient");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_Treatment_Account");
            });

            modelBuilder.Entity<TuThuoc>(entity =>
            {
                entity.HasKey(e => e.ThuocId);

                entity.ToTable("TuThuoc");

                entity.Property(e => e.ThuocId)
                    .ValueGeneratedNever()
                    .HasColumnName("ThuocID");

                entity.Property(e => e.HanSx)
                    .HasColumnType("date")
                    .HasColumnName("HanSX");

                entity.Property(e => e.HangSanXuat).HasMaxLength(100);

                entity.Property(e => e.LoaiId).HasColumnName("LoaiID");

                entity.Property(e => e.NgaySx)
                    .HasColumnType("date")
                    .HasColumnName("NgaySX");

                entity.Property(e => e.TenThuoc).HasMaxLength(100);

                entity.HasOne(d => d.Loai)
                    .WithMany(p => p.TuThuocs)
                    .HasForeignKey(d => d.LoaiId)
                    .HasConstraintName("FK_TuThuoc_LoaiThuoc");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
