using LeaderBoard.Models;
using LeaderboardApp.Data;
using System;
using System.Collections.Generic;

namespace LeaderboardApp.Services
{
    public class GameService
    {
        private readonly GameRepository _repo;

        public GameService(GameRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public List<Game> GetAllGames() => _repo.GetAll();
        public Game GetGame(int id) => _repo.GetById(id);
        public int CreateGame(Game g) => _repo.Insert(g);
        public bool UpdateGame(Game g) => _repo.Update(g);
        public bool DeleteGame(int id) => _repo.Delete(id);

        // Import games from JSON file and upsert to DB
        public int ImportGamesFromJson(string jsonFilePath) => _repo.ImportFromJsonFile(jsonFilePath);
    }
}