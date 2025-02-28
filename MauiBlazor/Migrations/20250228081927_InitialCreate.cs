using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiBlazor.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "社員s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    社員番号 = table.Column<string>(type: "TEXT", nullable: false),
                    入社年度 = table.Column<int>(type: "INTEGER", nullable: false),
                    名前 = table.Column<string>(type: "TEXT", nullable: false),
                    備考 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_社員s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "社員打刻s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    社員番号 = table.Column<string>(type: "TEXT", nullable: false),
                    打刻時間 = table.Column<DateTime>(type: "TEXT", nullable: false),
                    打刻日 = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    備考 = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_社員打刻s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "社員カードs",
                columns: table => new
                {
                    カードUID = table.Column<string>(type: "TEXT", nullable: false),
                    社員番号 = table.Column<string>(type: "TEXT", nullable: false),
                    カード名称 = table.Column<string>(type: "TEXT", nullable: false),
                    備考 = table.Column<string>(type: "TEXT", nullable: false),
                    社員Id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_社員カードs", x => x.カードUID);
                    table.ForeignKey(
                        name: "FK_社員カードs_社員s_社員Id",
                        column: x => x.社員Id,
                        principalTable: "社員s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_社員s_社員番号",
                table: "社員s",
                column: "社員番号",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_社員カードs_社員Id",
                table: "社員カードs",
                column: "社員Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "社員カードs");

            migrationBuilder.DropTable(
                name: "社員打刻s");

            migrationBuilder.DropTable(
                name: "社員s");
        }
    }
}
