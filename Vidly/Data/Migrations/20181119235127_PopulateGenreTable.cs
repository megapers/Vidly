using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class PopulateGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Action')");
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Comedy')");
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Family')");
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Romance')");
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Sci-Fi')");
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Fantasy')");
            migrationBuilder.Sql("INSERT INTO Genres(Name) VALUES ('Horror')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
