using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPool.DAL.Migrations
{
    public partial class AddPassenger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RideEntityUserEntity");

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PassengerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RideId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passengers_Rides_RideId",
                        column: x => x.RideId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passengers_Users_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_PassengerId",
                table: "Passengers",
                column: "PassengerId");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_RideId",
                table: "Passengers",
                column: "RideId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.CreateTable(
                name: "RideEntityUserEntity",
                columns: table => new
                {
                    PassengersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RidesPassengerId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RideEntityUserEntity", x => new { x.PassengersId, x.RidesPassengerId });
                    table.ForeignKey(
                        name: "FK_RideEntityUserEntity_Rides_RidesPassengerId",
                        column: x => x.RidesPassengerId,
                        principalTable: "Rides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RideEntityUserEntity_Users_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RideEntityUserEntity_RidesPassengerId",
                table: "RideEntityUserEntity",
                column: "RidesPassengerId");
        }
    }
}
