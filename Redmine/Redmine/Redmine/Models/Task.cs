using System;
using System.Collections.Generic;
using System.Text;

namespace Redmine.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; } // Projekt azonosítója, amelyhez a feladat tartozik
        public int UserId { get; set; } // A feladatot létrehozó menedzser azonosítója
        public DateTime Deadline { get; set; }
    }
}
