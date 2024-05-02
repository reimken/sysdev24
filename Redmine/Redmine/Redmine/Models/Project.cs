using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public List<int> DeveloperIds { get; set; } // A projekthez hozzárendelt fejlesztők azonosítói
    }
}
