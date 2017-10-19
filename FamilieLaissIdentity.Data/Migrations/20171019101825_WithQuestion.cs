using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FamilieLaissIdentity.Data.Migrations
{
    public partial class WithQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityAnswer",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecurityQuestion",
                table: "AspNetUsers",
                maxLength: 5,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowed",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityAnswer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecurityQuestion",
                table: "AspNetUsers");
        }
    }
}
