﻿// <auto-generated />
using ClassLibrary.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASP.NET_Core_Web_API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211207100007_web_api_migration")]
    partial class web_api_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClassLibrary.Models.School", b =>
                {
                    b.Property<int>("SchoolID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SchoolID");

                    b.ToTable("School");
                });

            modelBuilder.Entity("ClassLibrary.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PersonName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SchoolID")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.HasIndex("SchoolID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("ClassLibrary.Models.Employee", b =>
                {
                    b.HasBaseType("ClassLibrary.Person");

                    b.Property<int>("EmployeeType")
                        .HasColumnType("int");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ClassLibrary.Models.Student", b =>
                {
                    b.HasBaseType("ClassLibrary.Person");

                    b.Property<string>("Class")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentHatColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentType")
                        .HasColumnType("int");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("ClassLibrary.Models.EUDStudent", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.Student");

                    b.Property<string>("BusinessEducation")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("EUDStudent");
                });

            modelBuilder.Entity("ClassLibrary.Models.HTXStudent", b =>
                {
                    b.HasBaseType("ClassLibrary.Models.Student");

                    b.Property<string>("ITEducation")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("HTXStudent");
                });

            modelBuilder.Entity("ClassLibrary.Person", b =>
                {
                    b.HasOne("ClassLibrary.Models.School", "School")
                        .WithMany("Persons")
                        .HasForeignKey("SchoolID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("ClassLibrary.Models.Employee", b =>
                {
                    b.HasOne("ClassLibrary.Person", null)
                        .WithOne()
                        .HasForeignKey("ClassLibrary.Models.Employee", "PersonID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibrary.Models.Student", b =>
                {
                    b.HasOne("ClassLibrary.Person", null)
                        .WithOne()
                        .HasForeignKey("ClassLibrary.Models.Student", "PersonID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibrary.Models.EUDStudent", b =>
                {
                    b.HasOne("ClassLibrary.Models.Student", null)
                        .WithOne()
                        .HasForeignKey("ClassLibrary.Models.EUDStudent", "PersonID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibrary.Models.HTXStudent", b =>
                {
                    b.HasOne("ClassLibrary.Models.Student", null)
                        .WithOne()
                        .HasForeignKey("ClassLibrary.Models.HTXStudent", "PersonID")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibrary.Models.School", b =>
                {
                    b.Navigation("Persons");
                });
#pragma warning restore 612, 618
        }
    }
}
