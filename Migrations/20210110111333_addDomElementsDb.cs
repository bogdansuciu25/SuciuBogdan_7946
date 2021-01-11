using Microsoft.EntityFrameworkCore.Migrations;

namespace SuciuBogdan_7946.Migrations
{
    public partial class addDomElementsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocatorValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DomElement_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DomElement_PageId",
                table: "DomElement",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomElement");
        }
    }
}
