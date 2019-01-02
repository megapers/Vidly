using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class ChangeGenresIsToByte2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Id",
                table: "Genres",
                nullable: false,
                defaultValue: 0,
                defaultValueSql: null);

            //migrationBuilder.DropIndex(
            //   name: "PK_Genres",
            //   table: "Genres");

            //migrationBuilder.AddPrimaryKey("PK_Genres", "Genres", "Id");


            //migrationBuilder.AddForeignKey(
            //    name: "FK_Movies_Genres_GenreId",
            //    table: "Movies",
            //    column: "GenreId",
            //    principalTable: "Genres",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropColumn(
             name: "OldId",
             table: "Genres");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
