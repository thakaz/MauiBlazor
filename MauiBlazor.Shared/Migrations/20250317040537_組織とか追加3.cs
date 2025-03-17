using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    /// <inheritdoc />
    public partial class 組織とか追加3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Is管理組織",
                table: "組織s",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Is管理者",
                table: "社員s",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_組織s_組織コード",
                table: "組織s",
                column: "組織コード",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_組織s_組織コード",
                table: "組織s");

            migrationBuilder.DropColumn(
                name: "Is管理組織",
                table: "組織s");

            migrationBuilder.DropColumn(
                name: "Is管理者",
                table: "社員s");
        }
    }
}
