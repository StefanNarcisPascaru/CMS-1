using System;
using System.Collections.Generic;
using System.Linq;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.Domain.Models;
using CMS.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    public class RankLogic : IRankLogic
    {
        private readonly IRepository<Rank> _rankRepository;
        private readonly IRepository<UserRank> _userRankRepository;

        public RankLogic(IRepository<Rank> rankRepository,IRepository<UserRank> userRankRepository)
        {
            _rankRepository = rankRepository;
            _userRankRepository = userRankRepository;
        }
        public IList<Rank> GetUserRanks(Guid id)
        {
            var ranks = new List<Rank>();
            var userRanks=_userRankRepository.Query(ur => ur.UserId==id).Include("Rank").ToList();
            foreach (var userRank in userRanks)
            {
                ranks.Add(userRank.Rank);
            }

            return ranks;
        }
    }
}
