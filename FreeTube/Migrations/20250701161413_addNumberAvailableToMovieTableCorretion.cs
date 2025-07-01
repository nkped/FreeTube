using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreeTube.Migrations
{
    /// <inheritdoc />
    public partial class addNumberAvailableToMovieTableCorretion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Movies SET NumberAvailable = NumberInStock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
