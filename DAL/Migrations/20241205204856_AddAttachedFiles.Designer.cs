﻿// <auto-generated />
using System;
using DAL.EfCode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MimicWebApi.Migrations
{
    [DbContext(typeof(MimicContext))]
    [Migration("20241205204856_AddAttachedFiles")]
    partial class AddAttachedFiles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.EfClasses.AttachedFile", b =>
                {
                    b.Property<int>("AttachedFileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("AttachedFileId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OwnerId")
                        .HasColumnType("integer");

                    b.Property<string>("OwnerType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("AttachedFileId");

                    b.ToTable("AttachedFiles");
                });

            modelBuilder.Entity("DAL.EfClasses.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("CharacterId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Money")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("RoomId")
                        .HasColumnType("integer");

                    b.Property<int?>("StorageId")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RoomId");

                    b.HasIndex("StorageId")
                        .IsUnique();

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DAL.EfClasses.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("ItemId"));

                    b.Property<int>("CreatorId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("StorageId")
                        .HasColumnType("integer");

                    b.HasKey("ItemId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("StorageId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DAL.EfClasses.ItemProperty", b =>
                {
                    b.Property<int>("ItemPropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("ItemPropertyId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ItemPropertyId");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemProperties");
                });

            modelBuilder.Entity("DAL.EfClasses.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("RoomId"));

                    b.Property<int>("MasterId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("RoomId");

                    b.HasIndex("MasterId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("DAL.EfClasses.RoomStorageRelation", b =>
                {
                    b.Property<int>("RoomStorageRelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("RoomStorageRelationId"));

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<int>("StorageId")
                        .HasColumnType("integer");

                    b.HasKey("RoomStorageRelationId");

                    b.HasIndex("RoomId");

                    b.HasIndex("StorageId");

                    b.ToTable("RoomStorageRelation");
                });

            modelBuilder.Entity("DAL.EfClasses.Storage", b =>
                {
                    b.Property<int>("StorageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("StorageId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("StorageId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("DAL.EfClasses.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("UserId"));

                    b.Property<string>("ExternalUserId")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DAL.EfClasses.Character", b =>
                {
                    b.HasOne("DAL.EfClasses.User", "Creator")
                        .WithMany("Characters")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.EfClasses.Room", "Room")
                        .WithMany("Characters")
                        .HasForeignKey("RoomId");

                    b.HasOne("DAL.EfClasses.Storage", "Storage")
                        .WithOne()
                        .HasForeignKey("DAL.EfClasses.Character", "StorageId");

                    b.Navigation("Creator");

                    b.Navigation("Room");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("DAL.EfClasses.Item", b =>
                {
                    b.HasOne("DAL.EfClasses.User", "Creator")
                        .WithMany("Items")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.EfClasses.Storage", "Storage")
                        .WithMany("Items")
                        .HasForeignKey("StorageId");

                    b.Navigation("Creator");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("DAL.EfClasses.ItemProperty", b =>
                {
                    b.HasOne("DAL.EfClasses.Item", null)
                        .WithMany("Properties")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.EfClasses.Room", b =>
                {
                    b.HasOne("DAL.EfClasses.User", "Master")
                        .WithMany("Rooms")
                        .HasForeignKey("MasterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Master");
                });

            modelBuilder.Entity("DAL.EfClasses.RoomStorageRelation", b =>
                {
                    b.HasOne("DAL.EfClasses.Room", "Room")
                        .WithMany("RoomStorageRelations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.EfClasses.Storage", "Storage")
                        .WithMany("RoomStorageRelations")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("DAL.EfClasses.Item", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("DAL.EfClasses.Room", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("RoomStorageRelations");
                });

            modelBuilder.Entity("DAL.EfClasses.Storage", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("RoomStorageRelations");
                });

            modelBuilder.Entity("DAL.EfClasses.User", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Items");

                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
