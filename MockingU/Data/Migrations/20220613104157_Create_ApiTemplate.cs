using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MockingU.Data.Migrations
{
    public partial class Create_ApiTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlPattern = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Methods = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response_Contents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response_ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response_StatusCode = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiTemplates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiTemplates_UserId",
                table: "ApiTemplates",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiTemplates");
        }
    }
}
