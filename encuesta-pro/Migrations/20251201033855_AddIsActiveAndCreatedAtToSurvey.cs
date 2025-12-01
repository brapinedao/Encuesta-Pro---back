using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace encuesta_pro.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveAndCreatedAtToSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Surveys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Surveys");
        }
    }
}
