﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NetBootcamp.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "Created", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, "123456789", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product 1", 100m, 0 },
                    { 2, "123456789", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product 2", 100m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
