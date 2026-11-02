using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTaller.Datos.Migrations
{
    /// <inheritdoc />
    public partial class QuitarEmpleadoCotizacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orden_empleado_empleado_cotizacion_id",
                table: "orden");

            migrationBuilder.DropIndex(
                name: "IX_orden_empleado_cotizacion_id",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_cotizacion_id",
                table: "orden");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "empleado_cotizacion_id",
                table: "orden",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orden_empleado_cotizacion_id",
                table: "orden",
                column: "empleado_cotizacion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orden_empleado_empleado_cotizacion_id",
                table: "orden",
                column: "empleado_cotizacion_id",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
