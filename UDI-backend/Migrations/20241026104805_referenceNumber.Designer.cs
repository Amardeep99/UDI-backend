﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UDI_backend.Models;

#nullable disable

namespace UDI_backend.Migrations
{
    [DbContext(typeof(UdiDatabase))]
    [Migration("20241026104805_referenceNumber")]
    partial class referenceNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UDI_backend.Models.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DNumber")
                        .HasColumnType("int")
                        .HasColumnName("dnumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<DateTime>("TravelDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("traveldate");

                    b.HasKey("Id")
                        .HasName("pk_applications");

                    b.ToTable("applications", (string)null);
                });

            modelBuilder.Entity("UDI_backend.Models.Form", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("contactname");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<bool>("HasDebt")
                        .HasColumnType("bit")
                        .HasColumnName("hasdebt");

                    b.Property<bool>("HasObjection")
                        .HasColumnType("bit")
                        .HasColumnName("hasobjection");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.Property<int>("ReferenceNumber")
                        .HasColumnType("int")
                        .HasColumnName("referencenumber");

                    b.Property<DateOnly?>("SuggestedTravelDate")
                        .HasColumnType("date")
                        .HasColumnName("suggestedtraveldate");

                    b.HasKey("Id")
                        .HasName("pk_forms");

                    b.HasIndex("ReferenceNumber")
                        .IsUnique()
                        .HasDatabaseName("ix_forms_referencenumber");

                    b.ToTable("forms", (string)null);
                });

            modelBuilder.Entity("UDI_backend.Models.Reference", b =>
                {
                    b.Property<int>("ReferenceNumber")
                        .HasColumnType("int")
                        .HasColumnName("referencenumber");

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int")
                        .HasColumnName("applicationid");

                    b.Property<DateTime>("Deadline")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("deadline")
                        .HasDefaultValueSql("DATEADD(day, 14, GETUTCDATE())");

                    b.Property<int?>("FormId")
                        .HasColumnType("int")
                        .HasColumnName("formid");

                    b.Property<int>("OrganisationNr")
                        .HasColumnType("int")
                        .HasColumnName("organisationnr");

                    b.HasKey("ReferenceNumber")
                        .HasName("pk_references");

                    b.HasIndex("ApplicationId")
                        .HasDatabaseName("ix_references_applicationid");

                    b.ToTable("references", (string)null);
                });

            modelBuilder.Entity("UDI_backend.Models.Form", b =>
                {
                    b.HasOne("UDI_backend.Models.Reference", "Reference")
                        .WithOne("Form")
                        .HasForeignKey("UDI_backend.Models.Form", "ReferenceNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_forms_references_referencenumber");

                    b.Navigation("Reference");
                });

            modelBuilder.Entity("UDI_backend.Models.Reference", b =>
                {
                    b.HasOne("UDI_backend.Models.Application", "Application")
                        .WithMany("References")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_references_applications_applicationid");

                    b.Navigation("Application");
                });

            modelBuilder.Entity("UDI_backend.Models.Application", b =>
                {
                    b.Navigation("References");
                });

            modelBuilder.Entity("UDI_backend.Models.Reference", b =>
                {
                    b.Navigation("Form");
                });
#pragma warning restore 612, 618
        }
    }
}
