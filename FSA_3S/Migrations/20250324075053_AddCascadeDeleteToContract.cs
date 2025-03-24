using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSA_3S.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteToContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customertype",
                table: "mappingcontractcustomer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "customertype",
                table: "mappingcontractcustomer",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
