﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp_Web.API.Migrations
{
    /// <inheritdoc />
    public partial class updateMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Messages",
                newName: "MessageContent");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MessageContent",
                table: "Messages",
                newName: "Content");
        }
    }
}
