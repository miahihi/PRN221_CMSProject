using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project.Models
{
    public partial class prn231_finalprojectContext : DbContext
    {
        public prn231_finalprojectContext()
        {
        }

        public prn231_finalprojectContext(DbContextOptions<prn231_finalprojectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assignment> Assignments { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseCategory> CourseCategories { get; set; } = null!;
        public virtual DbSet<Enrollment> Enrollments { get; set; } = null!;
        public virtual DbSet<Submission> Submissions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<WeekLesson> WeekLessons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server =DESKTOP-R5CMR84; database =prn231_finalproject;uid=sa;pwd=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AssignmentFilesize).HasColumnName("assignment_filesize");

                entity.Property(e => e.AssignmentName)
                    .HasMaxLength(250)
                    .HasColumnName("assignment_name");

                entity.Property(e => e.Attachment).HasColumnName("attachment");

                entity.Property(e => e.Wlid).HasColumnName("wlid");

                entity.HasOne(d => d.Wl)
                    .WithMany(p => p.Assignments)
                    .HasForeignKey(d => d.Wlid)
                    .HasConstraintName("FK_Assignments_WeekLesson");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .HasColumnName("code")
                    .IsFixedLength();

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Course_course_category");
            });

            modelBuilder.Entity<CourseCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("course_category");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryName).HasColumnName("category_name");
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.CourseId });

                entity.ToTable("Enrollment");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.EnrollTime)
                    .HasColumnType("date")
                    .HasColumnName("enrollTime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_Course");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Enrollments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Enrollment_User");
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(e => e.Int);

                entity.ToTable("submission");

                entity.Property(e => e.Int).HasColumnName("int");

                entity.Property(e => e.AssignId).HasColumnName("assignId");

                entity.Property(e => e.DueDate)
                    .HasColumnType("date")
                    .HasColumnName("dueDate");

                entity.Property(e => e.LastModifiedTime)
                    .HasColumnType("date")
                    .HasColumnName("lastModifiedTime");

                entity.Property(e => e.UploadTime)
                    .HasColumnType("date")
                    .HasColumnName("uploadTime");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Assign)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.AssignId)
                    .HasConstraintName("FK_submission_Assignments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Submissions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_submission_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Mssv)
                    .HasMaxLength(10)
                    .HasColumnName("mssv")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .HasColumnName("password");

                entity.Property(e => e.Type).HasColumnName("type");
            });

            modelBuilder.Entity<WeekLesson>(entity =>
            {
                entity.ToTable("WeekLesson");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CourseId).HasColumnName("courseId");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.WeekLessons)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_WeekLesson_Course");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
