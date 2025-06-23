namespace Dev_Mind_Speed_Game.Server.DTO_s
{
	public class GameResultDTO
	{
		public string Name { get; set; }
		public int Difficulty { get; set; }
		public string CurrentScore { get; set; }
		public double TotalTimeSpent { get; set; }
		public double BestScore { get; set; }
	}
}
