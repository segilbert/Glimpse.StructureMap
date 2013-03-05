//
using System;

namespace Glimpse.StructureMap.Sample.Core.Domain
{
    public class Comment
    {
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } // This should always be Utc.
        public string CreatedByUserId { get; set; }
        public int UpVoteCount { get; set; }
    }
}