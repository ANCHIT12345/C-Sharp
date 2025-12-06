using LeaderBoard.Data;
using LeaderBoard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaderBoard.Services
{
    
    public class PlayerService : IPlayerService
    {
        private readonly PlayerRepository _repo;
        public PlayerService(PlayerRepository repository)
        {
            _repo = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public int CreatePlayer(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(user.UserName)) throw new ArgumentException("UserName is required", nameof(user));
            if (!user.ValidateEmail()) throw new ArgumentException("Invalid email format", nameof(user));
            if (_repo.EmailExists(user.Email)) throw new ArgumentException("Email already exists", nameof(user));
            return _repo.Insert(user);
        }
        public User GetPlayerById(int id) => _repo.GetById(id);
        public List<User> GetAllPlayers() => _repo.GetAll();
        public bool UpdatePlayer(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user.UserID <= 0) throw new ArgumentException("Invalid UserID", nameof(user));
            if (string.IsNullOrWhiteSpace(user.UserName)) throw new ArgumentException("UserName is required", nameof(user));
            if (!user.ValidateEmail()) throw new ArgumentException("Invalid email format", nameof(user));
            if (_repo.EmailExists(user.Email, user.UserID)) throw new ArgumentException("Email already exists", nameof(user));
            return _repo.Update(user);
        }
        public bool DeletePlayer(int id) => _repo.Delete(id);
        public bool EmailExists(string email, int? excludingUserId = null) => _repo.EmailExists(email, excludingUserId);
    }
}
