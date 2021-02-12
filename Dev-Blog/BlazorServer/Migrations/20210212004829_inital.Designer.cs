﻿// <auto-generated />
using System;
using BlazorServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210212004829_inital")]
    partial class inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("BlazorServer.Models.CommentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BlazorServer.Models.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("DownVotes")
                        .HasColumnType("int");

                    b.Property<string>("ImgURL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UpVotes")
                        .HasColumnType("int");

                    b.Property<string>("UpdateNum")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BlazorServer.Models.VoteModel", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<bool>("DownVote")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("UpVote")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("PostId", "UserId");

                    b.ToTable("Vote");
                });

            modelBuilder.Entity("BlazorServer.Models.CommentModel", b =>
                {
                    b.HasOne("BlazorServer.Models.PostModel", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostModelId");
                });

            modelBuilder.Entity("BlazorServer.Models.VoteModel", b =>
                {
                    b.HasOne("BlazorServer.Models.PostModel", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlazorServer.Models.PostModel", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}