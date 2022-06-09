using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perspektiva.Migrations
{
    public partial class AddPerspectivestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
              name: "Perspectives",
              schema: "dbo",
              columns: table => new
              {
                  /* add autoincrement*/
                  Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                  UserID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                  Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                  Difficulty = table.Column<int>(type: "int", nullable: false),
                  PerspectivePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)

              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_Perspectives", x => x.Id);
              });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Perspectives",
                schema: "dbo");

        }
    }
}
