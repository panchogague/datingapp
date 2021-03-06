﻿// <auto-generated />
using System;
using DateApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DateApp.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DateApp.API.Models.LikeModel", b =>
                {
                    b.Property<int>("LikerID");

                    b.Property<int>("LikeeID");

                    b.HasKey("LikerID", "LikeeID");

                    b.HasIndex("LikeeID");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("DateApp.API.Models.PhotoModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<string>("FireBaseID");

                    b.Property<bool>("IsMain");

                    b.Property<string>("Url");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DateApp.API.Models.UserModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Gender");

                    b.Property<string>("Interests");

                    b.Property<string>("Introduction");

                    b.Property<string>("KnowAs");

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("LookingFor");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DateApp.API.Models.ValuesModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Values");
                });

            modelBuilder.Entity("DateApp.API.Models.LikeModel", b =>
                {
                    b.HasOne("DateApp.API.Models.UserModel", "Likee")
                        .WithMany("Likers")
                        .HasForeignKey("LikeeID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DateApp.API.Models.UserModel", "Liker")
                        .WithMany("Likees")
                        .HasForeignKey("LikerID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("DateApp.API.Models.PhotoModel", b =>
                {
                    b.HasOne("DateApp.API.Models.UserModel", "User")
                        .WithMany("Photos")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
