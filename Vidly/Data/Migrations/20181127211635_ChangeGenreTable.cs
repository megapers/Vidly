using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class ChangeGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
               name: "Id",
               table: "Genres",
               nullable: false,
               oldClrType: typeof(byte));

            migrationBuilder.DropForeignKey(
               name: "FK_Movies_Genres_GenreId1",
               table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_GenreId1",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenreId1",
                table: "Movies");

            migrationBuilder.AlterColumn<int>(
               name: "GenreId",
               table: "Movies",
               nullable: false,
               oldClrType: typeof(byte));


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
