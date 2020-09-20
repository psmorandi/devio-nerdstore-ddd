﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PsmjCo.NerdStore.Vendas.Data;

namespace PsmjCo.NerdStore.Vendas.Data.Migrations
{
    [DbContext(typeof(VendasContext))]
    [Migration("20200919214944_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0-preview.8.20407.4");

            modelBuilder.HasSequence<int>("MinhaSequencia")
                .StartsAt(1000L);

            modelBuilder.Entity("PsmjCo.NerdStore.Vendas.Domain.Pedido", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR MinhaSequencia");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<decimal>("Desconto")
                        .HasColumnType("decimal");

                    b.Property<int>("PedidoStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("VoucherUtilizado")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("VoucherId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("PsmjCo.NerdStore.Vendas.Domain.PedidoItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PedidoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProdutoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProdutoNome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorUnitario")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.ToTable("PedidoItems");
                });

            modelBuilder.Entity("PsmjCo.NerdStore.Vendas.Domain.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DataUtilizacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataValidade")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Percentual")
                        .HasColumnType("decimal");

                    b.Property<int>("TipoDescontoVoucher")
                        .HasColumnType("int");

                    b.Property<bool>("Utilizado")
                        .HasColumnType("bit");

                    b.Property<decimal?>("ValorDesconto")
                        .HasColumnType("decimal");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("PsmjCo.NerdStore.Vendas.Domain.Pedido", b =>
                {
                    b.HasOne("PsmjCo.NerdStore.Vendas.Domain.Voucher", "Voucher")
                        .WithMany("Pedidos")
                        .HasForeignKey("VoucherId");
                });

            modelBuilder.Entity("PsmjCo.NerdStore.Vendas.Domain.PedidoItem", b =>
                {
                    b.HasOne("PsmjCo.NerdStore.Vendas.Domain.Pedido", "Pedido")
                        .WithMany("PedidoItems")
                        .HasForeignKey("PedidoId")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}