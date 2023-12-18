using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalUserModels_AnimalUserModels_AnimalUserModelId",
                table: "AnimalUserModels");

            migrationBuilder.DropIndex(
                name: "IX_AnimalUserModels_AnimalUserModelId",
                table: "AnimalUserModels");

            migrationBuilder.DropColumn(
                name: "AnimalUserModelId",
                table: "AnimalUserModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnimalUserModelId",
                table: "AnimalUserModels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnimalUserModels_AnimalUserModelId",
                table: "AnimalUserModels",
                column: "AnimalUserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalUserModels_AnimalUserModels_AnimalUserModelId",
                table: "AnimalUserModels",
                column: "AnimalUserModelId",
                principalTable: "AnimalUserModels",
                principalColumn: "Id");
        }
    }
}
