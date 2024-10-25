﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Product.Api.Migrations
{
    /// <inheritdoc />
    public partial class Product_Set_No_IsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_No",
                table: "Products",
                column: "No",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_No",
                table: "Products");
        }
    }
}