Circa Mind-Speed Game – Backend API

This project implements the backend logic of a fast-paced mind-speed game where a player answers math questions under time pressure.

---

Project Overview

This API allows players to:

- Start a new game with a selected difficulty.
- Receive randomly generated math questions.
- Submit answers and receive immediate feedback.
- Automatically get a new question upon a correct answer.
- End the game upon a wrong answer and get performance results.

---

Game Logic

 1. Start a Game – `/start`
- A player submits their name and difficulty (1 to 4).
- If no game is in progress for that player, a new `Game` is created.
- A `Question` is generated based on the difficulty:
  - Difficulty 1 → Addition
  - Difficulty 2 → Subtraction
  - Difficulty 3 → Multiplication
  - Difficulty 4 → Division (avoiding division by 0)

The question is stored in the `Questions` table with a reference to the `GameId`.

---

❓ 2. Get Current Question – `/question`
- Returns the latest question that belongs to the current active game.
- Helps the frontend show the player what to answer.

---

3. Submit Answer – `/submit-answer`
- The server compares the submitted answer to the correct answer.
- If **correct**:
  - Marks the question as correct and records time taken.
  - Generates a new question and continues the game.
- If **incorrect**:
  - Marks the game as ended.
  - Saves the final score and time spent.
  - Stores it in a `GameResult` table.

---

4. End Game – `/game/{game_id}/end`
Returns:
- Player name  
- Game difficulty  
- Current score = correct answers / total questions  
- Total time spent  
- Best (fastest) answer time among all questions

---

Tech Stack
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- LINQ & DTOs
- Clean service architecture

---

Sample Postman Usage
You can test the endpoints by importing the `PostmanCollection.json` included in this repo.

---

Features Implemented
- Random question generation per difficulty
- Accurate tracking of correct answers and time taken
- Final scoring logic with time and best question
- Clean API endpoints

---

Notes
- Questions are tied to the last active game for the player.
- Division is guarded against zero and adjusted if result is 0 (forced to 1).
