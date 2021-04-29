using IoTApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Metric> Metrics { get; set; }
        public virtual DbSet<Value> Values { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ValueSummarize> ValueSummarizes { get; set; }

        public ApplicationDbContext(
             DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Metric>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("metrics");


                entity.HasIndex(e => new { e.Name, e.Dimensions }, "metrics_name_dimensions_key")
                    .IsUnique();

                entity.Property(e => e.Dimensions)
                    .HasColumnType("jsonb")
                    .HasColumnName("dimensions");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Value>(entity =>
            {
                entity.ToTable("values");

                entity.HasIndex(e => new { e.MetricId, e.Timestamp }, "values_metric_id_timestamp_idx");

                entity.HasIndex(e => new { e.Timestamp, e.MetricId }, "values_timestamp_metric_id_idx");

                entity.Property(e => e.MetricId).HasColumnName("metric_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("timestamp");

                entity.Property(e => e.Value1).HasColumnName("value");

                entity.Property(e => e.ValueMeta)
                    .HasColumnType("json")
                    .HasColumnName("value_meta");
            });

            modelBuilder.Entity<ValueSummarize>(entity =>
            {
                entity.ToTable("values_summarize");

                //entity.HasIndex(e => new { e.MetricId, e.Timestamp }, "values_metric_id_timestamp_idx");

                //entity.HasIndex(e => new { e.Timestamp, e.MetricId }, "values_timestamp_metric_id_idx");

                entity.Property(e => e.MetricId).HasColumnName("metric_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp with time zone")
                    .HasColumnName("timestamp");

                entity.Property(e => e.TheSum).HasColumnName("theSum");

                entity.Property(e => e.TheCount).HasColumnName("theCount");

                //entity.Property(e => e.ValueMeta)
                //    .HasColumnType("json")
                //    .HasColumnName("value_meta");
            });



        }
    }
}
