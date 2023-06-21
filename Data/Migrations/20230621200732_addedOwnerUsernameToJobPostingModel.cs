using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_Step_API.Data.Migrations
{
    public partial class addedOwnerUsernameToJobPostingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerUsername",
                table: "JobPostings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerUsername",
                table: "JobPostings");
        }
    }
}
