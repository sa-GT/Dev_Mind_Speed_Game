using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dev_Mind_Speed_Game.Server.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameResult> GameResults { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-6263BV65;Database=GameDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Games__3213E83F2659018A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Difficulty).HasColumnName("difficulty");
            entity.Property(e => e.IsEnded).HasColumnName("is_ended");
            entity.Property(e => e.PlayerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("player_name");
            entity.Property(e => e.StartTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<GameResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GameResu__3213E83F0E9A11AF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FastestQuestionId).HasColumnName("fastest_question_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.TotalTime).HasColumnName("total_time");

            entity.HasOne(d => d.FastestQuestion).WithMany(p => p.GameResults)
                .HasForeignKey(d => d.FastestQuestionId)
                .HasConstraintName("FK__GameResul__faste__403A8C7D");

            entity.HasOne(d => d.Game).WithMany(p => p.GameResults)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GameResul__game___3F466844");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Question__3213E83F01066EBB");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CorrectAnswer).HasColumnName("correct_answer");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.PlayerAnswer).HasColumnName("player_answer");
            entity.Property(e => e.QuestionText).HasColumnName("question_text");
            entity.Property(e => e.TimeTaken).HasColumnName("time_taken");

            entity.HasOne(d => d.Game).WithMany(p => p.Questions)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK__Questions__game___3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
