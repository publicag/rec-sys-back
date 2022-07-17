using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RecommendationSystem.Persistence.Migrations
{
    public partial class UserPredGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_pred_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    first_group = table.Column<int>(type: "integer", nullable: false),
                    first_group_score = table.Column<float>(type: "real", nullable: false),
                    second_group = table.Column<int>(type: "integer", nullable: false),
                    second_group_score = table.Column<float>(type: "real", nullable: false),
                    third_group = table.Column<int>(type: "integer", nullable: false),
                    third_group_score = table.Column<float>(type: "real", nullable: false),
                    fourth_group = table.Column<int>(type: "integer", nullable: false),
                    fourth_group_score = table.Column<float>(type: "real", nullable: false),
                    fifth_group = table.Column<int>(type: "integer", nullable: false),
                    fifth_group_score = table.Column<float>(type: "real", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_pred_group", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_pred_group");
        }
    }
}
