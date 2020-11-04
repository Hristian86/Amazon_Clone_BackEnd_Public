namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Models.Models;

    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> ChangeUserName(string email, string userName)
        {
            var user = this.userRepository.All()
                .FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new ArgumentNullException("User does not exists");
            }

            user.UserName = userName;
            this.userRepository.Update(user);
            await this.userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IpAddress(string ip, string email)
        {
            var user = this.userRepository.All()
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                throw new ArgumentNullException("User does not exists");
            }
            else
            {
                var exists = user.IpAddresses.Any(x => x.Ip == ip);

                if (exists)
                {
                    return true;
                }
                else
                {
                    // To Do: ask the user for the unknown address login
                    var address = new IpAddress()
                    {
                        Ip = ip,
                        UserId = user.Id,
                        User = user,
                        Email = email,
                    };

                    user.IpAddresses.Add(address);
                    int currentCountsOfUserLogin = user.LoginsCount;
                    user.LoginsCount = currentCountsOfUserLogin += 1;
                    this.userRepository.Update(user);
                    await this.userRepository.SaveChangesAsync();
                    return false;
                }
            }
        }
    }
}
