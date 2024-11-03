﻿// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DB.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241103042117_initDB")]
    partial class initDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DB.Cargo", b =>
                {
                    b.Property<int>("IdCargo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCargo"));

                    b.Property<string>("cargo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCargo");

                    b.ToTable("Cargo", (string)null);
                });

            modelBuilder.Entity("DB.Categoria", b =>
                {
                    b.Property<int>("IdCategoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCategoria"));

                    b.Property<string>("categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCategoria");

                    b.ToTable("Categoria", (string)null);
                });

            modelBuilder.Entity("DB.Cita", b =>
                {
                    b.Property<int>("IdCita")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCita"));

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int>("IdHorario")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdSexo")
                        .HasColumnType("int");

                    b.HasKey("IdCita");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdEstado");

                    b.HasIndex("IdHorario");

                    b.HasIndex("IdPersonaCliente");

                    b.HasIndex("IdPersonaEmpleado");

                    b.HasIndex("IdSexo");

                    b.ToTable("Cita", (string)null);
                });

            modelBuilder.Entity("DB.CitaServicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<int>("IdServicio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdCita");

                    b.HasIndex("IdServicio");

                    b.ToTable("CitaServicio", (string)null);
                });

            modelBuilder.Entity("DB.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCliente");

                    b.ToTable("Cliente", (string)null);
                });

            modelBuilder.Entity("DB.ComboServicio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdCombo")
                        .HasColumnType("int");

                    b.Property<int>("IdServicio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdCombo");

                    b.HasIndex("IdServicio");

                    b.ToTable("ComboServicio", (string)null);
                });

            modelBuilder.Entity("DB.Combos", b =>
                {
                    b.Property<int>("IdCombos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCombos"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DuracionTotal")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.HasKey("IdCombos");

                    b.ToTable("Combos", (string)null);
                });

            modelBuilder.Entity("DB.DetalleFactura", b =>
                {
                    b.Property<int>("IdDetalleFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetalleFactura"));

                    b.Property<int?>("CitaIdCita")
                        .HasColumnType("int");

                    b.Property<int?>("ClienteIdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("FacturaIdFactura")
                        .HasColumnType("int");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdFactura")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdServicio")
                        .HasColumnType("int");

                    b.Property<float>("PrecioUnidad")
                        .HasColumnType("real");

                    b.Property<float>("Subtotal")
                        .HasColumnType("real");

                    b.HasKey("IdDetalleFactura");

                    b.HasIndex("CitaIdCita");

                    b.HasIndex("ClienteIdCliente");

                    b.HasIndex("FacturaIdFactura");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdCita");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdFactura");

                    b.HasIndex("IdPersonaCliente");

                    b.HasIndex("IdPersonaEmpleado");

                    b.HasIndex("IdServicio");

                    b.ToTable("DetalleFactura", (string)null);
                });

            modelBuilder.Entity("DB.Empleado", b =>
                {
                    b.Property<int>("IdEmpleado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEmpleado"));

                    b.Property<int>("Comision")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaContrato")
                        .HasColumnType("datetime2");

                    b.HasKey("IdEmpleado");

                    b.ToTable("Empleado", (string)null);
                });

            modelBuilder.Entity("DB.Estado", b =>
                {
                    b.Property<int>("IdEstado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdEstado"));

                    b.Property<string>("estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdEstado");

                    b.ToTable("Estado", (string)null);
                });

            modelBuilder.Entity("DB.Factura", b =>
                {
                    b.Property<int>("IdFactura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdFactura"));

                    b.Property<int?>("ClienteIdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("EmpleadoIdEmpleado")
                        .HasColumnType("int");

                    b.Property<int?>("EstadoIdEstado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaComision")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaEmpleado")
                        .HasColumnType("int");

                    b.Property<float>("MontoTotal")
                        .HasColumnType("real");

                    b.HasKey("IdFactura");

                    b.HasIndex("ClienteIdCliente");

                    b.HasIndex("EmpleadoIdEmpleado");

                    b.HasIndex("EstadoIdEstado");

                    b.HasIndex("IdCita");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdPersonaCliente");

                    b.HasIndex("IdPersonaEmpleado");

                    b.ToTable("Factura", (string)null);
                });

            modelBuilder.Entity("DB.Horario", b =>
                {
                    b.Property<int>("IdHorario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHorario"));

                    b.Property<DateTime>("Dia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.HasKey("IdHorario");

                    b.ToTable("Horario", (string)null);
                });

            modelBuilder.Entity("DB.MetodoPago", b =>
                {
                    b.Property<int>("IdMetodoPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMetodoPago"));

                    b.Property<string>("metodoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMetodoPago");

                    b.ToTable("MetodoPago", (string)null);
                });

            modelBuilder.Entity("DB.Pago", b =>
                {
                    b.Property<int>("IdPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPago"));

                    b.Property<int?>("CitaIdCita")
                        .HasColumnType("int");

                    b.Property<int?>("ClienteIdCliente")
                        .HasColumnType("int");

                    b.Property<int?>("EmpleadoIdEmpleado")
                        .HasColumnType("int");

                    b.Property<int?>("EstadoIdEstado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCita")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("int");

                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int>("IdFactura")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonaEmpleado")
                        .HasColumnType("int");

                    b.Property<float>("Monto")
                        .HasColumnType("real");

                    b.HasKey("IdPago");

                    b.HasIndex("CitaIdCita");

                    b.HasIndex("ClienteIdCliente");

                    b.HasIndex("EmpleadoIdEmpleado");

                    b.HasIndex("EstadoIdEstado");

                    b.HasIndex("IdCita");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdEmpleado");

                    b.HasIndex("IdEstado");

                    b.HasIndex("IdFactura");

                    b.HasIndex("IdPersonaCliente");

                    b.HasIndex("IdPersonaEmpleado");

                    b.ToTable("Pago", (string)null);
                });

            modelBuilder.Entity("DB.Persona", b =>
                {
                    b.Property<int>("IdPersona")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPersona"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cedula")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdSexo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.HasKey("IdPersona");

                    b.HasIndex("IdSexo");

                    b.ToTable("Persona", (string)null);
                });

            modelBuilder.Entity("DB.Promocion", b =>
                {
                    b.Property<int>("IdPromocion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPromocion"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Descuento")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("FechaFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdEstado")
                        .HasColumnType("int");

                    b.Property<int?>("ServicioIdServicio")
                        .HasColumnType("int");

                    b.HasKey("IdPromocion");

                    b.HasIndex("IdEstado");

                    b.HasIndex("ServicioIdServicio");

                    b.ToTable("Promocion", (string)null);
                });

            modelBuilder.Entity("DB.Servicio", b =>
                {
                    b.Property<int>("IdServicio")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdServicio"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.Property<int>("Minutos")
                        .HasColumnType("int");

                    b.Property<string>("NombreServicio")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Precio")
                        .HasColumnType("real");

                    b.HasKey("IdServicio");

                    b.HasIndex("IdCategoria");

                    b.ToTable("Servicio", (string)null);
                });

            modelBuilder.Entity("DB.Sexo", b =>
                {
                    b.Property<int>("IdSexo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSexo"));

                    b.Property<string>("sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSexo");

                    b.ToTable("Sexo", (string)null);
                });

            modelBuilder.Entity("DB.Cita", b =>
                {
                    b.HasOne("DB.Cliente", "Cliente")
                        .WithMany("Citas")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Empleado", "Empleado")
                        .WithMany("Citas")
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Estado", "Estado")
                        .WithMany("Citas")
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Horario", "Horario")
                        .WithMany()
                        .HasForeignKey("IdHorario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaCliente")
                        .WithMany()
                        .HasForeignKey("IdPersonaCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaEmpleado")
                        .WithMany()
                        .HasForeignKey("IdPersonaEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Sexo", "Sexo")
                        .WithMany()
                        .HasForeignKey("IdSexo")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Empleado");

                    b.Navigation("Estado");

                    b.Navigation("Horario");

                    b.Navigation("PersonaCliente");

                    b.Navigation("PersonaEmpleado");

                    b.Navigation("Sexo");
                });

            modelBuilder.Entity("DB.CitaServicio", b =>
                {
                    b.HasOne("DB.Cita", "Cita")
                        .WithMany("CitaServicios")
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Servicio", "Servicio")
                        .WithMany("CitaServicios")
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("DB.ComboServicio", b =>
                {
                    b.HasOne("DB.Combos", "Combo")
                        .WithMany("ComboServicios")
                        .HasForeignKey("IdCombo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Servicio", "Servicio")
                        .WithMany("ComboServicios")
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Combo");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("DB.DetalleFactura", b =>
                {
                    b.HasOne("DB.Cita", null)
                        .WithMany("DetallesFactura")
                        .HasForeignKey("CitaIdCita");

                    b.HasOne("DB.Cliente", null)
                        .WithMany("DetallesFactura")
                        .HasForeignKey("ClienteIdCliente");

                    b.HasOne("DB.Factura", null)
                        .WithMany("Detalles")
                        .HasForeignKey("FacturaIdFactura");

                    b.HasOne("DB.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Factura", "Factura")
                        .WithMany()
                        .HasForeignKey("IdFactura")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaCliente")
                        .WithMany()
                        .HasForeignKey("IdPersonaCliente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaEmpleado")
                        .WithMany()
                        .HasForeignKey("IdPersonaEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Servicio", "Servicio")
                        .WithMany()
                        .HasForeignKey("IdServicio")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Cita");

                    b.Navigation("Cliente");

                    b.Navigation("Empleado");

                    b.Navigation("Factura");

                    b.Navigation("PersonaCliente");

                    b.Navigation("PersonaEmpleado");

                    b.Navigation("Servicio");
                });

            modelBuilder.Entity("DB.Factura", b =>
                {
                    b.HasOne("DB.Cliente", null)
                        .WithMany("Facturas")
                        .HasForeignKey("ClienteIdCliente");

                    b.HasOne("DB.Empleado", null)
                        .WithMany("Facturas")
                        .HasForeignKey("EmpleadoIdEmpleado");

                    b.HasOne("DB.Estado", null)
                        .WithMany("Facturas")
                        .HasForeignKey("EstadoIdEstado");

                    b.HasOne("DB.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DB.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaCliente")
                        .WithMany()
                        .HasForeignKey("IdPersonaCliente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaEmpleado")
                        .WithMany()
                        .HasForeignKey("IdPersonaEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Cliente");

                    b.Navigation("Empleado");

                    b.Navigation("PersonaCliente");

                    b.Navigation("PersonaEmpleado");
                });

            modelBuilder.Entity("DB.Pago", b =>
                {
                    b.HasOne("DB.Cita", null)
                        .WithMany("Pagos")
                        .HasForeignKey("CitaIdCita");

                    b.HasOne("DB.Cliente", null)
                        .WithMany("Pagos")
                        .HasForeignKey("ClienteIdCliente");

                    b.HasOne("DB.Empleado", null)
                        .WithMany("Pagos")
                        .HasForeignKey("EmpleadoIdEmpleado");

                    b.HasOne("DB.Estado", null)
                        .WithMany("Pagos")
                        .HasForeignKey("EstadoIdEstado");

                    b.HasOne("DB.Cita", "Cita")
                        .WithMany()
                        .HasForeignKey("IdCita")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Estado", "Estado")
                        .WithMany()
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Factura", "Factura")
                        .WithMany()
                        .HasForeignKey("IdFactura")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaCliente")
                        .WithMany()
                        .HasForeignKey("IdPersonaCliente")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DB.Persona", "PersonaEmpleado")
                        .WithMany()
                        .HasForeignKey("IdPersonaEmpleado")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cita");

                    b.Navigation("Cliente");

                    b.Navigation("Empleado");

                    b.Navigation("Estado");

                    b.Navigation("Factura");

                    b.Navigation("PersonaCliente");

                    b.Navigation("PersonaEmpleado");
                });

            modelBuilder.Entity("DB.Persona", b =>
                {
                    b.HasOne("DB.Sexo", "Sexo")
                        .WithMany()
                        .HasForeignKey("IdSexo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sexo");
                });

            modelBuilder.Entity("DB.Promocion", b =>
                {
                    b.HasOne("DB.Estado", "EstadoPromocion")
                        .WithMany("Promociones")
                        .HasForeignKey("IdEstado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.Servicio", null)
                        .WithMany("Promociones")
                        .HasForeignKey("ServicioIdServicio");

                    b.Navigation("EstadoPromocion");
                });

            modelBuilder.Entity("DB.Servicio", b =>
                {
                    b.HasOne("DB.Categoria", "Categoria")
                        .WithMany("Servicios")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");
                });

            modelBuilder.Entity("DB.Categoria", b =>
                {
                    b.Navigation("Servicios");
                });

            modelBuilder.Entity("DB.Cita", b =>
                {
                    b.Navigation("CitaServicios");

                    b.Navigation("DetallesFactura");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("DB.Cliente", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("DetallesFactura");

                    b.Navigation("Facturas");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("DB.Combos", b =>
                {
                    b.Navigation("ComboServicios");
                });

            modelBuilder.Entity("DB.Empleado", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("Facturas");

                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("DB.Estado", b =>
                {
                    b.Navigation("Citas");

                    b.Navigation("Facturas");

                    b.Navigation("Pagos");

                    b.Navigation("Promociones");
                });

            modelBuilder.Entity("DB.Factura", b =>
                {
                    b.Navigation("Detalles");
                });

            modelBuilder.Entity("DB.Servicio", b =>
                {
                    b.Navigation("CitaServicios");

                    b.Navigation("ComboServicios");

                    b.Navigation("Promociones");
                });
#pragma warning restore 612, 618
        }
    }
}
