using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Repository.Migrations
{
    /// <inheritdoc />
    public partial class updateEntities2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_ClientId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AspNetUsers_ClientId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_ClientId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsAgent",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId1",
                table: "Properties",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Favorites",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId1",
                table: "Favorites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId1",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Agent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agent_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties",
                column: "AgentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ClientId1",
                table: "Favorites",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_AgentId",
                table: "Appointments",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId1",
                table: "Appointments",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Agent_ApplicationUserId",
                table: "Agent",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agent_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Client_ClientId1",
                table: "Appointments",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Client_ClientId1",
                table: "Favorites",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agent_AgentId1",
                table: "Properties",
                column: "AgentId1",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agent_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Client_ClientId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Client_ClientId1",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agent_AgentId1",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Agent");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AgentId1",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_ClientId1",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_AgentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ClientId1",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AgentId1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Appointments");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Properties",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AgentId",
                table: "Properties",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Favorites",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsAgent",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AgentId",
                table: "Properties",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ClientId",
                table: "Favorites",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_ClientId",
                table: "Appointments",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AspNetUsers_ClientId",
                table: "Favorites",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_AspNetUsers_AgentId",
                table: "Properties",
                column: "AgentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
