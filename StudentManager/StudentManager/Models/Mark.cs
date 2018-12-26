using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Models
{
    public class Mark
    {
        public Mark()
        {
            Theory = 0;
            Practice = 0;
            Assignment = 0;
            CreatedAt = DateTime.Now;
            UpdateAt = DateTime.Now;

        }
        public int Id { get; set; }
        public int Theory { get; set; }
        public int Practice { get; set; }
        public int Assignment { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
