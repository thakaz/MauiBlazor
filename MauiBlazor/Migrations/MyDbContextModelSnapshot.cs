﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MauiBlazor.Migrations
{
    [DbContext(typeof(出退勤DbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("MauiBlazor.Models.社員", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("備考")
                        .HasColumnType("TEXT");

                    b.Property<int>("入社年度")
                        .HasColumnType("INTEGER");

                    b.Property<string>("名前")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("社員番号")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("社員番号")
                        .IsUnique();

                    b.ToTable("社員s", (string)null);
                });

            modelBuilder.Entity("MauiBlazor.Models.社員カード", b =>
                {
                    b.Property<string>("カードUID")
                        .HasColumnType("TEXT");

                    b.Property<string>("カード名称")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("備考")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("社員Id")
                        .HasColumnType("INTEGER");

                    b.Property<string>("社員番号")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("カードUID");

                    b.HasIndex("社員Id");

                    b.ToTable("社員カードs", (string)null);
                });

            modelBuilder.Entity("MauiBlazor.Models.社員打刻", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("備考")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("打刻日")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("打刻時間")
                        .HasColumnType("TEXT");

                    b.Property<string>("社員番号")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("社員打刻s", (string)null);
                });

            modelBuilder.Entity("MauiBlazor.Models.社員カード", b =>
                {
                    b.HasOne("MauiBlazor.Models.社員", "社員")
                        .WithMany("社員カード")
                        .HasForeignKey("社員Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("社員");
                });

            modelBuilder.Entity("MauiBlazor.Models.社員", b =>
                {
                    b.Navigation("社員カード");
                });
#pragma warning restore 612, 618
        }
    }
}
