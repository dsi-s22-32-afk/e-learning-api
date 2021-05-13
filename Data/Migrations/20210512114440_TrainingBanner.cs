using Microsoft.EntityFrameworkCore.Migrations;

namespace UniWall.Migrations.ApiDb
{
    public partial class TrainingBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaximumAttendees",
                table: "Trainings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BannerId",
                table: "Trainings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_BannerId",
                table: "Trainings",
                column: "BannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trainings_UploadedFiles_BannerId",
                table: "Trainings",
                column: "BannerId",
                principalTable: "UploadedFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trainings_UploadedFiles_BannerId",
                table: "Trainings");

            migrationBuilder.DropIndex(
                name: "IX_Trainings_BannerId",
                table: "Trainings");

            migrationBuilder.DropColumn(
                name: "BannerId",
                table: "Trainings");

            migrationBuilder.AlterColumn<int>(
                name: "MaximumAttendees",
                table: "Trainings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
