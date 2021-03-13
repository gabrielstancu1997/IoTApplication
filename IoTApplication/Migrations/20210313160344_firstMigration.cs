using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IoTApplication.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "metrics",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    dimensions = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metrics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "values",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    value = table.Column<double>(type: "double precision", nullable: true),
                    metric_id = table.Column<int>(type: "integer", nullable: true),
                    value_meta = table.Column<string>(type: "json", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_values", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "metrics_name_dimensions_key",
                table: "metrics",
                columns: new[] { "name", "dimensions" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "values_metric_id_timestamp_idx",
                table: "values",
                columns: new[] { "metric_id", "timestamp" });

            migrationBuilder.CreateIndex(
                name: "values_timestamp_metric_id_idx",
                table: "values",
                columns: new[] { "timestamp", "metric_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "metrics");

            migrationBuilder.DropTable(
                name: "values");
        }
    }
}
