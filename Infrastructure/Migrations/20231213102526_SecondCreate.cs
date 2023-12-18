using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatWeight",
                table: "AnimalModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DogWeight",
                table: "AnimalModels",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatWeight",
                table: "AnimalModels");

            migrationBuilder.DropColumn(
                name: "DogWeight",
                table: "AnimalModels");
        }
    }
}
