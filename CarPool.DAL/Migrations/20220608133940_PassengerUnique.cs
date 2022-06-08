using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarPool.DAL.Migrations
{
    public partial class PassengerUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Passengers_PassengerId",
                table: "Passengers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Passengers_PassengerId_RideId",
                table: "Passengers",
                columns: new[] { "PassengerId", "RideId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Passengers_PassengerId_RideId",
                table: "Passengers");

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_PassengerId",
                table: "Passengers",
                column: "PassengerId");
        }
    }
}
