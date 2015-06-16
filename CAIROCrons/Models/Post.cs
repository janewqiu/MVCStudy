using ServiceStack.DataAnnotations;
using ServiceStack.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;



namespace CAIROCrons.Models
{

     [Alias("Posts")]
    public class Post   
    {
         public Post ()
         {
             if (this.Id == Guid.Empty)this.Id = Guid.NewGuid();
         }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        [Index(Unique = true)]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(350)]
        public string Url { get; set; }

        [StringLength(350)]
        public string Summary { get; set; }

        [System.ComponentModel.DataAnnotations.UIHint("WYSIWYG")]
        [AllowHtml]
        [StringLength(50000)]
        public string Details { get; set; }

        [StringLength(150)]
        public string Author { get; set; }
        public int TotalComments { get; set; }
        public IList<Comment> Comments { get; set; }

        



         
    }
}
