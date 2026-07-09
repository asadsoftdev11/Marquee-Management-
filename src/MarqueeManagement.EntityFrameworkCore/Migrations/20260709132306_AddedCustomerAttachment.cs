using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarqueeManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomerAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCustomerAttachments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileAttachmentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCustomerAttachments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAttachments_CustomerId",
                table: "AppCustomerAttachments",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCustomerAttachments");
        }
    }
}
