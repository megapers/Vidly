using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class PopulateMoviesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Movies(Name, DateAdded, GenreId, InStock, ReleaseDate) VALUES ('Hangover', '05-04-2018', 2, 5, '11-06-2009')");
            migrationBuilder.Sql("INSERT INTO Movies(Name, DateAdded, GenreId, InStock, ReleaseDate) VALUES ('Die Hard', '03-14-2018', 1, 8, '07-12-1998')");
            migrationBuilder.Sql("INSERT INTO Movies(Name, DateAdded, GenreId, InStock, ReleaseDate) VALUES ('The Terminator', '01-10-2018', 5, 5, '02-10-1993')");
            migrationBuilder.Sql("INSERT INTO Movies(Name, DateAdded, GenreId, InStock, ReleaseDate) VALUES ('Toy Story', '02-02-2018', 3, 3, '09-25-2002')");
            migrationBuilder.Sql("INSERT INTO Movies(Name, DateAdded, GenreId, InStock, ReleaseDate) VALUES ('Titanic', '06-20-2018', 4, 0, '08-20-1999')");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
