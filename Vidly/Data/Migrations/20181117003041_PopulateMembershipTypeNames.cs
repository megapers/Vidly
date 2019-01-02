using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class PopulateMembershipTypeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Pay as You Go' WHERE DurationInMonths = 0");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Monthly' WHERE DurationInMonths = 1");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Quarterly' WHERE DurationInMonths = 3");
            migrationBuilder.Sql("UPDATE MembershipType SET Name = 'Annual' WHERE DurationInMonths = 12");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
