namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IUserService
    {
        public Task<bool> SaveIpAddress(string ip);

        bool UserNameExists(string userName);

        Task<bool> IpAddress(string ip, string email);

        Task<bool> ChangeUserName(string email, string userName);
    }
}
