using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class ModifyAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Movies SET Available = InStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
