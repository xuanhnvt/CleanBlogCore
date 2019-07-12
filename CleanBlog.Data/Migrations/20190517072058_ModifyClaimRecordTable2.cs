using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanBlog.Data.Migrations
{
    public partial class ModifyClaimRecordTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_ClaimRecord_ClaimRecordId",
                table: "RoleClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_ClaimRecord_ClaimRecordId",
                table: "UserClaim");

            migrationBuilder.DropIndex(
                name: "IX_UserClaim_ClaimRecordId",
                table: "UserClaim");

            migrationBuilder.DropIndex(
                name: "IX_RoleClaim_ClaimRecordId",
                table: "RoleClaim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_ClaimRecordId",
                table: "UserClaim",
                column: "ClaimRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_ClaimRecordId",
                table: "RoleClaim",
                column: "ClaimRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_ClaimRecord_ClaimRecordId",
                table: "RoleClaim",
                column: "ClaimRecordId",
                principalTable: "ClaimRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_ClaimRecord_ClaimRecordId",
                table: "UserClaim",
                column: "ClaimRecordId",
                principalTable: "ClaimRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
