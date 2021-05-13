﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniWall.Data.Contexts;

namespace UniWall.Data.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20210509201259_TrainingAddressProblem")]
    partial class TrainingAddressProblem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LecturerSubject", b =>
                {
                    b.Property<int>("LecturersId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.HasKey("LecturersId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("LecturerSubject");
                });

            modelBuilder.Entity("LecturerTraining", b =>
                {
                    b.Property<int>("LecturersId")
                        .HasColumnType("int");

                    b.Property<int>("TrainingsId")
                        .HasColumnType("int");

                    b.HasKey("LecturersId", "TrainingsId");

                    b.HasIndex("TrainingsId");

                    b.ToTable("LecturerTraining");
                });

            modelBuilder.Entity("SubjectTraining", b =>
                {
                    b.Property<int>("SubjectsId")
                        .HasColumnType("int");

                    b.Property<int>("TrainingsId")
                        .HasColumnType("int");

                    b.HasKey("SubjectsId", "TrainingsId");

                    b.HasIndex("TrainingsId");

                    b.ToTable("SubjectTraining");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("BuildingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Attendee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmailSent")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrainingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TrainingId");

                    b.ToTable("Attendees");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvatarId")
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("Lecturers");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IconId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<int>("MaximumAttendees")
                        .HasColumnType("int");

                    b.Property<string>("MeetupUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("UniWall.Data.Entities.UploadedFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Directory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("UploadedFiles");
                });

            modelBuilder.Entity("LecturerSubject", b =>
                {
                    b.HasOne("UniWall.Data.Entities.Lecturer", null)
                        .WithMany()
                        .HasForeignKey("LecturersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniWall.Data.Entities.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LecturerTraining", b =>
                {
                    b.HasOne("UniWall.Data.Entities.Lecturer", null)
                        .WithMany()
                        .HasForeignKey("LecturersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniWall.Data.Entities.Training", null)
                        .WithMany()
                        .HasForeignKey("TrainingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectTraining", b =>
                {
                    b.HasOne("UniWall.Data.Entities.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniWall.Data.Entities.Training", null)
                        .WithMany()
                        .HasForeignKey("TrainingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniWall.Data.Entities.Attendee", b =>
                {
                    b.HasOne("UniWall.Data.Entities.Training", "SelectedTraining")
                        .WithMany("Attendees")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SelectedTraining");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Lecturer", b =>
                {
                    b.HasOne("UniWall.Data.Entities.UploadedFile", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Subject", b =>
                {
                    b.HasOne("UniWall.Data.Entities.UploadedFile", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Training", b =>
                {
                    b.HasOne("UniWall.Data.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("UniWall.Data.Entities.Training", b =>
                {
                    b.Navigation("Attendees");
                });
#pragma warning restore 612, 618
        }
    }
}