using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTaller.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrecioRepuestos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "precio",
                table: "repuesto",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "precio_unitario",
                table: "orden_repuesto",
                type: "numeric(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddCheckConstraint(
                name: "ck_repuesto_precio",
                table: "repuesto",
                sql: "precio >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_orden_repuesto_precio_unitario",
                table: "orden_repuesto",
                sql: "precio_unitario >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_repuesto_precio",
                table: "repuesto");

            migrationBuilder.DropCheckConstraint(
                name: "ck_orden_repuesto_precio_unitario",
                table: "orden_repuesto");

            migrationBuilder.DropColumn(
                name: "precio",
                table: "repuesto");

            migrationBuilder.DropColumn(
                name: "precio_unitario",
                table: "orden_repuesto");
        }
    }
}
