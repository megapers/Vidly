﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Vidly.Data.Migrations
{
    public partial class ChangeCustomersBirthDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Customers SET BirthDate = '1980-01-01' WHERE Id = 1");
            migrationBuilder.Sql("UPDATE Customers SET BirthDate = NULL WHERE Id = 2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
