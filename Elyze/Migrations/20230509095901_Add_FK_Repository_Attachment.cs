using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elyze.Migrations
{
    public partial class Add_FK_Repository_Attachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Attachment_SocietaId",
                table: "Repository");

            migrationBuilder.CreateIndex(
                name: "IX_Repository_AttachmentId",
                table: "Repository",
                column: "AttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Attachment_AttachmentId",
                table: "Repository",
                column: "AttachmentId",
                principalTable: "Attachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repository_Attachment_AttachmentId",
                table: "Repository");

            migrationBuilder.DropIndex(
                name: "IX_Repository_AttachmentId",
                table: "Repository");

            migrationBuilder.AddForeignKey(
                name: "FK_Repository_Attachment_SocietaId",
                table: "Repository",
                column: "SocietaId",
                principalTable: "Attachment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
