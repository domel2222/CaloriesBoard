﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyCaloriesBoards.Enteties;

#nullable disable

namespace MyCaloriesBoards.Migrations
{
    [DbContext(typeof(CaloriesContext))]
    [Migration("20220901075211_Addcoordinate")]
    partial class Addcoordinate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyBoards.Enteties.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(200)");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Calories")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(350);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StateMealId")
                        .HasColumnType("int");

                    b.Property<string>("WeeklyPath")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Weekly_Path");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("StateMealId");

                    b.ToTable("Meals");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Meal");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .ValueGeneratedOnUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("CommentId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("MealId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.MealTag", b =>
                {
                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublicationDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getutcdate()");

                    b.HasKey("TagId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("MealTag");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.StateMeal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("StateMeals");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = "To Do"
                        },
                        new
                        {
                            Id = 2,
                            Value = "Eating"
                        },
                        new
                        {
                            Id = 3,
                            Value = "Done"
                        });
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = "Brakfast"
                        },
                        new
                        {
                            Id = 2,
                            Value = "Lunch"
                        },
                        new
                        {
                            Id = 3,
                            Value = "Cocktail"
                        },
                        new
                        {
                            Id = 4,
                            Value = "Dinner"
                        },
                        new
                        {
                            Id = 5,
                            Value = "Snack"
                        });
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.ViewModels.TopAuthor", b =>
                {
                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Meal")
                        .HasColumnType("int");

                    b.ToView("View_TopAuthors");
                });

            modelBuilder.Entity("MyBoards.Enteties.Activity", b =>
                {
                    b.HasBaseType("MyBoards.Enteties.Meal");

                    b.Property<string>("ActivityKind")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("BurnCalories")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.Property<decimal>("RemainingCalories")
                        .HasPrecision(14, 2)
                        .HasColumnType("decimal(14,2)");

                    b.HasDiscriminator().HasValue("Activity");
                });

            modelBuilder.Entity("MyBoards.Enteties.DailyCalories", b =>
                {
                    b.HasBaseType("MyBoards.Enteties.Meal");

                    b.Property<decimal>("DailyCaloriesIntake")
                        .HasColumnType("decimal(7,2)");

                    b.HasDiscriminator().HasValue("DailyCalories");
                });

            modelBuilder.Entity("MyBoards.Enteties.PeriodOfTime", b =>
                {
                    b.HasBaseType("MyBoards.Enteties.Meal");

                    b.Property<DateTime?>("EndDate")
                        .HasPrecision(3)
                        .HasColumnType("datetime2(3)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("PeriodOfTime");
                });

            modelBuilder.Entity("MyBoards.Enteties.Meal", b =>
                {
                    b.HasOne("MyCaloriesBoards.Enteties.User", "Author")
                        .WithMany("Meals")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyCaloriesBoards.Enteties.StateMeal", "StateMeal")
                        .WithMany("Meals")
                        .HasForeignKey("StateMealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("StateMeal");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.Address", b =>
                {
                    b.HasOne("MyCaloriesBoards.Enteties.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("MyCaloriesBoards.Enteties.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("MyCaloriesBoards.Enteties.Coordinate", "Coordinate", b1 =>
                        {
                            b1.Property<Guid>("AddressId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Latiitude")
                                .HasPrecision(18, 7)
                                .HasColumnType("decimal(18,7)");

                            b1.Property<decimal?>("Longitude")
                                .HasPrecision(18, 7)
                                .HasColumnType("decimal(18,7)");

                            b1.HasKey("AddressId");

                            b1.ToTable("Addresses");

                            b1.WithOwner()
                                .HasForeignKey("AddressId");
                        });

                    b.Navigation("Coordinate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.Comment", b =>
                {
                    b.HasOne("MyCaloriesBoards.Enteties.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MyBoards.Enteties.Meal", "Meal")
                        .WithMany("Comments")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.MealTag", b =>
                {
                    b.HasOne("MyBoards.Enteties.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyCaloriesBoards.Enteties.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("MyBoards.Enteties.Meal", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.StateMeal", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("MyCaloriesBoards.Enteties.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Comments");

                    b.Navigation("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
