using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManager.Models
{
    public class MyCredential
    {
        public MyCredential()
        {

        }
        public MyCredential(int id)
        {
            AccessToken = Guid.NewGuid().ToString();
            OwnId = id;
            CretedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            ExpireAt = DateTime.Now.AddDays(7);
        }
        [Key]
        public string AccessToken { get; set; }
        public int OwnId { get; set; }
        public DateTime CretedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime ExpireAt { get; set; }
    }
    public enum CredentialStatus 
    {
        Active = 1,
        Deactive = 0
    }
}
