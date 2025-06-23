using Dev_Mind_Speed_Game.Server.DTO_s;
using Dev_Mind_Speed_Game.Server.InterFaces;
using Dev_Mind_Speed_Game.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Mind_Speed_Game.Server.Services
{
	public class Dataservice : IDataservice
	{
		private readonly MyDbContext _context;
		public Dataservice(MyDbContext context)
		{
			_context = context;
		}
		public bool StartGame([FromBody] StartGameDTO1 dto)
		{
			var lastGame = _context.Games
				.OrderByDescending(g => g.Id)
				.FirstOrDefault(g => g.PlayerName == dto.PlayerName && !g.IsEnded);

			if (lastGame == null)
			{
				var newGame = new Game
				{
					PlayerName = dto.PlayerName,
					Difficulty = dto.Difficulty,
				};
				_context.Games.Add(newGame);
				_context.SaveChanges();

				lastGame = newGame;
			}

			var rand = new Random();
			var num1 = rand.Next(1, 20);
			var num2 = rand.Next(1, 20);
			string questionText = "";
			int correctAnswer = 0;

			switch (lastGame.Difficulty)
			{
				case 1:
					questionText = $"{num1} + {num2} ?";
					correctAnswer = num1 + num2;
					break;
				case 2:
					questionText = $"{num1} - {num2} ?";
					correctAnswer = num1 - num2;
					break;
				case 3:
					questionText = $"{num1} * {num2} ?";
					correctAnswer = num1 * num2;
					break;
				case 4:
					num2 = rand.Next(1, 10);
					num1 = num2 * rand.Next(1, 10);
					questionText = $"{num1} / {num2} ?";
					correctAnswer = Math.Max(1, num1 / num2);
					break;
			}

			_context.Questions.Add(new Question
			{
				QuestionText = questionText,
				CorrectAnswer = correctAnswer,
				GameId = lastGame.Id,
				CreatedAt = DateTime.UtcNow
			});

			_context.SaveChanges();
			return true;
		}
		public bool SubmitAnswer([FromBody] SubmitAnswerDTO dto)
		{
			var lastQuestion = _context.Questions
				.OrderByDescending(q => q.Id)
				.FirstOrDefault();
			var scorr = 0;
			var game = _context.Games
				.FirstOrDefault(g => g.Id == lastQuestion.GameId);

			if (lastQuestion == null || game == null)
			{
				return false;
			}
			if (dto.Answer == lastQuestion.CorrectAnswer)
			{
				lastQuestion.PlayerAnswer = dto.Answer;
				lastQuestion.TimeTaken = (DateTime.UtcNow - lastQuestion.CreatedAt).TotalSeconds;
				_context.Update(lastQuestion);
				_context.SaveChanges();

				var dtoGame = new StartGameDTO1
				{
					PlayerName = game.PlayerName,
					Difficulty = game.Difficulty
				};
				scorr += 10;
				var resultgame = new GameResult
				{
					GameId = game.Id,
					Score = scorr,
					FastestQuestionId = lastQuestion.Id,
					TotalTime = lastQuestion.TimeTaken
				};
				_context.GameResults.Add(resultgame);
				_context.SaveChanges();
				StartGame(dtoGame);
				return true;
			}
			else
			{
				game.IsEnded = true;
				_context.Update(game);
				_context.SaveChanges();
				return false;
			}
		}

		public GetCurrentQuestionDTO? GetQuestionInfo()
		{
			var lastGame = _context.Games
				.OrderByDescending(g => g.Id)
				.FirstOrDefault(g => !g.IsEnded);

			if (lastGame == null)
			{
				return null;
			}

			var question = _context.Questions
				.Where(q => q.GameId == lastGame.Id)
				.OrderByDescending(q => q.Id)
				.FirstOrDefault();

			if (question == null)
			{
				return null;
			}

			return new GetCurrentQuestionDTO
			{
				QuestionId = question.Id,
				QuestionText = question.QuestionText,
				GameId = question.GameId
			};
		}

	}
}
