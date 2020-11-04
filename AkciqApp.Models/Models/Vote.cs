namespace AkciqApp.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AkciqApp.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        public VoteType Type { get; set; }
    }
}
