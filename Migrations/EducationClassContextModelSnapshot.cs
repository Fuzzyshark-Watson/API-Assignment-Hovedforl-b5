﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDoApi.Models;

#nullable disable

namespace ToDoApi.Migrations
{
    [DbContext(typeof(EducationClassContext))]
    partial class EducationClassContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ToDoApi.Models.Course", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ToDoApi.Models.CourseSubject", b =>
                {
                    b.Property<long>("CourseId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubjectId")
                        .HasColumnType("bigint");

                    b.HasKey("CourseId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("CourseSubject");
                });

            modelBuilder.Entity("ToDoApi.Models.Day", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Days");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClass", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("DayId")
                        .HasColumnType("bigint");

                    b.Property<int>("Hours")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("EducationClasses");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClassDay", b =>
                {
                    b.Property<long>("EducationClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("DayId")
                        .HasColumnType("bigint");

                    b.HasKey("EducationClassId", "DayId");

                    b.HasIndex("DayId");

                    b.ToTable("EducationClassDay");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClassSubject", b =>
                {
                    b.Property<long>("EducationClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("SubjectId")
                        .HasColumnType("bigint");

                    b.HasKey("EducationClassId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("EducationClassSubject");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClassTeacher", b =>
                {
                    b.Property<long>("EducationClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeacherId")
                        .HasColumnType("bigint");

                    b.HasKey("EducationClassId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("EducationClassTeacher");
                });

            modelBuilder.Entity("ToDoApi.Models.Subject", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("ToDoApi.Models.SubjectTeacher", b =>
                {
                    b.Property<long>("SubjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("TeacherId")
                        .HasColumnType("bigint");

                    b.HasKey("SubjectId", "TeacherId");

                    b.HasIndex("TeacherId");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("ToDoApi.Models.Teacher", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ToDoApi.Models.CourseSubject", b =>
                {
                    b.HasOne("ToDoApi.Models.Course", "Course")
                        .WithMany("CourseSubjects")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoApi.Models.Subject", "Subject")
                        .WithMany("CourseSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClass", b =>
                {
                    b.HasOne("ToDoApi.Models.Day", "Day")
                        .WithMany()
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Day");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClassDay", b =>
                {
                    b.HasOne("ToDoApi.Models.Day", "Day")
                        .WithMany("EducationClassDays")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoApi.Models.EducationClass", "EducationClass")
                        .WithMany("EducationClassDays")
                        .HasForeignKey("EducationClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Day");

                    b.Navigation("EducationClass");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClassSubject", b =>
                {
                    b.HasOne("ToDoApi.Models.EducationClass", "EducationClass")
                        .WithMany("EducationClassSubjects")
                        .HasForeignKey("EducationClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ToDoApi.Models.Subject", "Subject")
                        .WithMany("EducationClassSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EducationClass");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClassTeacher", b =>
                {
                    b.HasOne("ToDoApi.Models.EducationClass", "EducationClass")
                        .WithMany("EducationClassTeachers")
                        .HasForeignKey("EducationClassId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ToDoApi.Models.Teacher", "Teacher")
                        .WithMany("EducationClassTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EducationClass");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ToDoApi.Models.SubjectTeacher", b =>
                {
                    b.HasOne("ToDoApi.Models.Subject", "Subject")
                        .WithMany("SubjectTeachers")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ToDoApi.Models.Teacher", "Teacher")
                        .WithMany("SubjectTeachers")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ToDoApi.Models.Course", b =>
                {
                    b.Navigation("CourseSubjects");
                });

            modelBuilder.Entity("ToDoApi.Models.Day", b =>
                {
                    b.Navigation("EducationClassDays");
                });

            modelBuilder.Entity("ToDoApi.Models.EducationClass", b =>
                {
                    b.Navigation("EducationClassDays");

                    b.Navigation("EducationClassSubjects");

                    b.Navigation("EducationClassTeachers");
                });

            modelBuilder.Entity("ToDoApi.Models.Subject", b =>
                {
                    b.Navigation("CourseSubjects");

                    b.Navigation("EducationClassSubjects");

                    b.Navigation("SubjectTeachers");
                });

            modelBuilder.Entity("ToDoApi.Models.Teacher", b =>
                {
                    b.Navigation("EducationClassTeachers");

                    b.Navigation("SubjectTeachers");
                });
#pragma warning restore 612, 618
        }
    }
}
