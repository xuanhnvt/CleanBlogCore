using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanBlog.Data.Migrations
{
    public partial class AddClaimRecordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecord_Role_Mapping_ClaimRecord_ClaimRecord_Id",
                table: "PermissionRecord_Role_Mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecord_Role_Mapping_PermissionRecord_PermissionRecord_Id",
                table: "PermissionRecord_Role_Mapping");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecord_Role_Mapping_Role_RoleId",
                table: "PermissionRecord_Role_Mapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionRecord_Role_Mapping",
                table: "PermissionRecord_Role_Mapping");

            migrationBuilder.RenameTable(
                name: "PermissionRecord_Role_Mapping",
                newName: "PermissionRecordClaimRecordMapping");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionRecord_Role_Mapping_RoleId",
                table: "PermissionRecordClaimRecordMapping",
                newName: "IX_PermissionRecordClaimRecordMapping_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionRecord_Role_Mapping_PermissionRecord_Id",
                table: "PermissionRecordClaimRecordMapping",
                newName: "IX_PermissionRecordClaimRecordMapping_PermissionRecord_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionRecord_Role_Mapping_ClaimRecord_Id",
                table: "PermissionRecordClaimRecordMapping",
                newName: "IX_PermissionRecordClaimRecordMapping_ClaimRecord_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionRecordClaimRecordMapping",
                table: "PermissionRecordClaimRecordMapping",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_ClaimRecord_ClaimRecord_Id",
                table: "PermissionRecordClaimRecordMapping",
                column: "ClaimRecord_Id",
                principalTable: "ClaimRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_PermissionRecord_PermissionRecord_Id",
                table: "PermissionRecordClaimRecordMapping",
                column: "PermissionRecord_Id",
                principalTable: "PermissionRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_Role_RoleId",
                table: "PermissionRecordClaimRecordMapping",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_ClaimRecord_ClaimRecord_Id",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_PermissionRecord_PermissionRecord_Id",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionRecordClaimRecordMapping_Role_RoleId",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionRecordClaimRecordMapping",
                table: "PermissionRecordClaimRecordMapping");

            migrationBuilder.RenameTable(
                name: "PermissionRecordClaimRecordMapping",
                newName: "PermissionRecord_Role_Mapping");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionRecordClaimRecordMapping_RoleId",
                table: "PermissionRecord_Role_Mapping",
                newName: "IX_PermissionRecord_Role_Mapping_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionRecordClaimRecordMapping_PermissionRecord_Id",
                table: "PermissionRecord_Role_Mapping",
                newName: "IX_PermissionRecord_Role_Mapping_PermissionRecord_Id");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionRecordClaimRecordMapping_ClaimRecord_Id",
                table: "PermissionRecord_Role_Mapping",
                newName: "IX_PermissionRecord_Role_Mapping_ClaimRecord_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionRecord_Role_Mapping",
                table: "PermissionRecord_Role_Mapping",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecord_Role_Mapping_ClaimRecord_ClaimRecord_Id",
                table: "PermissionRecord_Role_Mapping",
                column: "ClaimRecord_Id",
                principalTable: "ClaimRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecord_Role_Mapping_PermissionRecord_PermissionRecord_Id",
                table: "PermissionRecord_Role_Mapping",
                column: "PermissionRecord_Id",
                principalTable: "PermissionRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionRecord_Role_Mapping_Role_RoleId",
                table: "PermissionRecord_Role_Mapping",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
