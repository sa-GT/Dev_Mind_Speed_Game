using System;
using System.Collections.Generic;

namespace Dev_Mind_Speed_Game.Server.Models;

public partial class Game
{
    public int Id { get; set; }

    public string PlayerName { get; set; } = null!;

    public int Difficulty { get; set; }

    public DateTime StartTime { get; set; }

    public bool IsEnded { get; set; }

    public virtual ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
