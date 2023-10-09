using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientSupport.Migrations
{
    public partial class MotivoFechamentoChamado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "motivoFechamento",
                table: "ServicesRequest",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "motivoFechamento",
                table: "ServicesRequest");
        }
    }
}
