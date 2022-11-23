using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Deleted { get; set; }
        public List<SubCategories> SubCategories { get; set; }
    }
}
