using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTaller.Datos.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarEmpleadosEnOrden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "empleado_cotizacion_id",
                table: "orden",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "empleado_diagnostico_id",
                table: "orden",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "empleado_entrega_id",
                table: "orden",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "empleado_reparacion_id",
                table: "orden",
                type: "uuid",
                nullable: true);

            // Conserva los responsables: busca al empleado por su nombre (sin importar mayusculas ni espacios
            // sobrantes). Los nombres que no coinciden con ningun empleado quedan en NULL.
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_cotizacion_id = e.""Id"" FROM empleado e WHERE lower(trim(e.nombre)) = lower(trim(o.empleado_cotizacion));");
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_diagnostico_id = e.""Id"" FROM empleado e WHERE lower(trim(e.nombre)) = lower(trim(o.empleado_diagnostico));");
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_entrega_id = e.""Id"" FROM empleado e WHERE lower(trim(e.nombre)) = lower(trim(o.empleado_entrega));");
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_reparacion_id = e.""Id"" FROM empleado e WHERE lower(trim(e.nombre)) = lower(trim(o.empleado_reparacion));");

            migrationBuilder.DropColumn(
                name: "empleado_cotizacion",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_diagnostico",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_entrega",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_reparacion",
                table: "orden");

            migrationBuilder.CreateIndex(
                name: "IX_orden_empleado_cotizacion_id",
                table: "orden",
                column: "empleado_cotizacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_orden_empleado_diagnostico_id",
                table: "orden",
                column: "empleado_diagnostico_id");

            migrationBuilder.CreateIndex(
                name: "IX_orden_empleado_entrega_id",
                table: "orden",
                column: "empleado_entrega_id");

            migrationBuilder.CreateIndex(
                name: "IX_orden_empleado_reparacion_id",
                table: "orden",
                column: "empleado_reparacion_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orden_empleado_empleado_cotizacion_id",
                table: "orden",
                column: "empleado_cotizacion_id",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orden_empleado_empleado_diagnostico_id",
                table: "orden",
                column: "empleado_diagnostico_id",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orden_empleado_empleado_entrega_id",
                table: "orden",
                column: "empleado_entrega_id",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orden_empleado_empleado_reparacion_id",
                table: "orden",
                column: "empleado_reparacion_id",
                principalTable: "empleado",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orden_empleado_empleado_cotizacion_id",
                table: "orden");

            migrationBuilder.DropForeignKey(
                name: "FK_orden_empleado_empleado_diagnostico_id",
                table: "orden");

            migrationBuilder.DropForeignKey(
                name: "FK_orden_empleado_empleado_entrega_id",
                table: "orden");

            migrationBuilder.DropForeignKey(
                name: "FK_orden_empleado_empleado_reparacion_id",
                table: "orden");

            migrationBuilder.DropIndex(
                name: "IX_orden_empleado_cotizacion_id",
                table: "orden");

            migrationBuilder.DropIndex(
                name: "IX_orden_empleado_diagnostico_id",
                table: "orden");

            migrationBuilder.DropIndex(
                name: "IX_orden_empleado_entrega_id",
                table: "orden");

            migrationBuilder.DropIndex(
                name: "IX_orden_empleado_reparacion_id",
                table: "orden");

            migrationBuilder.AddColumn<string>(
                name: "empleado_cotizacion",
                table: "orden",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "empleado_diagnostico",
                table: "orden",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "empleado_entrega",
                table: "orden",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "empleado_reparacion",
                table: "orden",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"UPDATE orden o SET empleado_cotizacion = e.nombre FROM empleado e WHERE e.""Id"" = o.empleado_cotizacion_id;");
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_diagnostico = e.nombre FROM empleado e WHERE e.""Id"" = o.empleado_diagnostico_id;");
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_entrega = e.nombre FROM empleado e WHERE e.""Id"" = o.empleado_entrega_id;");
            migrationBuilder.Sql(@"UPDATE orden o SET empleado_reparacion = e.nombre FROM empleado e WHERE e.""Id"" = o.empleado_reparacion_id;");

            migrationBuilder.DropColumn(
                name: "empleado_cotizacion_id",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_diagnostico_id",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_entrega_id",
                table: "orden");

            migrationBuilder.DropColumn(
                name: "empleado_reparacion_id",
                table: "orden");
        }
    }
}
