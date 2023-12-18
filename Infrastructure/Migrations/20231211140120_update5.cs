using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Dog_Breed",
                table: "AnimalModels",
                newName: "DogBreed");

            migrationBuilder.RenameColumn(
                name: "Breed",
                table: "AnimalModels",
                newName: "CatBreed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DogBreed",
                table: "AnimalModels",
                newName: "Dog_Breed");

            migrationBuilder.RenameColumn(
                name: "CatBreed",
                table: "AnimalModels",
                newName: "Breed");
        }
    }
}
