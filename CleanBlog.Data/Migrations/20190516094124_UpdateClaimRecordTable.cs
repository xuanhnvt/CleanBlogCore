using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanBlog.Data.Migrations
{
    public partial class UpdateClaimRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_Role_RoleId",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropIndex(
                name: "IX_PermissionRecordClaimRecordMapping_RoleId",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.AddColumn<string>(
                name: "RequiredClaimValue",
                table: "PermissionRecordClaimRecordMapping",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuleToCheckPermission",
                table: "PermissionRecordClaimRecordMapping",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                table: "ClaimRecord",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimValueType",
                table: "ClaimRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredClaimValue",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropColumn(
                name: "RuleToCheckPermission",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "ClaimRecord");

            migrationBuilder.DropColumn(
                name: "ClaimValueType",
                table: "ClaimRecord");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "PermissionRecordClaimRecordMapping",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRecordClaimRecordMapping_RoleId",
                table: "PermissionRecordClaimRecordMapping",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_Role_RoleId",
                table: "PermissionRecordClaimRecordMapping",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
