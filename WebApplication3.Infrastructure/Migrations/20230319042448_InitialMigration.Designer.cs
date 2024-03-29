﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApplication3.Infrastructure.Persistance;

#nullable disable

namespace WebApplication3.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230319042448_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WebApplication3.Domain.Entities.Employees", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Deletedby")
                        .HasColumnType("integer");

                    b.Property<string>("Designation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModifiedTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ModifiedBy")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
