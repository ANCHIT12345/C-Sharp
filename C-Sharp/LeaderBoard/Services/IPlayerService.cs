using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Services
{
    public interface IPlayerService
    {
        int CreatePlayer(User user);
        User GetPlayerById(int userId);
        List<User> GetAllPlayers();
        bool UpdatePlayer(User user);
        bool DeletePlayer(int userId);
        bool EmailExists(string email, int? excludingUserId = null);
    }
}
