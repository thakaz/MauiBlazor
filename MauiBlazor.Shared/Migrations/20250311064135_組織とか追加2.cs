using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    /// <inheritdoc />
    public partial class 組織とか追加2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "組織Id",
                table: "社員s",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_社員s_組織Id",
                table: "社員s",
                column: "組織Id");

            migrationBuilder.AddForeignKey(
                name: "FK_社員s_組織s_組織Id",
                table: "社員s",
                column: "組織Id",
                principalTable: "組織s",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_社員s_組織s_組織Id",
                table: "社員s");

            migrationBuilder.DropIndex(
                name: "IX_社員s_組織Id",
                table: "社員s");

            migrationBuilder.DropColumn(
                name: "組織Id",
                table: "社員s");
        }
    }
}
