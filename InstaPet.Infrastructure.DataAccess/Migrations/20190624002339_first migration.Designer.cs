﻿// <auto-generated />
using System;
using InstaPet.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InstaPet.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(InstaPetDbContext))]
    [Migration("20190624002339_first migration")]
    partial class firstmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("PhotoUrl");

                    b.Property<Guid?>("PostId");

                    b.Property<DateTime>("PublishDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Pet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnName("Breed");

                    b.Property<string>("Name");

                    b.Property<Guid?>("OwnerId");

                    b.Property<string>("PhotoUrl");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Pets");
                });

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<Guid?>("CreatorId");

                    b.Property<string>("PhotoUrl");

                    b.Property<DateTime>("PublishDateTime");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<string>("Country");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("InstaPet.Infrastructure.DataAccess.Contexts.Models.DbSpecie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Specie")
                        .IsRequired()
                        .HasColumnName("Specie");

                    b.HasKey("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Comment", b =>
                {
                    b.HasOne("InstaPet.DomainModel.Entities.Pet", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("InstaPet.DomainModel.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Pet", b =>
                {
                    b.HasOne("InstaPet.DomainModel.Entities.Profile", "Owner")
                        .WithMany("Pets")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("InstaPet.DomainModel.Entities.Post", b =>
                {
                    b.HasOne("InstaPet.DomainModel.Entities.Pet", "Creator")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId");
                });
#pragma warning restore 612, 618
        }
    }
}
