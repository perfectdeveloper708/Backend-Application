using System;
using System.Collections.Generic;

#nullable disable

namespace DemoAPI.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ProductTypeId { get; set; }
        public int? ContactPersonId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? CreatedBy { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? LastUpdatedOn { get; set; }

        public virtual ContactPerson ContactPerson { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
