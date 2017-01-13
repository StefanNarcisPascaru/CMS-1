using System;
using System.Collections.Generic;
using System.Linq;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.Domain.Models;
using CMS.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class UserLogic : IUserLogic
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Rank> _rankRepository;
        private readonly IRepository<UserRank> _userRankRepository;

        public UserLogic(IRepository<User> users, IRepository<Rank> ranks, IRepository<UserRank> userRanks)
        {
            _userRepository = users;
            _rankRepository = ranks;
            _userRankRepository = userRanks;
        }

        public ICollection<string> GetRanks(string user)
        {
            ICollection<string> ranksToString=new HashSet<string>();
            var userRanks= _userRepository.Query()
                .Include(u => u.UserRanks).FirstOrDefault(u => u.UserName.Equals(user)).UserRanks;
            foreach (var userRank in userRanks)
            {
                ranksToString.Add(_rankRepository.Query(r => r.Id == userRank.RankId).FirstOrDefault().Name);
            }
            return ranksToString;
        }

        public ICollection<User> GetUsers()
        {
            return _userRepository.Query().ToList();
        }

        public User GetUser(Guid id)
        {
            return _userRepository.Query(u => u.Id==id).FirstOrDefault();
        }
        public bool IsValidUser(string user, string password)
        {
            return _userRepository.Query(u=> u.Password==password).Any(u => u.Email==user||u.UserName==user);
        }

        public void Update(User user,IList<Rank> ranks)
        {
            var editedUser=_userRepository.Query(u => u.Id == user.Id).FirstOrDefault();
            editedUser.Email = user.Email;
            editedUser.UserName = user.UserName;
            editedUser.CompleteName = user.CompleteName;
            _userRepository.SaveChanges();
            List<Guid> userRankList=new List<Guid>();
            foreach (var rank in ranks)
            {
                if (rank.Name != null)
                {
                    var rankId = _rankRepository.Query(r => r.Name.Equals(rank.Name)).FirstOrDefault().Id;
                    var userRank = _userRankRepository.Query(ur => ur.UserId == user.Id && ur.RankId == rankId).FirstOrDefault();
                    if (userRank == null)
                    {
                        _userRankRepository.Add(new UserRank(user.Id, rankId));
                    }
                    userRankList.Add(rankId);
                }
            }
            var userRanks = _userRankRepository.Query(ur => ur.UserId == user.Id).ToList();
            foreach (var uRank in userRanks)
            {
                if (!userRankList.Contains(uRank.RankId))
                {
                    _userRankRepository.Delete(uRank.Id);
                }
            }
            _userRankRepository.SaveChanges();
        }
    }
}
