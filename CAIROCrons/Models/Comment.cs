using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;




namespace CAIROCrons.Models
{
    [Alias("Comments")]
    public class Comment   : IHasId<Guid>
    {
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }

        public DateTime Date { get; set; }

        [StringLength(150)]
        public string Author { get; set; }

        [StringLength(5000)]
        public string Detail { get; set; }

        public Guid Id
        {
            get { return Guid.NewGuid() ; }
        }
    }
}
