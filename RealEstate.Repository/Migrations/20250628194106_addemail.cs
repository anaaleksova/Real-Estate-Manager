using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstate.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agent_AspNetUsers_ApplicationUserId",
                table: "Agent");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentProperty_Agent_AgentId",
                table: "AgentProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentProperty_Properties_PropertyId",
                table: "AgentProperty");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agent_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Client_ClientId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Client_ClientId1",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agent_AgentId1",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Client",
                table: "Client");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentProperty",
                table: "AgentProperty");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agent",
                table: "Agent");

            migrationBuilder.RenameTable(
                name: "Client",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "AgentProperty",
                newName: "AgentProperties");

            migrationBuilder.RenameTable(
                name: "Agent",
                newName: "Agents");

            migrationBuilder.RenameIndex(
                name: "IX_Client_ApplicationUserId",
                table: "Clients",
                newName: "IX_Clients_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentProperty_PropertyId",
                table: "AgentProperties",
                newName: "IX_AgentProperties_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentProperty_AgentId",
                table: "AgentProperties",
                newName: "IX_AgentProperties_AgentId");

            migrationBuilder.RenameIndex(
                name: "IX_Agent_ApplicationUserId",
                table: "Agents",
                newName: "IX_Agents_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentProperties",
                table: "AgentProperties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agents",
                table: "Agents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AgentProperties_Agents_AgentId",
                table: "AgentProperties",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentProperties_Properties_PropertyId",
                table: "AgentProperties",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_ApplicationUserId",
                table: "Agents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Clients_ClientId1",
                table: "Appointments",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_AspNetUsers_ApplicationUserId",
                table: "Clients",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Clients_ClientId1",
                table: "Favorites",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agents_AgentId1",
                table: "Properties",
                column: "AgentId1",
                principalTable: "Agents",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgentProperties_Agents_AgentId",
                table: "AgentProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_AgentProperties_Properties_PropertyId",
                table: "AgentProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_ApplicationUserId",
                table: "Agents");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Agents_AgentId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Clients_ClientId1",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_AspNetUsers_ApplicationUserId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Clients_ClientId1",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Agents_AgentId1",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Agents",
                table: "Agents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AgentProperties",
                table: "AgentProperties");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "Client");

            migrationBuilder.RenameTable(
                name: "Agents",
                newName: "Agent");

            migrationBuilder.RenameTable(
                name: "AgentProperties",
                newName: "AgentProperty");

            migrationBuilder.RenameIndex(
                name: "IX_Clients_ApplicationUserId",
                table: "Client",
                newName: "IX_Client_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Agents_ApplicationUserId",
                table: "Agent",
                newName: "IX_Agent_ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentProperties_PropertyId",
                table: "AgentProperty",
                newName: "IX_AgentProperty_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_AgentProperties_AgentId",
                table: "AgentProperty",
                newName: "IX_AgentProperty_AgentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Client",
                table: "Client",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Agent",
                table: "Agent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AgentProperty",
                table: "AgentProperty",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Agent_AspNetUsers_ApplicationUserId",
                table: "Agent",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentProperty_Agent_AgentId",
                table: "AgentProperty",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AgentProperty_Properties_PropertyId",
                table: "AgentProperty",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Agent_AgentId",
                table: "Appointments",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Client_ClientId1",
                table: "Appointments",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_AspNetUsers_ApplicationUserId",
                table: "Client",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Client_ClientId1",
                table: "Favorites",
                column: "ClientId1",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Agent_AgentId1",
                table: "Properties",
                column: "AgentId1",
                principalTable: "Agent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
