//
using System;
using System.Collections.Generic;

namespace Glimpse.StructureMap.Sample.Core.Domain
{
    public class Question
    {
        public Question()
        {
            NumberOfViews = 1;
            CreatedOn = DateTime.UtcNow;
        }

        //[Required]
        public string Subject { get; set; }
        //[Required]
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } // This should always be Utc.
        //[Required]
        public ICollection<string> Tags { get; set; }
        //[Required(ErrorMessage = "You need to be logged in to ask a question.")]
        public string CreatedByUserId { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public int NumberOfViews { get; set; }
        public Vote Vote { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
