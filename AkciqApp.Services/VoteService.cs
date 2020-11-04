namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Models.Models;

    public class VoteService : IVoteService
    {
        private readonly IRepository<Vote> voteRepository;

        public VoteService(IRepository<Vote> voteRepository)
        {
            this.voteRepository = voteRepository;
        }

        public int GetVotes(int productId)
        {
            var votes = this.voteRepository.All()
                .Where(x => x.ProductId == productId)
                .Sum(x => (int)x.Type);
            return votes;
        }

        public async Task VoteAsync(int productId, string userId, bool isUpVote)
        {
            var vote = this.voteRepository.All()
                .FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
            if (vote != null)
            {
                vote.Type = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote()
                {
                    ProductId = productId,
                    UserId = userId,
                    Type = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.voteRepository.AddAsync(vote);
            }

            await this.voteRepository.SaveChangesAsync();
        }
    }
}
