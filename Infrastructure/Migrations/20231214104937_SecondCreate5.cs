using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalUserModels_AnimalModels_AnimalId",
                table: "AnimalUserModels");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalUserModels_UserModel_UserId",
                table: "AnimalUserModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalUserModels",
                table: "AnimalUserModels");

            migrationBuilder.RenameTable(
                name: "AnimalUserModels",
                newName: "AnimalUsers");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalUserModels_UserId",
                table: "AnimalUsers",
                newName: "IX_AnimalUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalUserModels_AnimalId",
                table: "AnimalUsers",
                newName: "IX_AnimalUsers_AnimalId");

            migrationBuilder.AddColumn<Guid>(
                name: "AnimalUserModelId",
                table: "AnimalUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedProperty",
                table: "AnimalUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalUsers",
                table: "AnimalUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalUsers_AnimalUserModelId",
                table: "AnimalUsers",
                column: "AnimalUserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalUsers_AnimalModels_AnimalId",
                table: "AnimalUsers",
                column: "AnimalId",
                principalTable: "AnimalModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalUsers_AnimalUsers_AnimalUserModelId",
                table: "AnimalUsers",
                column: "AnimalUserModelId",
                principalTable: "AnimalUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalUsers_UserModel_UserId",
                table: "AnimalUsers",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalUsers_AnimalModels_AnimalId",
                table: "AnimalUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalUsers_AnimalUsers_AnimalUserModelId",
                table: "AnimalUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalUsers_UserModel_UserId",
                table: "AnimalUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalUsers",
                table: "AnimalUsers");

            migrationBuilder.DropIndex(
                name: "IX_AnimalUsers_AnimalUserModelId",
                table: "AnimalUsers");

            migrationBuilder.DropColumn(
                name: "AnimalUserModelId",
                table: "AnimalUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedProperty",
                table: "AnimalUsers");

            migrationBuilder.RenameTable(
                name: "AnimalUsers",
                newName: "AnimalUserModels");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalUsers_UserId",
                table: "AnimalUserModels",
                newName: "IX_AnimalUserModels_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalUsers_AnimalId",
                table: "AnimalUserModels",
                newName: "IX_AnimalUserModels_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalUserModels",
                table: "AnimalUserModels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalUserModels_AnimalModels_AnimalId",
                table: "AnimalUserModels",
                column: "AnimalId",
                principalTable: "AnimalModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalUserModels_UserModel_UserId",
                table: "AnimalUserModels",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
