using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    /// <inheritdoc />
    public partial class postgresJsonをやめる : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "社員設定",
                table: "社員s");

            migrationBuilder.CreateTable(
                name: "社員設定s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    通知先メールアドレス = table.Column<string>(type: "text", nullable: true),
                    効果音タイプ = table.Column<int>(type: "integer", nullable: false),
                    社員Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_社員設定s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_社員設定s_社員s_社員Id",
                        column: x => x.社員Id,
                        principalTable: "社員s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "社員メモs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    タイトル = table.Column<string>(type: "text", nullable: true),
                    本文 = table.Column<string>(type: "text", nullable: true),
                    社員設定Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_社員メモs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_社員メモs_社員設定s_社員設定Id",
                        column: x => x.社員設定Id,
                        principalTable: "社員設定s",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_社員メモs_社員設定Id",
                table: "社員メモs",
                column: "社員設定Id");

            migrationBuilder.CreateIndex(
                name: "IX_社員設定s_社員Id",
                table: "社員設定s",
                column: "社員Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "社員メモs");

            migrationBuilder.DropTable(
                name: "社員設定s");

            migrationBuilder.AddColumn<string>(
                name: "社員設定",
                table: "社員s",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");
        }
    }
}
