using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class initDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    IdCargo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cargo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.IdCargo);
                });

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoria = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.IdCliente);
                });

            migrationBuilder.CreateTable(
                name: "Combos",
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
                    table.PrimaryKey("PK_Combos", x => x.IdCombos);
                });

            migrationBuilder.CreateTable(
                name: "Empleado",
                columns: table => new
                {
                    IdEmpleado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comision = table.Column<int>(type: "int", nullable: false),
                    FechaContrato = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.IdEmpleado);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                columns: table => new
                {
                    IdEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.IdEstado);
                });

            migrationBuilder.CreateTable(
                name: "Horario",
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
                    table.PrimaryKey("PK_Horario", x => x.IdHorario);
                });

            migrationBuilder.CreateTable(
                name: "MetodoPago",
                columns: table => new
                {
                    IdMetodoPago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    metodoPago = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPago", x => x.IdMetodoPago);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    IdSexo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sexo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.IdSexo);
                });

            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreServicio = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    Minutos = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.IdServicio);
                    table.ForeignKey(
                        name: "FK_Servicio_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Persona",
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
                    IdSexo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Persona_Sexo_IdSexo",
                        column: x => x.IdSexo,
                        principalTable: "Sexo",
                        principalColumn: "IdSexo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComboServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCombo = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComboServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComboServicio_Combos_IdCombo",
                        column: x => x.IdCombo,
                        principalTable: "Combos",
                        principalColumn: "IdCombos",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComboServicio_Servicio_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Promocion",
                columns: table => new
                {
                    IdPromocion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdEstado = table.Column<int>(type: "int", nullable: false),
                    ServicioIdServicio = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocion", x => x.IdPromocion);
                    table.ForeignKey(
                        name: "FK_Promocion_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Promocion_Servicio_ServicioIdServicio",
                        column: x => x.ServicioIdServicio,
                        principalTable: "Servicio",
                        principalColumn: "IdServicio");
                });

            migrationBuilder.CreateTable(
                name: "Cita",
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
                    table.PrimaryKey("PK_Cita", x => x.IdCita);
                    table.ForeignKey(
                        name: "FK_Cita_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Horario_IdHorario",
                        column: x => x.IdHorario,
                        principalTable: "Horario",
                        principalColumn: "IdHorario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Persona_IdPersonaCliente",
                        column: x => x.IdPersonaCliente,
                        principalTable: "Persona",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cita_Persona_IdPersonaEmpleado",
                        column: x => x.IdPersonaEmpleado,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_Cita_Sexo_IdSexo",
                        column: x => x.IdSexo,
                        principalTable: "Sexo",
                        principalColumn: "IdSexo");
                });

            migrationBuilder.CreateTable(
                name: "CitaServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCita = table.Column<int>(type: "int", nullable: false),
                    IdServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitaServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CitaServicio_Cita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "Cita",
                        principalColumn: "IdCita",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitaServicio_Servicio_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "IdServicio",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
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
                    IdPersonaEmpleado = table.Column<int>(type: "int", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    EmpleadoIdEmpleado = table.Column<int>(type: "int", nullable: true),
                    EstadoIdEstado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.IdFactura);
                    table.ForeignKey(
                        name: "FK_Factura_Cita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "Cita",
                        principalColumn: "IdCita",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factura_Cliente_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Factura_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Factura_Empleado_EmpleadoIdEmpleado",
                        column: x => x.EmpleadoIdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                    table.ForeignKey(
                        name: "FK_Factura_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                    table.ForeignKey(
                        name: "FK_Factura_Estado_EstadoIdEstado",
                        column: x => x.EstadoIdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado");
                    table.ForeignKey(
                        name: "FK_Factura_Persona_IdPersonaCliente",
                        column: x => x.IdPersonaCliente,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_Factura_Persona_IdPersonaEmpleado",
                        column: x => x.IdPersonaEmpleado,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateTable(
                name: "DetalleFactura",
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
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    CitaIdCita = table.Column<int>(type: "int", nullable: true),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    FacturaIdFactura = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFactura", x => x.IdDetalleFactura);
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "IdCategoria");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Cita_CitaIdCita",
                        column: x => x.CitaIdCita,
                        principalTable: "Cita",
                        principalColumn: "IdCita");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Cita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "Cita",
                        principalColumn: "IdCita");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Cliente_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Factura_FacturaIdFactura",
                        column: x => x.FacturaIdFactura,
                        principalTable: "Factura",
                        principalColumn: "IdFactura");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Factura_IdFactura",
                        column: x => x.IdFactura,
                        principalTable: "Factura",
                        principalColumn: "IdFactura");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Persona_IdPersonaCliente",
                        column: x => x.IdPersonaCliente,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Persona_IdPersonaEmpleado",
                        column: x => x.IdPersonaEmpleado,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_DetalleFactura_Servicio_IdServicio",
                        column: x => x.IdServicio,
                        principalTable: "Servicio",
                        principalColumn: "IdServicio");
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
                    IdPersonaEmpleado = table.Column<int>(type: "int", nullable: false),
                    CitaIdCita = table.Column<int>(type: "int", nullable: true),
                    ClienteIdCliente = table.Column<int>(type: "int", nullable: true),
                    EmpleadoIdEmpleado = table.Column<int>(type: "int", nullable: true),
                    EstadoIdEstado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.IdPago);
                    table.ForeignKey(
                        name: "FK_Pago_Cita_CitaIdCita",
                        column: x => x.CitaIdCita,
                        principalTable: "Cita",
                        principalColumn: "IdCita");
                    table.ForeignKey(
                        name: "FK_Pago_Cita_IdCita",
                        column: x => x.IdCita,
                        principalTable: "Cita",
                        principalColumn: "IdCita");
                    table.ForeignKey(
                        name: "FK_Pago_Cliente_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Pago_Cliente_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Cliente",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Pago_Empleado_EmpleadoIdEmpleado",
                        column: x => x.EmpleadoIdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                    table.ForeignKey(
                        name: "FK_Pago_Empleado_IdEmpleado",
                        column: x => x.IdEmpleado,
                        principalTable: "Empleado",
                        principalColumn: "IdEmpleado");
                    table.ForeignKey(
                        name: "FK_Pago_Estado_EstadoIdEstado",
                        column: x => x.EstadoIdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado");
                    table.ForeignKey(
                        name: "FK_Pago_Estado_IdEstado",
                        column: x => x.IdEstado,
                        principalTable: "Estado",
                        principalColumn: "IdEstado");
                    table.ForeignKey(
                        name: "FK_Pago_Factura_IdFactura",
                        column: x => x.IdFactura,
                        principalTable: "Factura",
                        principalColumn: "IdFactura");
                    table.ForeignKey(
                        name: "FK_Pago_Persona_IdPersonaCliente",
                        column: x => x.IdPersonaCliente,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                    table.ForeignKey(
                        name: "FK_Pago_Persona_IdPersonaEmpleado",
                        column: x => x.IdPersonaEmpleado,
                        principalTable: "Persona",
                        principalColumn: "IdPersona");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdCliente",
                table: "Cita",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdEmpleado",
                table: "Cita",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdEstado",
                table: "Cita",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdHorario",
                table: "Cita",
                column: "IdHorario");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdPersonaCliente",
                table: "Cita",
                column: "IdPersonaCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdPersonaEmpleado",
                table: "Cita",
                column: "IdPersonaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Cita_IdSexo",
                table: "Cita",
                column: "IdSexo");

            migrationBuilder.CreateIndex(
                name: "IX_CitaServicio_IdCita",
                table: "CitaServicio",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_CitaServicio_IdServicio",
                table: "CitaServicio",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_ComboServicio_IdCombo",
                table: "ComboServicio",
                column: "IdCombo");

            migrationBuilder.CreateIndex(
                name: "IX_ComboServicio_IdServicio",
                table: "ComboServicio",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_CitaIdCita",
                table: "DetalleFactura",
                column: "CitaIdCita");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_ClienteIdCliente",
                table: "DetalleFactura",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_FacturaIdFactura",
                table: "DetalleFactura",
                column: "FacturaIdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdCategoria",
                table: "DetalleFactura",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdCita",
                table: "DetalleFactura",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdCliente",
                table: "DetalleFactura",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdEmpleado",
                table: "DetalleFactura",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdFactura",
                table: "DetalleFactura",
                column: "IdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdPersonaCliente",
                table: "DetalleFactura",
                column: "IdPersonaCliente");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdPersonaEmpleado",
                table: "DetalleFactura",
                column: "IdPersonaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFactura_IdServicio",
                table: "DetalleFactura",
                column: "IdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_ClienteIdCliente",
                table: "Factura",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_EmpleadoIdEmpleado",
                table: "Factura",
                column: "EmpleadoIdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_EstadoIdEstado",
                table: "Factura",
                column: "EstadoIdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdCita",
                table: "Factura",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdCliente",
                table: "Factura",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdEmpleado",
                table: "Factura",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdPersonaCliente",
                table: "Factura",
                column: "IdPersonaCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_IdPersonaEmpleado",
                table: "Factura",
                column: "IdPersonaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_CitaIdCita",
                table: "Pago",
                column: "CitaIdCita");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ClienteIdCliente",
                table: "Pago",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_EmpleadoIdEmpleado",
                table: "Pago",
                column: "EmpleadoIdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_EstadoIdEstado",
                table: "Pago",
                column: "EstadoIdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdCita",
                table: "Pago",
                column: "IdCita");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdCliente",
                table: "Pago",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdEmpleado",
                table: "Pago",
                column: "IdEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdEstado",
                table: "Pago",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdFactura",
                table: "Pago",
                column: "IdFactura");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdPersonaCliente",
                table: "Pago",
                column: "IdPersonaCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_IdPersonaEmpleado",
                table: "Pago",
                column: "IdPersonaEmpleado");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_IdSexo",
                table: "Persona",
                column: "IdSexo");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_IdEstado",
                table: "Promocion",
                column: "IdEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Promocion_ServicioIdServicio",
                table: "Promocion",
                column: "ServicioIdServicio");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_IdCategoria",
                table: "Servicio",
                column: "IdCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "CitaServicio");

            migrationBuilder.DropTable(
                name: "ComboServicio");

            migrationBuilder.DropTable(
                name: "DetalleFactura");

            migrationBuilder.DropTable(
                name: "MetodoPago");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Promocion");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Servicio");

            migrationBuilder.DropTable(
                name: "Cita");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Empleado");

            migrationBuilder.DropTable(
                name: "Estado");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "Sexo");
        }
    }
}
