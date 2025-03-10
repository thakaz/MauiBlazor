using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    /// <inheritdoc />
    public partial class postgreJson列の追加 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "社員設定",
                table: "社員s",
                type: "jsonb",
                nullable: false,
                defaultValue: "{}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "社員設定",
                table: "社員s");
        }
    }
}
