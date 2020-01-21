using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ImageGallery.API.Entities;

namespace ImageGallery.API.Migrations
{
    [DbContext(typeof(GalleryContext))]
    [Migration("20170306141842_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ImageGallery.API.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("Uri")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.HasKey("Id");

                    b.ToTable("Images");
                });
        }
    }
}
