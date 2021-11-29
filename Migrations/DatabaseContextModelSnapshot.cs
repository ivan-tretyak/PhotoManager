﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ORMDatabaseModule;

namespace PhotoManager.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("ORMDatabaseModule.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DateCreation")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("AlbumId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("ORMDatabaseModule.AlbumContext", b =>
                {
                    b.Property<int>("AlbumContextId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PhotoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlbumContextId");

                    b.HasIndex("AlbumId");

                    b.HasIndex("PhotoId");

                    b.ToTable("AlbumContexts");
                });

            modelBuilder.Entity("ORMDatabaseModule.MetaData", b =>
                {
                    b.Property<int>("MetadataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DateCreation")
                        .HasColumnType("TEXT");

                    b.Property<int>("Flash")
                        .HasColumnType("INTEGER");

                    b.Property<float>("FocusLength")
                        .HasColumnType("REAL");

                    b.Property<float>("Latitude")
                        .HasColumnType("REAL");

                    b.Property<float>("Longitude")
                        .HasColumnType("REAL");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .HasColumnType("TEXT");

                    b.Property<int>("Orientation")
                        .HasColumnType("INTEGER");

                    b.HasKey("MetadataId");

                    b.ToTable("MetaDatas");
                });

            modelBuilder.Entity("ORMDatabaseModule.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Exist")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MetaDataId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Path")
                        .HasColumnType("TEXT");

                    b.HasKey("PhotoId");

                    b.HasIndex("MetaDataId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("ORMDatabaseModule.AlbumContext", b =>
                {
                    b.HasOne("ORMDatabaseModule.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ORMDatabaseModule.Photo", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("ORMDatabaseModule.Photo", b =>
                {
                    b.HasOne("ORMDatabaseModule.MetaData", "MetaData")
                        .WithMany()
                        .HasForeignKey("MetaDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MetaData");
                });
#pragma warning restore 612, 618
        }
    }
}
