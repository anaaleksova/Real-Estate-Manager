using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Repository.Migrations
{
    /// <inheritdoc />
    public partial class of : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_ApplicationUserId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Agents_ApplicationUserId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "AgentId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Agents");

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentId",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Agents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                table: "Properties",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Agents");

            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId1",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties",
                column: "AgentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Clients",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ApplicationUserId",
                table: "Agents",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_ApplicationUserId",
                table: "Agents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId1",
                table: "Properties",
                column: "AgentId1",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
