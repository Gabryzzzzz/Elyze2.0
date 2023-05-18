using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class Attachment_Nullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Attachment_AttachmentId",
                table: "Repository");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Repository",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Attachment_AttachmentId",
                table: "Repository",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Attachment_AttachmentId",
                table: "Repository");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentId",
                table: "Repository",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Attachment_AttachmentId",
                table: "Repository",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
