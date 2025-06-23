using Dev_Mind_Speed_Game.Server.DTO_s;
using Dev_Mind_Speed_Game.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Mind_Speed_Game.Server.InterFaces
{
	public interface IDataservice
	{
		public bool StartGame([FromBody] StartGameDTO1 dto);
		public bool SubmitAnswer([FromBody] SubmitAnswerDTO dto);
		public GetCurrentQuestionDTO? GetQuestionInfo();
	}
}
