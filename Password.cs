using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class Password
    {
        [Key] //duid aan dat id een primary key is
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        [ForeignKey("groupID")]// Duid aan dat groupID een foreign key is naar de entiteit group
        public int groupID { get; set; }
    }
}
