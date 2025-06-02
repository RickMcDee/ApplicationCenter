using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationCenter.WebApp.Migrations.ConfigurationData
{
    /// <inheritdoc />
    public partial class Added_ConfigurationKeyValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "ConfigurationKeys");

            migrationBuilder.CreateTable(
                name: "ConfigurationKeyValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Stage = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ConfigurationKeyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationKeyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationKeyValues_ConfigurationKeys_ConfigurationKeyId",
                        column: x => x.ConfigurationKeyId,
                        principalTable: "ConfigurationKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationKeyValues_ConfigurationKeyId",
                table: "ConfigurationKeyValues",
                column: "ConfigurationKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationKeyValues");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "ConfigurationKeys",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}