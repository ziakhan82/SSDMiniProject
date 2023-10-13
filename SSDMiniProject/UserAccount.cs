using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; } // Define an integer primary key
        public string HashedPassword { get; set; }
        public byte[] Salt { get; set; }
    }
}
