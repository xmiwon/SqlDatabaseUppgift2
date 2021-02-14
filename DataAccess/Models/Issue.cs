using System;
using System.Collections.Generic;

namespace DataAccess.Models
{
    public class Issue
    {
        public Issue()
        {

        }

        public Issue( long id, long customerId, string title, string description, string status, DateTime created, string category, string picture )
        {
            Id = id;
            CustomerId = customerId;
            Title = title;
            Description = description;
            Status = status;
            Created = created;
            Category = category;
            PictureSource = picture;
        }

        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string Category { get; set; }
        public string PictureSource { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
