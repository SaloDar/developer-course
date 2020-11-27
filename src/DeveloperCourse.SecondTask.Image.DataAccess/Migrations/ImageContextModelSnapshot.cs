﻿// <auto-generated />
using System;
using DeveloperCourse.SecondTask.Image.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeveloperCourse.SecondTask.Image.DataAccess.Migrations
{
    [DbContext(typeof(ImageContext))]
    partial class ImageContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeveloperCourse.SecondTask.Image.Domain.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnName("created_by")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnName("created_date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("LastSavedBy")
                        .HasColumnName("last_saved_by")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("LastSavedDate")
                        .HasColumnName("last_saved_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Link")
                        .HasColumnName("link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnName("product_id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("image");
                });
#pragma warning restore 612, 618
        }
    }
}
