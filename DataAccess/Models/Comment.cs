using System;

namespace DataAccess.Models
{
    public class Comment
    {

        public Comment()
        {

        }

        public Comment( long id, long issueId, string description, DateTime created )
        {
            Id = id;
            IssueId = issueId;
            Description = description;
            Created = created;
        }

        public long Id { get; set; }
        public long IssueId { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }


    }
}
