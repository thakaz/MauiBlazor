﻿// <auto-generated />
using System;
using MauiBlazor.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    [DbContext(typeof(出退勤DbContext))]
    [Migration("20250311063446_組織とか追加")]
    partial class 組織とか追加
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MauiBlazor.Shared.Models.グループ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("グループ名")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("社員Id")
                        .HasColumnType("integer");

                    b.Property<int?>("組織Id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("社員Id");

                    b.HasIndex("組織Id");

                    b.ToTable("グループs");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("備考")
                        .HasColumnType("text");

                    b.Property<int>("入社年度")
                        .HasColumnType("integer");

                    b.Property<string>("名前")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("社員番号")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("社員番号")
                        .IsUnique();

                    b.ToTable("社員s");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員カード", b =>
                {
                    b.Property<string>("カードUID")
                        .HasColumnType("text");

                    b.Property<string>("カード名称")
                        .HasColumnType("text");

                    b.Property<string>("備考")
                        .HasColumnType("text");

                    b.Property<int>("社員Id")
                        .HasColumnType("integer");

                    b.Property<string>("社員番号")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("追加日")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("カードUID");

                    b.HasIndex("社員Id");

                    b.ToTable("社員カードs");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員メモ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("タイトル")
                        .HasColumnType("text");

                    b.Property<string>("本文")
                        .HasColumnType("text");

                    b.Property<int>("社員設定Id")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("社員設定Id");

                    b.ToTable("社員メモs");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員打刻", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("備考")
                        .HasColumnType("text");

                    b.Property<DateOnly>("打刻日")
                        .HasColumnType("date");

                    b.Property<DateTime>("打刻時間")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("社員番号")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("社員打刻s");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員設定", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("効果音タイプ")
                        .HasColumnType("integer");

                    b.Property<int>("社員Id")
                        .HasColumnType("integer");

                    b.Property<string>("通知先メールアドレス")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("社員Id")
                        .IsUnique();

                    b.ToTable("社員設定s");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.組織", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("パスワード")
                        .HasColumnType("text");

                    b.Property<string>("組織コード")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("組織名")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("組織s");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.グループ", b =>
                {
                    b.HasOne("MauiBlazor.Shared.Models.社員", null)
                        .WithMany("グループ")
                        .HasForeignKey("社員Id");

                    b.HasOne("MauiBlazor.Shared.Models.組織", null)
                        .WithMany("グループ")
                        .HasForeignKey("組織Id");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員カード", b =>
                {
                    b.HasOne("MauiBlazor.Shared.Models.社員", "社員")
                        .WithMany("社員カード")
                        .HasForeignKey("社員Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("社員");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員メモ", b =>
                {
                    b.HasOne("MauiBlazor.Shared.Models.社員設定", "社員設定")
                        .WithMany("メモ")
                        .HasForeignKey("社員設定Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("社員設定");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員設定", b =>
                {
                    b.HasOne("MauiBlazor.Shared.Models.社員", "社員")
                        .WithOne("社員設定")
                        .HasForeignKey("MauiBlazor.Shared.Models.社員設定", "社員Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("社員");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員", b =>
                {
                    b.Navigation("グループ");

                    b.Navigation("社員カード");

                    b.Navigation("社員設定");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.社員設定", b =>
                {
                    b.Navigation("メモ");
                });

            modelBuilder.Entity("MauiBlazor.Shared.Models.組織", b =>
                {
                    b.Navigation("グループ");
                });
#pragma warning restore 612, 618
        }
    }
}
