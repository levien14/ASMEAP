using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManager.Models;

namespace StudentManager.Models
{
    public class StudentManagerContext : DbContext
    {
        public StudentManagerContext (DbContextOptions<StudentManagerContext> options)
            : base(options)
        {
        }

        public DbSet<StudentManager.Models.Account> Account { get; set; }
        public DbSet<StudentManager.Models.ClassRoom> ClassRooms { get; set; }
        public DbSet<StudentManager.Models.Mark> Marks { get; set; }
        public DbSet<StudentManager.Models.Subject> Subjects { get; set; }
        public DbSet<StudentManager.Models.MyCredential> MyCredentials { get; set; }
        
    }
}
