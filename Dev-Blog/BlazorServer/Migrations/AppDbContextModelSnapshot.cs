﻿// <auto-generated />
using System;
using BlazorServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlazorServer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BlazorServer.Models.DownVoteModel", b =>
                {
                    b.Property<int>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("PostModelId", "UserName");

                    b.ToTable("DownVote");
                });

            modelBuilder.Entity("BlazorServer.Models.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ImgURL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UpdateNum")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BlazorServer.Models.UpVoteModel", b =>
                {
                    b.Property<int>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("PostModelId", "UserName");

                    b.ToTable("UpVote");
                });

            modelBuilder.Entity("BlazorServer.Models.CommentModel", b =>
                {
                    b.HasOne("BlazorServer.Models.PostModel", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlazorServer.Models.DownVoteModel", b =>
                {
                    b.HasOne("BlazorServer.Models.PostModel", "Post")
                        .WithMany("DownVotes")
                        .HasForeignKey("PostModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlazorServer.Models.UpVoteModel", b =>
                {
                    b.HasOne("BlazorServer.Models.PostModel", "Post")
                        .WithMany("UpVotes")
                        .HasForeignKey("PostModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("BlazorServer.Models.PostModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DownVotes");

                    b.Navigation("UpVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
