﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoelessJoe.DataAccess.DataModels;

#nullable disable

namespace ShoelessJoe.DataAccess.Migrations
{
    [DbContext(typeof(ShoelessJoeContext))]
    partial class ShoelessJoeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PotentialBuyId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("PotentialBuyId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Manufacter", b =>
                {
                    b.Property<int>("ManufacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ManufacterName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ManufacterId");

                    b.HasIndex("UserId");

                    b.ToTable("Manufacters");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Model", b =>
                {
                    b.Property<int>("ModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ManufacterId")
                        .HasColumnType("int");

                    b.Property<string>("ModelName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("ModelId");

                    b.HasIndex("ManufacterId");

                    b.ToTable("Models");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.PotentialBuy", b =>
                {
                    b.Property<int>("PotentialBuyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateSold")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsSold")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<int>("PotentialBuyerUserId")
                        .HasColumnType("int");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("PotentialBuyId");

                    b.HasIndex("PotentialBuyerUserId");

                    b.HasIndex("ShoeId");

                    b.ToTable("PotentialBuys");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Shoe", b =>
                {
                    b.Property<int>("ShoeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<double?>("LeftSize")
                        .HasColumnType("double");

                    b.Property<int>("ModelId")
                        .HasColumnType("int");

                    b.Property<double?>("RightSize")
                        .HasColumnType("double");

                    b.HasKey("ShoeId");

                    b.HasIndex("ModelId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.ShoeImage", b =>
                {
                    b.Property<int>("ShoeImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LeftShoeImage1")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("");

                    b.Property<string>("LeftShoeImage2")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("");

                    b.Property<string>("RightShoeImage1")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("");

                    b.Property<string>("RightShoeImage2")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasDefaultValue("");

                    b.Property<int>("ShoeId")
                        .HasColumnType("int");

                    b.HasKey("ShoeImageId");

                    b.HasIndex("ShoeId")
                        .IsUnique();

                    b.ToTable("ShoeImages");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("PhoneNumb")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Comment", b =>
                {
                    b.HasOne("ShoelessJoe.DataAccess.DataModels.PotentialBuy", "PotentialBuy")
                        .WithMany("Comments")
                        .HasForeignKey("PotentialBuyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoe.DataAccess.DataModels.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PotentialBuy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Manufacter", b =>
                {
                    b.HasOne("ShoelessJoe.DataAccess.DataModels.User", "User")
                        .WithMany("Manufacters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Model", b =>
                {
                    b.HasOne("ShoelessJoe.DataAccess.DataModels.Manufacter", "Manufacter")
                        .WithMany("Models")
                        .HasForeignKey("ManufacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacter");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.PotentialBuy", b =>
                {
                    b.HasOne("ShoelessJoe.DataAccess.DataModels.User", "PotentialBuyer")
                        .WithMany("PotentialBuys")
                        .HasForeignKey("PotentialBuyerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoelessJoe.DataAccess.DataModels.Shoe", "Shoe")
                        .WithMany("PotentialBuys")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PotentialBuyer");

                    b.Navigation("Shoe");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Shoe", b =>
                {
                    b.HasOne("ShoelessJoe.DataAccess.DataModels.Model", "Model")
                        .WithMany("Shoes")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.ShoeImage", b =>
                {
                    b.HasOne("ShoelessJoe.DataAccess.DataModels.Shoe", "Shoe")
                        .WithOne("ShoeImage")
                        .HasForeignKey("ShoelessJoe.DataAccess.DataModels.ShoeImage", "ShoeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shoe");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Manufacter", b =>
                {
                    b.Navigation("Models");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Model", b =>
                {
                    b.Navigation("Shoes");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.PotentialBuy", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.Shoe", b =>
                {
                    b.Navigation("PotentialBuys");

                    b.Navigation("ShoeImage");
                });

            modelBuilder.Entity("ShoelessJoe.DataAccess.DataModels.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Manufacters");

                    b.Navigation("PotentialBuys");
                });
#pragma warning restore 612, 618
        }
    }
}
