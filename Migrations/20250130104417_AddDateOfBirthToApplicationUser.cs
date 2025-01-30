using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HOSTEE.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirthToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "created",
                table: "Notes",
                newName: "Created");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Notes",
                newName: "created");
        }
    }
}
