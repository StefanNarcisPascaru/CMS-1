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

        public UserLogic(IRepository<User> users, IRepository<Rank> ranks)
        {
            _userRepository = users;
            _rankRepository = ranks;
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

        public User GetUser(Guid id)
        {
            return _userRepository.Query(u => u.Id==id).FirstOrDefault();
        }
        //return rank
        public bool IsValidUser(string user, string password)
        {
            return _userRepository.Query(u=> u.Password==password).Any(u => u.Email==user||u.UserName==user);
        }
    }
}
