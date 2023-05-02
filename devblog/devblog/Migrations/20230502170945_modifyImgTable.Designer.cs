﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using devblog.Data;

#nullable disable

namespace devblog.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230502170945_modifyImgTable")]
    partial class modifyImgTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("devblog.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("devblog.Models.DownVote", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostId", "UserName");

                    b.ToTable("DownVote");
                });

            modelBuilder.Entity("devblog.Models.Img", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Img");
                });

            modelBuilder.Entity("devblog.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("UpdateNum")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("devblog.Models.UpVote", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(255)");

                    b.HasKey("PostId", "UserName");

                    b.ToTable("UpVote");
                });

            modelBuilder.Entity("devblog.Models.Comment", b =>
                {
                    b.HasOne("devblog.Models.Post", null)
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.DownVote", b =>
                {
                    b.HasOne("devblog.Models.Post", null)
                        .WithMany("DownVotes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.Img", b =>
                {
                    b.HasOne("devblog.Models.Post", null)
                        .WithMany("Imgs")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.UpVote", b =>
                {
                    b.HasOne("devblog.Models.Post", null)
                        .WithMany("UpVotes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("devblog.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DownVotes");

                    b.Navigation("Imgs");

                    b.Navigation("UpVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
