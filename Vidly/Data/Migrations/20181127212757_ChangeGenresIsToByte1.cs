using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class ChangeGenresIsToByte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Movies_Genres_GenreId",
            //    table: "Movies");

           


            //migrationBuilder.DropIndex(
            //    name: "IX_Movies_GenreId",
            //    table: "Movies");

            migrationBuilder.RenameColumn(
              name: "Id",
              table: "Genres",
              newName: "OldId");

            migrationBuilder.DropPrimaryKey(
               name: "PK_Genres",
               table: "Genres");

            //migrationBuilder.AddColumn<byte>(
            //    name: "Id",
            //    table: "Genres",
            //    nullable: false,
            //    defaultValue: 0,
            //    defaultValueSql: null);

            //migrationBuilder.AddPrimaryKey("PK_Genres", "Genres", "Id");


            //migrationBuilder.AddForeignKey(
            //    name: "FK_Movies_Genres_GenreId",
            //    table: "Movies",
            //    column: "GenreId",
            //    principalTable: "Genres",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.DropColumn(
            // name: "OldId",
            // table: "Genres");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Movies_Genres_GenreId",
            //    table: "Movies");

            //migrationBuilder.DropIndex(
            //    name: "IX_Movies_GenreId",
            //    table: "Movies");

            //migrationBuilder.AddColumn<int>(
            //    name: "GenreId1",
            //    table: "Movies",
            //    nullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Genres",
            //    nullable: false,
            //    oldClrType: typeof(byte))
            //    .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Movies_GenreId1",
            //    table: "Movies",
            //    column: "GenreId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Movies_Genres_GenreId1",
            //    table: "Movies",
            //    column: "GenreId1",
            //    principalTable: "Genres",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
