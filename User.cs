using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager
{
    public class User
    {
        [Key] //duid aan dat id een primary key is
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}