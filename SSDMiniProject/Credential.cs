using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSDMiniProject
{
    public class Credential
    {
        public int Id { get; set; } // Add an ID property
        public string ServiceName { get; set; }
        public string Username { get; set; }
        public byte[] Salt { get; set; } // Unique salt for this credential
        public string EncryptedPassword { get; set; }
        public byte[] Iv { get; set; } // Initialization vector for encryption
        public string AdditionalInfo { get; set; }

    }
}
