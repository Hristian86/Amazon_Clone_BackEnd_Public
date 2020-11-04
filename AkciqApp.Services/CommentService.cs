namespace AkciqApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AkciqApp.Common.Repositories;
    using AkciqApp.Models.Models;

    public class CommentService : ICommentService
    {
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly IProductService postService;

        public CommentService(
            IDeletableEntityRepository<Comment> commentRepository,
            IProductService postService)
        {
            this.commentRepository = commentRepository;
            this.postService = postService;
        }

        public async Task CreateComment(int productId, string userId, string content, string title)
        {
            var exists = this.postService.PostExists(productId);
            if (!exists)
            {
                throw new ArgumentException("Post id is not found");
            }

            if (content == null || content.Length < 5)
            {
                throw new ArgumentException("Content must be at least 5 symbols");
            }
            else if (content.Length > 15000)
            {
                throw new ArgumentException("Content must be under 15000 symbols");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("User is required");
            }

            var comment = new Comment()
            {
                Content = content,
                ProductId = productId,
                UserId = userId,
                Title = title,
            };

            await this.commentRepository.AddAsync(comment);
            await this.commentRepository.SaveChangesAsync();
        }
    }
}
