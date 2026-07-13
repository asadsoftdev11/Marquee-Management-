using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarqueeManagement.Migrations
{
    /// <inheritdoc />
    public partial class Added_FileData_To_FileAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "FileData",
                table: "AppFileAttachments",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppCustomerAttachments",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "AppCustomerAttachments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "AppCustomerAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppCustomerAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppCustomerAttachments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppCustomerAttachments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppCustomerAttachments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppCustomerAttachments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppCustomerAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "AppCustomerAttachments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppCustomerAttachments_FileAttachmentId",
                table: "AppCustomerAttachments",
                column: "FileAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomerAttachments_AppCustomers_CustomerId",
                table: "AppCustomerAttachments",
                column: "CustomerId",
                principalTable: "AppCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppCustomerAttachments_AppFileAttachments_FileAttachmentId",
                table: "AppCustomerAttachments",
                column: "FileAttachmentId",
                principalTable: "AppFileAttachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomerAttachments_AppCustomers_CustomerId",
                table: "AppCustomerAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppCustomerAttachments_AppFileAttachments_FileAttachmentId",
                table: "AppCustomerAttachments");

            migrationBuilder.DropIndex(
                name: "IX_AppCustomerAttachments_FileAttachmentId",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "FileData",
                table: "AppFileAttachments");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppCustomerAttachments");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppCustomerAttachments");
        }
    }
}
