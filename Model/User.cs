using System;
using System.Collections.Generic;

namespace trainingmiddleware.Model
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
