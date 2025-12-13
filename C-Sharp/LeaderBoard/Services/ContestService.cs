using LeaderBoard.Models;
using Leaderboard.Data;
using Leaderboard.Models;
using System;
using System.Collections.Generic;

namespace Leaderboard.Services
{
    public class ContestService
    {
        private readonly ContestRepository _repo;
        public ContestService(ContestRepository repo) { _repo = repo ?? throw new ArgumentNullException(nameof(repo)); }

        public int CreateContest(Contest c) => _repo.Insert(c);
        public bool UpdateContest(Contest c) => _repo.Update(c);
        public bool DeleteContest(int id) => _repo.Delete(id);
        public Contest GetContest(int id) => _repo.GetById(id);
        public List<Contest> GetAllContests() => _repo.GetAll();
        public List<Contest> GetActiveContests(DateTime now) => _repo.GetActiveContests(now);
    }
}