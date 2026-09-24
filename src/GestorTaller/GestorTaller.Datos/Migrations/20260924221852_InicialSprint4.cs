using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestorTaller.Datos.Migrations
{
    /// <inheritdoc />
    public partial class InicialSprint4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    nombre_usuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    contrasena = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    rol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orden",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    cliente_id = table.Column<Guid>(type: "uuid", nullable: false),
                    descripcion_objeto = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    descripcion_problema = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    costo_diagnostico = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    fecha_recepcion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    empleado_diagnostico = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    detalle_diagnostico = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    empleado_cotizacion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    monto_cotizado = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    cotizacion_aceptada = table.Column<bool>(type: "boolean", nullable: false),
                    empleado_reparacion = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    detalle_reparacion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    empleado_entrega = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    monto_pagado = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    fecha_entrega = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orden", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orden_cliente_cliente_id",
                        column: x => x.cliente_id,
                        principalTable: "cliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orden_historial_estado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrdenId = table.Column<Guid>(type: "uuid", nullable: false),
                    estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    orden_posicion = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orden_historial_estado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orden_historial_estado_orden_OrdenId",
                        column: x => x.OrdenId,
                        principalTable: "orden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orden_cliente_id",
                table: "orden",
                column: "cliente_id");

            migrationBuilder.CreateIndex(
                name: "IX_orden_historial_estado_OrdenId",
                table: "orden_historial_estado",
                column: "OrdenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orden_historial_estado");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "orden");

            migrationBuilder.DropTable(
                name: "cliente");
        }
    }
}
