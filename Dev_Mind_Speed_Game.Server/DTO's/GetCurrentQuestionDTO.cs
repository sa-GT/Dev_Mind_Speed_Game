namespace Dev_Mind_Speed_Game.Server.DTO_s
{
	public class GetCurrentQuestionDTO
	{
		public int QuestionId { get; set; }
		public string QuestionText { get; set; } = null!;
		public int GameId { get; set; }
	}
}
