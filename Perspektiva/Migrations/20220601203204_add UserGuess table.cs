using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Perspektiva.Migrations
{
    public partial class addUserGuesstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGuess",
                schema: "dbo",
                columns: table => new
                {
                  /* add autoincrement*/  Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerspectivePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGuess", x => x.Id);
                });
        }
     
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserGuess",
                schema: "dbo");
        }
    }
}
