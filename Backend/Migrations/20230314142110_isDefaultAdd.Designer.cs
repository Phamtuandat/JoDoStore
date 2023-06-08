﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using App.Data;

#nullable disable

namespace App.Migrations
{
      [DbContext(typeof(DataContext))]
      [Migration("20230314142110_isDefaultAdd")]
      partial class isDefaultAdd
      {
            /// <inheritdoc />
            protected override void BuildTargetModel(ModelBuilder modelBuilder)
            {
#pragma warning disable 612, 618
                  modelBuilder
                      .HasAnnotation("ProductVersion", "7.0.3")
                      .HasAnnotation("Relational:MaxIdentifierLength", 63);

                  NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

                  modelBuilder.Entity("Backend.Models.Identity.Address", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("City")
                          .HasMaxLength(50)
                          .HasColumnType("character varying(50)");

                            b.Property<bool>("IsDefault")
                          .HasColumnType("boolean");

                            b.Property<string>("State")
                          .HasMaxLength(50)
                          .HasColumnType("character varying(50)");

                            b.Property<string>("StreetAddress")
                          .HasMaxLength(100)
                          .HasColumnType("character varying(100)");

                            b.Property<string>("UserId")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.HasIndex("UserId");

                            b.ToTable("Address");
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                      {
                            b.Property<string>("Id")
                          .HasColumnType("text");

                            b.Property<string>("ConcurrencyStamp")
                          .IsConcurrencyToken()
                          .HasColumnType("text");

                            b.Property<string>("Name")
                          .HasMaxLength(256)
                          .HasColumnType("character varying(256)");

                            b.Property<string>("NormalizedName")
                          .HasMaxLength(256)
                          .HasColumnType("character varying(256)");

                            b.HasKey("Id");

                            b.HasIndex("NormalizedName")
                          .IsUnique()
                          .HasDatabaseName("RoleNameIndex");

                            b.ToTable("Roles", (string)null);
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("ClaimType")
                          .HasColumnType("text");

                            b.Property<string>("ClaimValue")
                          .HasColumnType("text");

                            b.Property<string>("RoleId")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.HasIndex("RoleId");

                            b.ToTable("RoleClaims", (string)null);
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("ClaimType")
                          .HasColumnType("text");

                            b.Property<string>("ClaimValue")
                          .HasColumnType("text");

                            b.Property<string>("UserId")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.HasIndex("UserId");

                            b.ToTable("UserClaims", (string)null);
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                      {
                            b.Property<string>("LoginProvider")
                          .HasColumnType("text");

                            b.Property<string>("ProviderKey")
                          .HasColumnType("text");

                            b.Property<string>("ProviderDisplayName")
                          .HasColumnType("text");

                            b.Property<string>("UserId")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("LoginProvider", "ProviderKey");

                            b.HasIndex("UserId");

                            b.ToTable("UserLogins", (string)null);
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                      {
                            b.Property<string>("UserId")
                          .HasColumnType("text");

                            b.Property<string>("RoleId")
                          .HasColumnType("text");

                            b.HasKey("UserId", "RoleId");

                            b.HasIndex("RoleId");

                            b.ToTable("UserRoles", (string)null);
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                      {
                            b.Property<string>("UserId")
                          .HasColumnType("text");

                            b.Property<string>("LoginProvider")
                          .HasColumnType("text");

                            b.Property<string>("Name")
                          .HasColumnType("text");

                            b.Property<string>("Value")
                          .HasColumnType("text");

                            b.HasKey("UserId", "LoginProvider", "Name");

                            b.ToTable("UserTokens", (string)null);
                      });

                  modelBuilder.Entity("ProductTag", b =>
                      {
                            b.Property<int>("ProductsId")
                          .HasColumnType("integer");

                            b.Property<int>("TagsId")
                          .HasColumnType("integer");

                            b.HasKey("ProductsId", "TagsId");

                            b.HasIndex("TagsId");

                            b.ToTable("ProductTag");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.Identity.User", b =>
                      {
                            b.Property<string>("Id")
                          .HasColumnType("text");

                            b.Property<int>("AccessFailedCount")
                          .HasColumnType("integer");

                            b.Property<string>("ConcurrencyStamp")
                          .IsConcurrencyToken()
                          .HasColumnType("text");

                            b.Property<DateTime>("DateTime")
                          .HasColumnType("timestamp with time zone");

                            b.Property<string>("Email")
                          .IsRequired()
                          .HasMaxLength(256)
                          .HasColumnType("character varying(256)");

                            b.Property<bool>("EmailConfirmed")
                          .HasColumnType("boolean");

                            b.Property<string>("FirstName")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("LastName")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<bool>("LockoutEnabled")
                          .HasColumnType("boolean");

                            b.Property<DateTimeOffset?>("LockoutEnd")
                          .HasColumnType("timestamp with time zone");

                            b.Property<string>("NormalizedEmail")
                          .HasMaxLength(256)
                          .HasColumnType("character varying(256)");

                            b.Property<string>("NormalizedUserName")
                          .HasMaxLength(256)
                          .HasColumnType("character varying(256)");

                            b.Property<string>("PasswordHash")
                          .HasColumnType("text");

                            b.Property<string>("PhoneNumber")
                          .HasColumnType("text");

                            b.Property<bool>("PhoneNumberConfirmed")
                          .HasColumnType("boolean");

                            b.Property<string>("SecurityStamp")
                          .HasColumnType("text");

                            b.Property<bool>("TwoFactorEnabled")
                          .HasColumnType("boolean");

                            b.Property<string>("UserName")
                          .HasMaxLength(256)
                          .HasColumnType("character varying(256)");

                            b.HasKey("Id");

                            b.HasIndex("NormalizedEmail")
                          .HasDatabaseName("EmailIndex");

                            b.HasIndex("NormalizedUserName")
                          .IsUnique()
                          .HasDatabaseName("UserNameIndex");

                            b.ToTable("Users", (string)null);
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.OrderModel.Order", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<int>("AddressId")
                          .HasColumnType("integer");

                            b.Property<DateTime>("OrderDate")
                          .HasColumnType("timestamp with time zone");

                            b.Property<decimal>("ShippingCash")
                          .HasColumnType("numeric");

                            b.Property<decimal>("SubtotalPrice")
                          .HasColumnType("numeric");

                            b.Property<decimal>("TotalPrice")
                          .HasColumnType("numeric");

                            b.Property<string>("UserId")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.HasIndex("AddressId");

                            b.HasIndex("UserId");

                            b.ToTable("Order");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.OrderModel.OrderItem", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<int>("OrderId")
                          .HasColumnType("integer");

                            b.Property<int>("ProductId")
                          .HasColumnType("integer");

                            b.Property<int>("Quantity")
                          .HasColumnType("integer");

                            b.Property<decimal>("UnitPrice")
                          .HasColumnType("numeric");

                            b.HasKey("Id");

                            b.HasIndex("OrderId");

                            b.HasIndex("ProductId");

                            b.ToTable("OrderItem");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Brand", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("Description")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.ToTable("Brand");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Category", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("Description")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasMaxLength(100)
                          .HasColumnType("character varying(100)");

                            b.HasKey("Id");

                            b.ToTable("Category");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.ImageCollections", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.ToTable("ImageCollections");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Photo", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<DateTime>("Created")
                          .HasColumnType("timestamp with time zone");

                            b.Property<string>("Description")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<int?>("ImageCollectionsId")
                          .HasColumnType("integer");

                            b.Property<string>("ImageUrl")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<int?>("ProductId")
                          .HasColumnType("integer");

                            b.Property<string>("PublicId")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("Title")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.HasIndex("ImageCollectionsId");

                            b.HasIndex("ProductId");

                            b.ToTable("Photo");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Product", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<int>("BrandId")
                          .HasColumnType("integer");

                            b.Property<int>("CategoryId")
                          .HasColumnType("integer");

                            b.Property<string>("Description")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("NormalizedName")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<int>("Price")
                          .HasColumnType("integer");

                            b.Property<int>("SalePrice")
                          .HasColumnType("integer");

                            b.HasKey("Id");

                            b.HasIndex("BrandId");

                            b.HasIndex("CategoryId");

                            b.ToTable("Product");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Tag", b =>
                      {
                            b.Property<int>("Id")
                          .ValueGeneratedOnAdd()
                          .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                            b.Property<string>("Description")
                          .IsRequired()
                          .HasColumnType("text");

                            b.Property<string>("Name")
                          .IsRequired()
                          .HasColumnType("text");

                            b.HasKey("Id");

                            b.ToTable("Tag");
                      });

                  modelBuilder.Entity("Backend.Models.Identity.Address", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.Identity.User", "User")
                          .WithMany("Addresses")
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.Navigation("User");
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                      {
                            b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                          .WithMany()
                          .HasForeignKey("RoleId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.Identity.User", null)
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.Identity.User", null)
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                      {
                            b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                          .WithMany()
                          .HasForeignKey("RoleId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("gearshop_dotnetapp.Models.Identity.User", null)
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.Identity.User", null)
                          .WithMany()
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("ProductTag", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.Product", null)
                          .WithMany()
                          .HasForeignKey("ProductsId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.Tag", null)
                          .WithMany()
                          .HasForeignKey("TagsId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.OrderModel.Order", b =>
                      {
                            b.HasOne("Backend.Models.Identity.Address", "Address")
                          .WithMany()
                          .HasForeignKey("AddressId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("gearshop_dotnetapp.Models.Identity.User", "User")
                          .WithMany("Orders")
                          .HasForeignKey("UserId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.Navigation("Address");

                            b.Navigation("User");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.OrderModel.OrderItem", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.OrderModel.Order", "Order")
                          .WithMany("OrderItems")
                          .HasForeignKey("OrderId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.Product", "Product")
                          .WithMany()
                          .HasForeignKey("ProductId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.Navigation("Order");

                            b.Navigation("Product");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Photo", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.ImageCollections", "ImageCollections")
                          .WithMany("Thumbnails")
                          .HasForeignKey("ImageCollectionsId")
                          .OnDelete(DeleteBehavior.SetNull);

                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.Product", "Product")
                          .WithMany("Thumbnails")
                          .HasForeignKey("ProductId")
                          .OnDelete(DeleteBehavior.Cascade);

                            b.Navigation("ImageCollections");

                            b.Navigation("Product");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Product", b =>
                      {
                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.Brand", "Brand")
                          .WithMany("Products")
                          .HasForeignKey("BrandId")
                          .OnDelete(DeleteBehavior.Cascade)
                          .IsRequired();

                            b.HasOne("gearshop_dotnetapp.Models.ProductModel.Category", "Category")
                          .WithMany("Products")
                          .HasForeignKey("CategoryId")
                          .OnDelete(DeleteBehavior.SetNull)
                          .IsRequired();

                            b.Navigation("Brand");

                            b.Navigation("Category");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.Identity.User", b =>
                      {
                            b.Navigation("Addresses");

                            b.Navigation("Orders");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.OrderModel.Order", b =>
                      {
                            b.Navigation("OrderItems");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Brand", b =>
                      {
                            b.Navigation("Products");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Category", b =>
                      {
                            b.Navigation("Products");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.ImageCollections", b =>
                      {
                            b.Navigation("Thumbnails");
                      });

                  modelBuilder.Entity("gearshop_dotnetapp.Models.ProductModel.Product", b =>
                      {
                            b.Navigation("Thumbnails");
                      });
#pragma warning restore 612, 618
            }
      }
}
