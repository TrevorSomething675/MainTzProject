﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MainTz.Database.Migrations
{
    /// <inheritdoc />
    public partial class addCarName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Cars",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Cars");
        }
    }
}
