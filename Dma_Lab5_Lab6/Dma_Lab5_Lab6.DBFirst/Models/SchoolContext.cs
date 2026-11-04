using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dma_Lab5_Lab6.DBFirst.Models;

public partial class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Learner> Learners { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseID).ValueGeneratedNever();
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("Enrollment");

            entity.HasIndex(e => e.CourseID, "IX_Enrollment_CourseID");

            entity.HasIndex(e => e.LearnerID, "IX_Enrollment_LearnerID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments).HasForeignKey(d => d.CourseID);

            entity.HasOne(d => d.Learner).WithMany(p => p.Enrollments).HasForeignKey(d => d.LearnerID);
        });

        modelBuilder.Entity<Learner>(entity =>
        {
            entity.ToTable("Learner");

            entity.HasIndex(e => e.MajorID, "IX_Learner_MajorID");

            entity.HasOne(d => d.Major).WithMany(p => p.Learners).HasForeignKey(d => d.MajorID);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
