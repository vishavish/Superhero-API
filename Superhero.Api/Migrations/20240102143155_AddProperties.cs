using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Superhero.Api.Migrations
{
    public partial class AddProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Heroes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_OrganizationId",
                table: "Heroes",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Organizations_OrganizationId",
                table: "Heroes",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Organizations_OrganizationId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_OrganizationId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Heroes");
        }
    }
}
