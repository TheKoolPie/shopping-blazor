﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shopping.Server.Data;

namespace Shopping.Server.Data.Migrations.ShoppingDb
{
    [DbContext(typeof(ShoppingDbContext))]
    partial class ShoppingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shopping.Shared.Data.ProductCategory", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Shopping.Shared.Data.ProductItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CategoryId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shopping.Shared.Data.ShoppingList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ListDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("Shopping.Shared.Data.UserGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("Shopping.Shared.Data.UserGroupShoppingList", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ShoppingListId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("UserGroupId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingListId");

                    b.HasIndex("UserGroupId");

                    b.ToTable("UserGroupShoppingLists");
                });

            modelBuilder.Entity("Shopping.Shared.Data.ProductItem", b =>
                {
                    b.HasOne("Shopping.Shared.Data.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shopping.Shared.Data.ShoppingList", b =>
                {
                    b.OwnsMany("Shopping.Shared.Data.ShoppingListItem", "Items", b1 =>
                        {
                            b1.Property<string>("ShoppingListId")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.Property<string>("Id")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.Property<float>("Amount")
                                .HasColumnType("float");

                            b1.Property<DateTime?>("CreatedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<bool>("Done")
                                .HasColumnType("tinyint(1)");

                            b1.Property<string>("ProductItemId")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.HasKey("ShoppingListId", "Id");

                            b1.HasIndex("ProductItemId");

                            b1.ToTable("ShoppingListItem");

                            b1.HasOne("Shopping.Shared.Data.ProductItem", "ProductItem")
                                .WithMany()
                                .HasForeignKey("ProductItemId");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingListId");
                        });

                    b.OwnsOne("Shopping.Shared.Model.Account.ShoppingUserModel", "Owner", b1 =>
                        {
                            b1.Property<string>("ShoppingListId")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.Property<string>("Id")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("ShoppingListId");

                            b1.ToTable("ShoppingLists");

                            b1.WithOwner()
                                .HasForeignKey("ShoppingListId");
                        });
                });

            modelBuilder.Entity("Shopping.Shared.Data.UserGroup", b =>
                {
                    b.OwnsMany("Shopping.Shared.Model.Account.ShoppingUserModel", "Members", b1 =>
                        {
                            b1.Property<string>("UserGroupId")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.Property<string>("Id")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.HasKey("UserGroupId", "Id");

                            b1.ToTable("UserGroups_Members");

                            b1.WithOwner()
                                .HasForeignKey("UserGroupId");
                        });

                    b.OwnsOne("Shopping.Shared.Model.Account.ShoppingUserModel", "Owner", b1 =>
                        {
                            b1.Property<string>("UserGroupId")
                                .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                            b1.Property<string>("Id")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("UserGroupId");

                            b1.ToTable("UserGroups");

                            b1.WithOwner()
                                .HasForeignKey("UserGroupId");
                        });
                });

            modelBuilder.Entity("Shopping.Shared.Data.UserGroupShoppingList", b =>
                {
                    b.HasOne("Shopping.Shared.Data.ShoppingList", "ShoppingList")
                        .WithMany()
                        .HasForeignKey("ShoppingListId");

                    b.HasOne("Shopping.Shared.Data.UserGroup", "UserGroup")
                        .WithMany()
                        .HasForeignKey("UserGroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
