using LeaderBoard.Data;
using LeaderBoard.Models;
using System;
using System.Collections.Generic;

namespace Leaderboard.Services
{
    public class RatingService
    {
        private readonly PlayerRepository _playerRepo;
        public RatingService(PlayerRepository playerRepo)
        {
            _playerRepo = playerRepo ?? throw new ArgumentNullException(nameof(playerRepo));
        }
        public void UpdateRatingsForContest(List<ContestLeaderrBoard> leaderboardRows)
        {
            if (leaderboardRows == null || leaderboardRows.Count == 0) return;

            const decimal baseDelta = 25m; 
            int maxRank = 0;
            foreach (var r in leaderboardRows) if (r.Rank > maxRank) maxRank = r.Rank;

            foreach (var r in leaderboardRows)
            {
                var factor = maxRank > 0 ? (decimal)(maxRank - r.Rank + 1) / maxRank : 1m;
                var delta = Math.Round(baseDelta * factor, 2);

                var player = _playerRepo.GetById(r.PlayerID);
                if (player == null) continue;

                var oldRating = player.Rating ?? 1000m;
                decimal newRating;
                if (r.Rank <= (maxRank / 2))
                    newRating = oldRating + delta;
                else
                    newRating = Math.Max(0, oldRating - delta); 

                _playerRepo.UpdateRating(r.PlayerID, newRating);
            }
        }
    }
}
