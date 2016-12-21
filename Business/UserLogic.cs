using System;
using System.Linq;
using CMS.BussinesInterfaces.ModelLogic;
using CMS.Domain.Models;
using CMS.RepositoryInterfaces;

namespace Business
{
    public class UserLogic : IUserLogic
    {
        private readonly IRepository<User> _userRepository;

        public UserLogic(IRepository<User> users)
        {
            _userRepository = users;
        }

        public User GetUser(Guid id)
        {
            return _userRepository.Query(u => u.Id==id).FirstOrDefault();
        }

        public bool IsValidUser(string user, string password)
        {
            return _userRepository.Query(u=> u.Password==password).Any(u => u.Email==user||u.UserName==user);
        }
    }
}
