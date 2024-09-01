﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DBContext_EF))]
    [Migration("20240901192324_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Core.ExcelContent", b =>
                {
                    b.Property<string>("ExcelFileName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ExcelFileName");

                    b.ToTable("ExcelContents");
                });

            modelBuilder.Entity("Core.ExcelContentlRow", b =>
                {
                    b.Property<string>("ExcelFileName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("ColumnsValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExcelFileName", "Id");

                    b.ToTable("ExcelContentRows");
                });

            modelBuilder.Entity("Core.ExcelContentlRow", b =>
                {
                    b.HasOne("Core.ExcelContent", "ExcelContent")
                        .WithMany("Rows")
                        .HasForeignKey("ExcelFileName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExcelContent");
                });

            modelBuilder.Entity("Core.ExcelContent", b =>
                {
                    b.Navigation("Rows");
                });
#pragma warning restore 612, 618
        }
    }
}
