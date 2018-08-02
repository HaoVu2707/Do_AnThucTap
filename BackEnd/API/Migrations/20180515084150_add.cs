using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCatagories_ServiceCatagoryEntityId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceCatagoryEntityId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceCatagoryEntityId",
                table: "Services");

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCatagoryId",
                table: "Services",
                column: "ServiceCatagoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCatagories_ServiceCatagoryId",
                table: "Services",
                column: "ServiceCatagoryId",
                principalTable: "ServiceCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_ServiceCatagories_ServiceCatagoryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_ServiceCatagoryId",
                table: "Services");

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceCatagoryEntityId",
                table: "Services",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_ServiceCatagoryEntityId",
                table: "Services",
                column: "ServiceCatagoryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_ServiceCatagories_ServiceCatagoryEntityId",
                table: "Services",
                column: "ServiceCatagoryEntityId",
                principalTable: "ServiceCatagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
