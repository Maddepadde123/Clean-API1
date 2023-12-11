using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_animalModels",
                table: "animalModels");

            migrationBuilder.RenameTable(
                name: "animalModels",
                newName: "AnimalModels");

            migrationBuilder.AddColumn<string>(
                name: "Breed",
                table: "AnimalModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalModels",
                table: "AnimalModels",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalModels",
                table: "AnimalModels");

            migrationBuilder.DropColumn(
                name: "Breed",
                table: "AnimalModels");

            migrationBuilder.RenameTable(
                name: "AnimalModels",
                newName: "animalModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_animalModels",
                table: "animalModels",
                column: "Id");
        }
    }
}
