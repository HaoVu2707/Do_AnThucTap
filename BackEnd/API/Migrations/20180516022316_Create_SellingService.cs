using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Create_SellingService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellingServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BranchId = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    CreatedUserId = table.Column<Guid>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    FromDate = table.Column<DateTime>(nullable: false),
                    IsDiscountMoney = table.Column<bool>(nullable: false),
                    IsRunning = table.Column<bool>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    SoldAmount = table.Column<double>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellingServices_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellingServices_CreatedUserId",
                table: "SellingServices",
                column: "CreatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellingServices");
        }
    }
}
