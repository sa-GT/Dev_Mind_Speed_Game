using System;
using System.Collections.Generic;

namespace Dev_Mind_Speed_Game.Server.Models;

public partial class GameResult
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public double? Score { get; set; }

    public double? TotalTime { get; set; }

    public int? FastestQuestionId { get; set; }

    public virtual Question? FastestQuestion { get; set; }

    public virtual Game Game { get; set; } = null!;
}
