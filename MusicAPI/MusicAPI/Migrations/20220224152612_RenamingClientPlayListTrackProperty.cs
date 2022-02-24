using Microsoft.EntityFrameworkCore.Migrations;


namespace MusicAPI.Migrations
{
    public partial class RenamingClientPlayListTrackProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "ClientPlayListTracks",
                newName: "ClientPlaylistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClientPlaylistId",
                table: "ClientPlayListTracks",
                newName: "ClientId");
        }
    }
}
