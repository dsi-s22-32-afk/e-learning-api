using Microsoft.EntityFrameworkCore.Migrations;

namespace UniWall.Data.Migrations
{
    public partial class FileComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Directory",
                table: "UploadedFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "UploadedFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MimeType",
                table: "UploadedFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "UploadedFiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Directory",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "MimeType",
                table: "UploadedFiles");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "UploadedFiles");
        }
    }
}
