﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReactionsService.Context;

namespace ReactionsService.Migrations
{
    [DbContext(typeof(ReactionsContext))]
    partial class ReactionsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReactionsService.Entity.Reaction", b =>
                {
                    b.Property<int>("ReactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("ReactionTypeId")
                        .HasColumnType("int");

                    b.HasKey("ReactionId");

                    b.HasIndex("ReactionTypeId");

                    b.ToTable("Reactions");
                });

            modelBuilder.Entity("ReactionsService.Entity.ReactionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ReactionTypes");
                });

            modelBuilder.Entity("ReactionsService.Entity.Reaction", b =>
                {
                    b.HasOne("ReactionsService.Entity.ReactionType", "ReactionType")
                        .WithMany("ListReactions")
                        .HasForeignKey("ReactionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReactionType");
                });

            modelBuilder.Entity("ReactionsService.Entity.ReactionType", b =>
                {
                    b.Navigation("ListReactions");
                });
#pragma warning restore 612, 618
        }
    }
}
