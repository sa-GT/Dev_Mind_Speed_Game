using System;
using System.Collections.Generic;

namespace Dev_Mind_Speed_Game.Server.Models;

public partial class Question
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public string QuestionText { get; set; } = null!;

    public double CorrectAnswer { get; set; }

    public double? PlayerAnswer { get; set; }

    public double? TimeTaken { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();
}
