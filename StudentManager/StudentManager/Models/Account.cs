using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Models
{
    public class Account
    {
        public Account()
        {
            Gender = AccountGender.Other;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Status = AccountStatus.Active;
        }
        [Key]
        public int ID { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(4)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FullName { get; set; }
        [Display(Name = "Birthday")]
        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public AccountGender Gender { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public AccountStatus Status { get; set; }
        public int ClassRoomId { get; set; }
        public ClassRoom ClassRoom { get; set; }
        public List<Mark> Marks { get; set; }

    }
    public enum AccountStatus
    {
        Active =1,
        Deactive =0
    }
    public enum AccountGender
    {
        Male = 1,
        FeMale =0,
        Other = 2
    }
}
