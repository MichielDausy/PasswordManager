using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager
{
    public class Group
    {
        [Key] //duid aan dat id een primary key is
        public int Id { get; set; }
        public string GroupName { get; set; }

        [ForeignKey("userID")]// Duid aan dat userID een foreign key is naar de entiteit user
        public int userID { get; set; }
    }
}
