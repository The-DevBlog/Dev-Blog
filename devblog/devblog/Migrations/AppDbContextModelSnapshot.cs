﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using devblog.Data;

#nullable disable

namespace devblog.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("devblog.Models.CommentModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PostModelId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("devblog.Models.DownVoteModel", b =>
                {
                    b.Property<int>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostModelId", "UserName");

                    b.ToTable("DownVote");
                });

            modelBuilder.Entity("devblog.Models.PostModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImgURL")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UpdateNum")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("devblog.Models.UpVoteModel", b =>
                {
                    b.Property<int>("PostModelId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostModelId", "UserName");

                    b.ToTable("UpVote");
                });

            modelBuilder.Entity("devblog.Models.CommentModel", b =>
                {
                    b.HasOne("devblog.Models.PostModel", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.DownVoteModel", b =>
                {
                    b.HasOne("devblog.Models.PostModel", null)
                        .WithMany("DownVotes")
                        .HasForeignKey("PostModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.UpVoteModel", b =>
                {
                    b.HasOne("devblog.Models.PostModel", null)
                        .WithMany("UpVotes")
                        .HasForeignKey("PostModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.PostModel", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DownVotes");

                    b.Navigation("UpVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
