namespace AkciqApp.ViewModels.OutPutViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using AkciqApp.Mapping;
    using AkciqApp.Models.Models;

    public class ProductsInCategoryViewModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public int VotesCount { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public int Quantity { get; set; }

        public string ImageUrl { get; set; }

        public Decimal Price { get; set; }

        public string ShortContent
        {
            get
            {
                var content = WebUtility.HtmlDecode(Regex.Replace(this.Content, @"<[^>]+>", string.Empty));
                return content.Length > 260 ? content.Substring(0, 260) + "...." : content;
            }
        }

        public int ProductsCount { get; set; }

        public string Content { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public int PositiveVotes
        {
            get => this.Votes.Where(x => x.Type == VoteType.UpVote).Count();
        }

        public int NegativeVotes
        {
            get => this.Votes.Where(x => x.Type == VoteType.DownVote).Count();
        }
    }
}
