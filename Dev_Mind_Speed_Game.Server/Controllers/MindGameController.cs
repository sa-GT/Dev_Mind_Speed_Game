using Dev_Mind_Speed_Game.Server.DTO_s;
using Dev_Mind_Speed_Game.Server.InterFaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace Dev_Mind_Speed_Game.Server.Controllers
{
	public class MindGameController : Controller
	{
		private readonly IDataservice _dataService;
		public MindGameController(IDataservice dataService)
		{
			_dataService = dataService;
		}
		[HttpPost("start")]
		public IActionResult StartGame([FromBody] StartGameDTO1 dto)
		{
			var result = _dataService.StartGame(dto);
			return Ok("Game started");
		}
		[HttpPost("submit")]
		public IActionResult SubmitAnswer([FromBody] SubmitAnswerDTO dto)
		{
			var result = _dataService.SubmitAnswer(dto);
			if (result)
			{
				return Ok("Correct answer");
			}
			else
			{
				return BadRequest("Incorrect answer or game not started yet");
			}
		}
		[HttpGet("getQuestionInfo")]
		public IActionResult GetQuestionInfo()
		{
			var question = _dataService.GetQuestionInfo();
			if (question != null)
			{
				return Ok(question);
			}
			return NotFound("Question not found for the given game ID");
		}
		[HttpGet("game/{gameId}/end")]
		public IActionResult EndGame(int gameId)
		{
			var result = _dataService.EndGame(gameId);
			if (result == null)
				return NotFound("Game not found or already ended");

			return Ok(result);
		}

	}
}
