using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using AcessaCity.Business.Models;
using FirebaseAdmin.Auth;

namespace AcessaCity.Business.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IUserRepository _repo;
        private readonly FirebaseAuth _firebaseAuth;

        public UserService(INotifier notifier, IUserRepository repository, FirebaseAuth firebaseAuth) : base(notifier)
        {
            _repo = repository;
            _firebaseAuth = firebaseAuth;
        }

        public async Task Add(User user)
        {
            await _repo.Add(user);
        }

        public void Dispose()
        {
            _repo?.Dispose();
        }

        public Task<User> FindById(Guid Id)
        {
            return _repo.GetById(Id);
        }

        public async Task<User> FindUserByFirebaseId(string firebaseUserId)
        {
            var user = await _repo.Find(u => u.FirebaseUserId == firebaseUserId);

            return user.FirstOrDefault();
        }

        public async Task Update(User user)
        {
            await _repo.Update(user);
        }

        public async Task UpdateUserClaims(User user, Dictionary<string, object> claims)
        {
            await _firebaseAuth.SetCustomUserClaimsAsync(user.FirebaseUserId, claims);            
        }
    }
}