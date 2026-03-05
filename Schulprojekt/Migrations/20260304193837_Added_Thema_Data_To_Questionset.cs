using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schulprojekt.Migrations
{
    /// <inheritdoc />
    public partial class Added_Thema_Data_To_Questionset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                                        table: "QuestionSets",
                                        keyColumn: "Id",
                                        keyValue: 1,
                                        column: "ThemaId",
                                        value: 1
            );

            migrationBuilder.UpdateData(
                            table: "QuestionSets",
                            keyColumn: "Id",
                            keyValue: 2,
                            column: "ThemaId",
                            value: 1
            );

            migrationBuilder.UpdateData(
                            table: "QuestionSets",
                            keyColumn: "Id",
                            keyValue: 3,
                            column: "ThemaId",
                            value: 1
            );

            migrationBuilder.UpdateData(
                            table: "QuestionSets",
                            keyColumn: "Id",
                            keyValue: 4,
                            column: "ThemaId",
                            value: 1
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
