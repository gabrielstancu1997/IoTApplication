﻿// <auto-generated />
using System;
using IoTApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IoTApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("IoTApplication.Models.Metric", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Dimensions")
                        .HasColumnType("jsonb")
                        .HasColumnName("dimensions");

                    b.Property<string>("Name")
                        .HasColumnType("character varying")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Name", "Dimensions" }, "metrics_name_dimensions_key")
                        .IsUnique();

                    b.ToTable("metrics");
                });

            modelBuilder.Entity("IoTApplication.Models.Value", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("MetricId")
                        .HasColumnType("integer")
                        .HasColumnName("metric_id");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<double?>("Value1")
                        .HasColumnType("double precision")
                        .HasColumnName("value");

                    b.Property<string>("ValueMeta")
                        .HasColumnType("json")
                        .HasColumnName("value_meta");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "MetricId", "Timestamp" }, "values_metric_id_timestamp_idx");

                    b.HasIndex(new[] { "Timestamp", "MetricId" }, "values_timestamp_metric_id_idx");

                    b.ToTable("values");
                });
#pragma warning restore 612, 618
        }
    }
}
