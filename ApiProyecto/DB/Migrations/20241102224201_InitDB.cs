using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OCargo",
                columns: table => new
                {
                    IdCargo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cargo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCargo", x => x.IdCargo);
                });

            migrationBuilder.CreateTable(
                name: "OCategoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCategoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "OCita",
                columns: table => new
                {
                    IdCita = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPersonaCliente = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdPersonaEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdHorario = table.Column<int>(type: "int", nullable: false),
                    IdSexo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCita", x => x.IdCita);
                });

            migrationBuilder.CreateTable(
                name: "OCliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "OCombos",
                columns: table => new
                {
                    IdCombos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DuracionTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OCombos", x => x.IdCombos);
                });

            migrationBuilder.CreateTable(
                name: "ODetalleFactura",
                columns: table => new
                {
                    IdDetalleFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrecioUnidad = table.Column<float>(type: "real", nullable: false),
                    Subtotal = table.Column<float>(type: "real", nullable: false),
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPersonaCliente = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdPersonaEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false),
                    IdCategoriaServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ODetalleFactura", x => x.IdDetalleFactura);
                });

            migrationBuilder.CreateTable(
                name: "OEmpleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comision = table.Column<int>(type: "int", nullable: false),
                    FechaContrato = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OEmpleado", x => x.IdEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "OEstado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OEstado", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "OFactura",
                columns: table => new
                {
                    IdFactura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaComision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoTotal = table.Column<float>(type: "real", nullable: false),
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPersonaCliente = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdPersonaEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OFactura", x => x.IdFactura);
                });

            migrationBuilder.CreateTable(
                name: "OHorario",
                columns: table => new
                {
                    IdHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OHorario", x => x.IdHorario);
                });

            migrationBuilder.CreateTable(
                name: "OMetodoPago",
                columns: table => new
                {
                    IdMetodoPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    metodoPago = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OMetodoPago", x => x.IdMetodoPago);
                });

            migrationBuilder.CreateTable(
                name: "OPersona",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SexoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPersona", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "OPromocion",
                columns: table => new
                {
                    IdPromocion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descuento = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPromocion", x => x.IdPromocion);
                });

            migrationBuilder.CreateTable(
                name: "OServicio",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    servicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    precio = table.Column<float>(type: "real", nullable: false),
                    minutos = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OServicio", x => x.IdServicio);
                });

            migrationBuilder.CreateTable(
                name: "OSexo",
                columns: table => new
                {
                    IdSexo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sexo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OSexo", x => x.IdSexo);
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    IdPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    IdFactura = table.Column<int>(type: "int", nullable: false),
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPersonaCliente = table.Column<int>(type: "int", nullable: false),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    IdPersonaEmpleado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.IdPago);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OCargo");

            migrationBuilder.DropTable(
                name: "OCategoria");

            migrationBuilder.DropTable(
                name: "OCita");

            migrationBuilder.DropTable(
                name: "OCliente");

            migrationBuilder.DropTable(
                name: "OCombos");

            migrationBuilder.DropTable(
                name: "ODetalleFactura");

            migrationBuilder.DropTable(
                name: "OEmpleado");

            migrationBuilder.DropTable(
                name: "OEstado");

            migrationBuilder.DropTable(
                name: "OFactura");

            migrationBuilder.DropTable(
                name: "OHorario");

            migrationBuilder.DropTable(
                name: "OMetodoPago");

            migrationBuilder.DropTable(
                name: "OPersona");

            migrationBuilder.DropTable(
                name: "OPromocion");

            migrationBuilder.DropTable(
                name: "OServicio");

            migrationBuilder.DropTable(
                name: "OSexo");

            migrationBuilder.DropTable(
                name: "Pago");
        }
    }
}
