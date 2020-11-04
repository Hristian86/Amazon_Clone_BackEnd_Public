namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int ProductId { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        public virtual Product Product { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
