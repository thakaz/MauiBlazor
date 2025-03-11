using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    /// <inheritdoc />
    public partial class 組織とか追加 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "組織s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    組織コード = table.Column<string>(type: "text", nullable: false),
                    組織名 = table.Column<string>(type: "text", nullable: false),
                    パスワード = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_組織s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "グループs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    グループ名 = table.Column<string>(type: "text", nullable: false),
                    社員Id = table.Column<int>(type: "integer", nullable: true),
                    組織Id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_グループs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_グループs_社員s_社員Id",
                        column: x => x.社員Id,
                        principalTable: "社員s",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_グループs_組織s_組織Id",
                        column: x => x.組織Id,
                        principalTable: "組織s",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_グループs_社員Id",
                table: "グループs",
                column: "社員Id");

            migrationBuilder.CreateIndex(
                name: "IX_グループs_組織Id",
                table: "グループs",
                column: "組織Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "グループs");

            migrationBuilder.DropTable(
                name: "組織s");
        }
    }
}
