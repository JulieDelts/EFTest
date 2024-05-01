﻿// <auto-generated />
using EFRelations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFRelations.Migrations
{
    [DbContext(typeof(BrickContext))]
    [Migration("20240501223226_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BrickTag", b =>
                {
                    b.Property<int>("BricksId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("BricksId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("BrickTag");
                });

            modelBuilder.Entity("EFRelations.BrickAvailability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailableAmount")
                        .HasColumnType("int");

                    b.Property<int>("BrickId")
                        .HasColumnType("int");

                    b.Property<decimal>("PriceEur")
                        .HasColumnType("decimal(8, 2)");

                    b.Property<int>("VendorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrickId");

                    b.HasIndex("VendorId");

                    b.ToTable("BrickAvailabilities");
                });

            modelBuilder.Entity("EFRelations.Models.Brick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Colour")
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Bricks");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Brick");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EFRelations.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("EFRelations.Models.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("EFRelations.Models.BasePlate", b =>
                {
                    b.HasBaseType("EFRelations.Models.Brick");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("BasePlate");
                });

            modelBuilder.Entity("EFRelations.Models.MiniFigureHead", b =>
                {
                    b.HasBaseType("EFRelations.Models.Brick");

                    b.Property<bool>("IsDualSided")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("MiniFigureHead");
                });

            modelBuilder.Entity("BrickTag", b =>
                {
                    b.HasOne("EFRelations.Models.Brick", null)
                        .WithMany()
                        .HasForeignKey("BricksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFRelations.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFRelations.BrickAvailability", b =>
                {
                    b.HasOne("EFRelations.Models.Brick", "Brick")
                        .WithMany("Availability")
                        .HasForeignKey("BrickId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFRelations.Models.Vendor", "Vendor")
                        .WithMany("Availability")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brick");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("EFRelations.Models.Brick", b =>
                {
                    b.Navigation("Availability");
                });

            modelBuilder.Entity("EFRelations.Models.Vendor", b =>
                {
                    b.Navigation("Availability");
                });
#pragma warning restore 612, 618
        }
    }
}
