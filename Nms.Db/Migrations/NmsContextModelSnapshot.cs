﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nms.Db;

#nullable disable

namespace Nms.Db.Migrations
{
    [DbContext(typeof(NmsContext))]
    partial class NmsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Nms.Db.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Calories")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Carbohydrates")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Fats")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Protein")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("Nms.Db.Entities.MealPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("MealPlans");
                });

            modelBuilder.Entity("Nms.Db.Entities.MealPlanItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("MealPlanId");

                    b.ToTable("MealPlanItems");
                });

            modelBuilder.Entity("Nms.Db.Entities.ScheduledMealPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealPlanId");

                    b.ToTable("ScheduledMealPlans");
                });

            modelBuilder.Entity("Nms.Db.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nms.Db.Entities.Food", b =>
                {
                    b.HasOne("Nms.Db.Entities.User", "User")
                        .WithMany("Foods")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nms.Db.Entities.MealPlan", b =>
                {
                    b.HasOne("Nms.Db.Entities.User", "User")
                        .WithMany("MealPlans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nms.Db.Entities.MealPlanItem", b =>
                {
                    b.HasOne("Nms.Db.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nms.Db.Entities.MealPlan", "MealPlan")
                        .WithMany("MealPlanItems")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("MealPlan");
                });

            modelBuilder.Entity("Nms.Db.Entities.ScheduledMealPlan", b =>
                {
                    b.HasOne("Nms.Db.Entities.MealPlan", "MealPlan")
                        .WithMany()
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealPlan");
                });

            modelBuilder.Entity("Nms.Db.Entities.MealPlan", b =>
                {
                    b.Navigation("MealPlanItems");
                });

            modelBuilder.Entity("Nms.Db.Entities.User", b =>
                {
                    b.Navigation("Foods");

                    b.Navigation("MealPlans");
                });
#pragma warning restore 612, 618
        }
    }
}
