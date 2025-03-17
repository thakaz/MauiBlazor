using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MauiBlazor.Shared.Migrations
{
    /// <inheritdoc />
    public partial class ニックネーム追加 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "名前",
                table: "社員s",
                newName: "名前性");

            migrationBuilder.AddColumn<string>(
                name: "ニックネーム",
                table: "社員s",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "フリガナ名",
                table: "社員s",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "フリガナ性",
                table: "社員s",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "メールアドレス",
                table: "社員s",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "名前名",
                table: "社員s",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ニックネーム",
                table: "社員s");

            migrationBuilder.DropColumn(
                name: "フリガナ名",
                table: "社員s");

            migrationBuilder.DropColumn(
                name: "フリガナ性",
                table: "社員s");

            migrationBuilder.DropColumn(
                name: "メールアドレス",
                table: "社員s");

            migrationBuilder.DropColumn(
                name: "名前名",
                table: "社員s");

            migrationBuilder.RenameColumn(
                name: "名前性",
                table: "社員s",
                newName: "名前");
        }
    }
}
