using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISecurityBasic.Migrations
{
    public partial class Villas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amentity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupany", "Rate", "Sqft", "UpdatedDate" },
                values: new object[] { 1, "b", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hello", "a", "Long", 3, 3.0, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
