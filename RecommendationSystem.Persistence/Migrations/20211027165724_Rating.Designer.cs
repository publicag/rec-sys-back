﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RecommendationSystem.Persistence;

namespace RecommendationSystem.Persistence.Migrations
{
    [DbContext(typeof(RecommendationSystemDbContext))]
    [Migration("20211027165724_Rating")]
    partial class Rating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("RecommendationSystem.Domain.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date_time");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<int?>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<int>("Name")
                        .HasColumnType("integer")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_genre");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_genre_movie_id");

                    b.ToTable("genre");
                });

            modelBuilder.Entity("RecommendationSystem.Domain.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date_time");

                    b.Property<int>("ImdbId")
                        .HasColumnType("integer")
                        .HasColumnName("imdb_id");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<string>("Title")
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("TmdbId")
                        .HasColumnType("integer")
                        .HasColumnName("tmdb_id");

                    b.HasKey("Id")
                        .HasName("pk_movie");

                    b.ToTable("movie");
                });

            modelBuilder.Entity("RecommendationSystem.Domain.Entities.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("created_date_time");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("modified_date_time");

                    b.Property<int?>("MovieId")
                        .HasColumnType("integer")
                        .HasColumnName("movie_id");

                    b.Property<float>("Rate")
                        .HasColumnType("real")
                        .HasColumnName("rate");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_rating");

                    b.HasIndex("MovieId")
                        .HasDatabaseName("ix_rating_movie_id");

                    b.ToTable("rating");
                });

            modelBuilder.Entity("RecommendationSystem.Domain.Entities.Genre", b =>
                {
                    b.HasOne("RecommendationSystem.Domain.Entities.Movie", null)
                        .WithMany("Genres")
                        .HasForeignKey("MovieId")
                        .HasConstraintName("fk_genre_movie_movie_id");
                });

            modelBuilder.Entity("RecommendationSystem.Domain.Entities.Rating", b =>
                {
                    b.HasOne("RecommendationSystem.Domain.Entities.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .HasConstraintName("fk_rating_movie_movie_id");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("RecommendationSystem.Domain.Entities.Movie", b =>
                {
                    b.Navigation("Genres");
                });
#pragma warning restore 612, 618
        }
    }
}
