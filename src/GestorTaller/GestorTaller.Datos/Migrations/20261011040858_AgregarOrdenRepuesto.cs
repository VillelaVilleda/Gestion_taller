using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestorTaller.Datos.Migrations
{
    /// <inheritdoc />
    public partial class AgregarOrdenRepuesto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orden_repuesto",
                columns: table => new
                {
                    orden_id = table.Column<Guid>(type: "uuid", nullable: false),
                    repuesto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orden_repuesto", x => new { x.orden_id, x.repuesto_id });
                    table.CheckConstraint("ck_orden_repuesto_cantidad", "cantidad > 0");
                    table.ForeignKey(
                        name: "FK_orden_repuesto_orden_orden_id",
                        column: x => x.orden_id,
                        principalTable: "orden",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orden_repuesto_repuesto_repuesto_id",
                        column: x => x.repuesto_id,
                        principalTable: "repuesto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_orden_repuesto_repuesto_id",
                table: "orden_repuesto",
                column: "repuesto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orden_repuesto");
        }
    }
}
