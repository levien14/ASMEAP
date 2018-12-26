using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Models
{
    public class Subject
    {
        public Subject()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Mark> Marks { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
