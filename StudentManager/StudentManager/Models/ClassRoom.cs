using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Models
{
    public class ClassRoom
    {
        public ClassRoom()
        {
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = ClassRoomStatus.Active;
        }
        public int Id { get; set; }
        public string ClassName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClassRoomStatus Status { get; set; }
        public List<Account> Account { get; set; }
    }
    public enum ClassRoomStatus
    {
        Active=1,
        Deactive =0
    }
}
