using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SGMS.Models;

public partial class ManagmentSystemContext : DbContext
{
    public ManagmentSystemContext()
    {
    }

    public ManagmentSystemContext(DbContextOptions<ManagmentSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<Student> Students { get; set; }

//   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//      => optionsBuilder.UseNpgsql("Server=ep-rapid-glitter-a44qnu0w-pooler.us-east-1.aws.neon.tech;Database=MANAGMENT SYSTEM;User Id=\"MANAGMENT SYSTEM_owner\";Password=npg_eBjI3NAdM5OY;SSL Mode=Require");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Courseid).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.Property(e => e.Courseid)
                .HasMaxLength(10)
                .HasColumnName("courseid");
            entity.Property(e => e.Coursename)
                .HasMaxLength(255)
                .HasColumnName("coursename");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Enrollmentid).HasName("enrollments_pkey");

            entity.ToTable("enrollments");

            entity.Property(e => e.Enrollmentid)
                .HasMaxLength(10)
                .HasColumnName("enrollmentid");
            entity.Property(e => e.Courseid)
                .HasMaxLength(10)
                .HasColumnName("courseid");
            entity.Property(e => e.Grade)
                .HasMaxLength(10)
                .HasColumnName("grade");
            entity.Property(e => e.Instructorid)
                .HasMaxLength(10)
                .HasColumnName("instructorid");
            entity.Property(e => e.Studentid)
                .HasMaxLength(10)
                .HasColumnName("studentid");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Courseid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollments_courseid_fkey");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Instructorid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollments_instructorid_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Studentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollments_studentid_fkey");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Instructorid).HasName("instructors_pkey");

            entity.ToTable("instructors");

            entity.Property(e => e.Instructorid)
                .HasMaxLength(10)
                .HasColumnName("instructorid");
            entity.Property(e => e.Instructoremail)
                .HasMaxLength(255)
                .HasColumnName("instructoremail");
            entity.Property(e => e.Instructorname)
                .HasMaxLength(255)
                .HasColumnName("instructorname");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Studentid).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.Studentid)
                .HasMaxLength(10)
                .HasColumnName("studentid");
            entity.Property(e => e.Studentname)
                .HasMaxLength(255)
                .HasColumnName("studentname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
